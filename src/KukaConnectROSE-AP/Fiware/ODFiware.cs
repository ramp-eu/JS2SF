using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serialization;
using KUKABe.RobotConnection;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Linq;


namespace KukaConnectROSE_AP.Fiware
{
    public class ODFiware
    {
        #region variables
        private readonly RestClient m_client;

        private int pauseNumberOfTicksAfterError = 0;
        private bool timertickFinishedGeneral = true;
        private bool timertickFinishedIO = true;
        private bool updateRobotIOThisTimertick = true;

        private RobotConnection myRobotIO;
        public List<RobotInput> irecords = new List<RobotInput>();
        public List<RobotOutput> orecords = new List<RobotOutput>();
        private string serialNumber = "";
        #endregion

        #region constructors
        public ODFiware(RoseAPSettings roseAPSettings) 
        {
            m_client = new RestClient(roseAPSettings.ContextBrokerUrl);
            if (myRobotIO == null)
            {
                myRobotIO = new RobotConnection();
            }
            myRobotIO.LocalAddress = roseAPSettings.LocalAddress;
            myRobotIO.RemoteAddress = roseAPSettings.RemoteAddress;
            myRobotIO.TCP_Used = roseAPSettings.TCP_Used;
            myRobotIO.TCP_LocalPort = roseAPSettings.TCP_LocalPort;
            myRobotIO.TCP_RemotePort = roseAPSettings.TCP_RemotePort;
            myRobotIO.UDP_Used = roseAPSettings.UDP_Used;
            myRobotIO.UDP_LocalPort = roseAPSettings.UDP_LocalPort;
            myRobotIO.UDP_RemotePort = roseAPSettings.UDP_RemotePort;
            myRobotIO.Enabled = roseAPSettings.Enabled;
            myRobotIO.NetworkTimeout = roseAPSettings.NetworkTimeout;
            myRobotIO.Start();
            string path = roseAPSettings.ConfigDatPath;
            Thread.Sleep(1000);
            if (
                 myRobotIO.ConnectionStatus == eRobotConnectionState.ConnectedUDP
                 ||
                 myRobotIO.ConnectionStatus == eRobotConnectionState.ConnectedTCP
                 )
            {
                myRobotIO.GetVar("$KR_SERIALNO", ref serialNumber);
            }
            if (serialNumber == "")
            {
                Console.WriteLine("Problem getting correct serialNumber, check robot connection.");
            } else { 
                ParseRobotconfig(path);
                bool inputsOrOutputsChanged = ClearRobotIOIfInputsOrOutputsChanged();
                if (inputsOrOutputsChanged)
                {
                    AddRobotIO(irecords, orecords);
                }
            }

        }

        private bool ClearRobotIOIfInputsOrOutputsChanged()
        {
            List<string> irecordNames = new List<string>();
            List<string> orecordNames = new List<string>();
            foreach (RobotInput inp in irecords)
            {
                irecordNames.Add(inp.Name);
            }
            foreach (RobotOutput outp in orecords)
            {
                orecordNames.Add(outp.Name);
            }
            List<string> ocbInputNames = new List<string>();
            //  curl -iX GET   --url 'http://localhost:1026/v2/entities?type=Input&q=robotSerialNumber==456789&limit=1000'
            var request = new RestRequest("v2/entities?type=Input&limit=1000&q=robotSerialNumber==" + serialNumber);
            request.Method = Method.GET;
            IRestResponse response = m_client.Execute(request);
            CheckFiwareResponse(response);
            JArray inputsJsonArray = JArray.Parse(response.Content);
            foreach (var inputJson in inputsJsonArray)
            {
                string inputID = (string)inputJson.SelectToken("id");
                ocbInputNames.Add(inputID.Substring(inputID.LastIndexOf(":")+1));
            }

            List<string> ocbOutputNames = new List<string>();
            var request2 = new RestRequest("v2/entities?type=Output&limit=1000&q=robotSerialNumber==" + serialNumber);
            request2.Method = Method.GET;
            IRestResponse response2 = m_client.Execute(request2);
            CheckFiwareResponse(response2);
            JArray outputsJsonArray2 = JArray.Parse(response2.Content);
            foreach (var outputJson in outputsJsonArray2)
            {
                string outputID = (string)outputJson.SelectToken("id");
                ocbOutputNames.Add(outputID.Substring(outputID.LastIndexOf(":") + 1));
            }

            if (irecordNames.SequenceEqual(ocbInputNames) && orecordNames.SequenceEqual(ocbOutputNames))
            {
                return false;
            } else
            {
                // Delete inputs
                foreach (var inputJson in inputsJsonArray)
                {
                    string inputID = (string)inputJson.SelectToken("id");
                    // curl -iX DELETE --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:Input:456789-STARTBUTTON'
                    var request3 = new RestRequest("v2/entities/" + inputID);
                    request3.Method = Method.DELETE;
                    IRestResponse response3 = m_client.Execute(request3);
                    CheckFiwareResponse(response3);
                }
                // Delete outputs
                foreach (var outputJson in outputsJsonArray2)
                {
                    string outputID = (string)outputJson.SelectToken("id");
                    var request4 = new RestRequest("v2/entities/" + outputID);
                    request4.Method = Method.DELETE;
                    IRestResponse response4 = m_client.Execute(request4);
                    CheckFiwareResponse(response4);
                }
                return true;
            }

        }
        #endregion

        #region methods
        public void Tick()
        {
            try
            {
                if (pauseNumberOfTicksAfterError > 0)
                {
                    pauseNumberOfTicksAfterError--;
                }
                else
                {
                    if (timertickFinishedGeneral)
                    {
                        timertickFinishedGeneral = false;
                        
                        UpdateRobotInfo();
                        
                    }
                    if (timertickFinishedIO && updateRobotIOThisTimertick)
                    {
                        timertickFinishedIO = false;
                        UpdateRobotIO(irecords, orecords);
                    }
                    updateRobotIOThisTimertick = !updateRobotIOThisTimertick;  // update IO every other tick

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem with Fiware Tick " + ex);
            }
        }

        private void CheckFiwareResponse(IRestResponse response)
        {
            if (response.ErrorException != null)
            {
                Console.WriteLine("Problem with Fiware update " + response.ErrorMessage);
                pauseNumberOfTicksAfterError = 60;
            }
        }

        public async void UpdateRobotInfo()
        {
            if (serialNumber != "")
            {
                var request = new RestRequest("v2/entities/urn:ngsi-ld:Robot:" + serialNumber + "/attrs");
                request.Method = Method.POST;
                request.AddHeader("Content-Type", "application/json");
                string proState0 = string.Empty;
                string proState1 = string.Empty;
                // #P_FREE: Program is not selected.
                // #P_ACTIVE: Program is active.
                // #P_END: End of program has been reached.
                // #P_RESET: Program has been reset.
                // #P_STOP: Program has been stopped.
                myRobotIO.GetVar("$PRO_STATE0", ref proState0);
                myRobotIO.GetVar("$PRO_STATE1", ref proState1);

                string drivesOnString = string.Empty;
                myRobotIO.GetVar("$Drives_on", ref drivesOnString);
                bool drivesOn = Convert.ToBoolean(drivesOnString);

                string drivesOffString = string.Empty;
                myRobotIO.GetVar("$Drives_off", ref drivesOffString);
                bool drivesOff = Convert.ToBoolean(drivesOffString);

                // periphery Ready (doors closed etc)
                string periReadyString = string.Empty;
                myRobotIO.GetVar("$Peri_Rdy", ref periReadyString);
                bool periReady = Convert.ToBoolean(periReadyString);

                // Emergency Stop
                string alarmStopString = string.Empty;
                myRobotIO.GetVar("$ALARM_STOP", ref alarmStopString);
                bool alarmStop = Convert.ToBoolean(alarmStopString);

                string modeOp = string.Empty;
                // #AUT: Automatic
                // #EX: Automatic external
                // #T1
                // #T2
                myRobotIO.GetVar("$MODE_OP", ref modeOp);

                string posAct = string.Empty;
                // "{E6POS: X 840.229, Y -840.229, Z 1794.12939, A -135.000, B 2.22039314E-12, C 79.0000, S 2, T 34, E1 0.0, E2 0.0, E3 0.0, E4 0.0, E5 0.0, E6 0.0}"
                myRobotIO.GetVar("$POS_ACT", ref posAct);
                Frame positionFrame = new Frame(posAct);
                //actualPosition als string

                string tool = string.Empty;
                //"{FRAME: X 0.0, Y 0.0, Z 244.000, A -90.0000, B 0.0, C 180.000}"
                myRobotIO.GetVar("$TOOL", ref tool);
                Frame toolFrame = new Frame(tool);

                string baseValue = string.Empty;
                // "{FRAME: X 0.0, Y 0.0, Z 0.0, A 0.0, B 0.0, C 0.0}"
                myRobotIO.GetVar("$BASE", ref baseValue);
                Frame baseFrame = new Frame(baseValue);

                //$axis_act as waarden
                string axesAct = string.Empty;
                //"{E6AXIS: A1 45.0000, A2 -119.551010, A3 106.000, A4 4.48259061E-06, A5 27.0000057, A6 -4.45310388E-06, E1 0.0, E2 0.0, E3 0.0, E4 0.0, E5 0.0, E6 0.0}"
                myRobotIO.GetVar("$axis_act", ref axesAct);
                // return string formatting from robot  is more like a frame
                Frame tempFrame = new Frame(axesAct);
                Axes axes = new Axes();
                axes.Axis1 = tempFrame.X;
                axes.Axis2 = tempFrame.Y;
                axes.Axis3 = tempFrame.Z;
                axes.Axis4 = tempFrame.A;
                axes.Axis5 = tempFrame.B;
                axes.Axis6 = tempFrame.C;

                //torque, velocity optional
                string torqueAxisAct = string.Empty;
                //Axis number Type: INT
                // 1 … 6: Robot axis A1...A6
                // 7 … 12: External axis E1 … E6
                //Motor torque Type: REAL; unit: Nm(for linear axes: N)
                // "1.77311117E-11 -1.75174166E-12 -8.43800585E-11 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 "
                myRobotIO.GetVar("$TORQUE_AXIS_ACT[]", ref torqueAxisAct);
                Axes actTorqueAxes = new Axes(torqueAxisAct);

                string torqueAxisMax = string.Empty;
                //"18986.8809 19661.3477 18551.5469 5953.74 6455.33691 4157.46533 0.0 0.0 0.0 0.0 0.0 0.0 "
                myRobotIO.GetVar("$TORQUE_AXIS_MAX[]", ref torqueAxisMax);
                Axes maxTorqueAxes = new Axes(torqueAxisMax);

                string velAct = string.Empty;
                // 0.0
                myRobotIO.GetVar("$VEL_ACT", ref velAct);

                string velAxisAct = string.Empty;
                //"0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 "
                myRobotIO.GetVar("$VEL_AXIS_ACT[]", ref velAxisAct);
                Axes actVelocityAxes = new Axes(velAxisAct);

                string velAxisMax = string.Empty;
                // "4500.00 4500.00 4500.00 4500.00 4500.00 4500.00 0.0 0.0 0.0 0.0 0.0 0.0 "
                myRobotIO.GetVar("$VEL_AXIS_MA[]", ref velAxisMax);
                Axes maxVelocityAxes = new Axes(velAxisMax);

                request.AddJsonBody(new
                {
                    proState0 = new { type = "Text", value = proState0 },
                    proState1 = new { type = "Text", value = proState1 },
                    drivesOn = new { type = "Boolean", value = drivesOn },
                    drivesOff = new { type = "Boolean", value = drivesOff },
                    peripheryReady = new { type = "Boolean", value = periReady },
                    emergencyStop = new { type = "Boolean", value = alarmStop },
                    serialNumber = new { type = "Number", value = Convert.ToInt32(serialNumber) },
                    operatingMode = new { type = "Text", value = modeOp },
                    velocity = new { type = "Number", value = Convert.ToDouble(velAct) },
                    positionX = new { type = "Number", value = positionFrame.X },
                    positionY = new { type = "Number", value = positionFrame.Y },
                    positionZ = new { type = "Number", value = positionFrame.Z },
                    positionA = new { type = "Number", value = positionFrame.A },
                    positionB = new { type = "Number", value = positionFrame.B },
                    positionC = new { type = "Number", value = positionFrame.C },
                    toolX = new { type = "Number", value = toolFrame.X },
                    toolY = new { type = "Number", value = toolFrame.Y },
                    toolZ = new { type = "Number", value = toolFrame.Z },
                    toolA = new { type = "Number", value = toolFrame.A },
                    toolB = new { type = "Number", value = toolFrame.B },
                    toolC = new { type = "Number", value = toolFrame.C },
                    baseX = new { type = "Number", value = baseFrame.X },
                    baseY = new { type = "Number", value = baseFrame.Y },
                    baseZ = new { type = "Number", value = baseFrame.Z },
                    baseA = new { type = "Number", value = baseFrame.A },
                    baseB = new { type = "Number", value = baseFrame.B },
                    baseC = new { type = "Number", value = baseFrame.C },
                    angleAxis1 = new { type = "Number", value = axes.Axis1 },
                    angleAxis2 = new { type = "Number", value = axes.Axis2 },
                    angleAxis3 = new { type = "Number", value = axes.Axis3 },
                    angleAxis4 = new { type = "Number", value = axes.Axis4 },
                    angleAxis5 = new { type = "Number", value = axes.Axis5 },
                    angleAxis6 = new { type = "Number", value = axes.Axis6 },
                    torqueAxis1 = new { type = "Number", value = actTorqueAxes.Axis1 },
                    torqueAxis2 = new { type = "Number", value = actTorqueAxes.Axis2 },
                    torqueAxis3 = new { type = "Number", value = actTorqueAxes.Axis3 },
                    torqueAxis4 = new { type = "Number", value = actTorqueAxes.Axis4 },
                    torqueAxis5 = new { type = "Number", value = actTorqueAxes.Axis5 },
                    torqueAxis6 = new { type = "Number", value = actTorqueAxes.Axis6 },
                    maxTorqueAxis1 = new { type = "Number", value = maxTorqueAxes.Axis1 },
                    maxTorqueAxis2 = new { type = "Number", value = maxTorqueAxes.Axis2 },
                    maxTorqueAxis3 = new { type = "Number", value = maxTorqueAxes.Axis3 },
                    maxTorqueAxis4 = new { type = "Number", value = maxTorqueAxes.Axis4 },
                    maxTorqueAxis5 = new { type = "Number", value = maxTorqueAxes.Axis5 },
                    maxTorqueAxis6 = new { type = "Number", value = maxTorqueAxes.Axis6 },
                    velocityAxis1 = new { type = "Number", value = actVelocityAxes.Axis1 },
                    velocityAxis2 = new { type = "Number", value = actVelocityAxes.Axis2 },
                    velocityAxis3 = new { type = "Number", value = actVelocityAxes.Axis3 },
                    velocityAxis4 = new { type = "Number", value = actVelocityAxes.Axis4 },
                    velocityAxis5 = new { type = "Number", value = actVelocityAxes.Axis5 },
                    velocityAxis6 = new { type = "Number", value = actVelocityAxes.Axis6 },
                    maxVelocityAxis1 = new { type = "Number", value = maxVelocityAxes.Axis1 },
                    maxVelocityAxis2 = new { type = "Number", value = maxVelocityAxes.Axis2 },
                    maxVelocityAxis3 = new { type = "Number", value = maxVelocityAxes.Axis3 },
                    maxVelocityAxis4 = new { type = "Number", value = maxVelocityAxes.Axis4 },
                    maxVelocityAxis5 = new { type = "Number", value = maxVelocityAxes.Axis5 },
                    maxVelocityAxis6 = new { type = "Number", value = maxVelocityAxes.Axis6 },
                });

                IRestResponse response = await m_client.ExecuteAsync(request);
                CheckFiwareResponse(response);

                timertickFinishedGeneral = true;
            }
        }

        public void ParseRobotconfig(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Fiware IO Config.dat missing " + path);
                return;
            }

            string n = "";
            int nr = 0;
            string rid = "";
            bool definitionofexternalsentered = false;

            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                string[] parts = line.Split(new char[] { ' ', ';' }, StringSplitOptions.None);
                if (parts.Count() > 2 && parts[1] == "FOLD" && parts[2].StartsWith("EXTERNAL"))
                {
                    definitionofexternalsentered = true;
                }
                if (parts.Count() == 3 && definitionofexternalsentered && parts[0] == "SIGNAL" && parts[2].StartsWith("$IN"))
                {
                    string toFind1 = "[";
                    string toFind2 = "]";
                    int start = parts[2].IndexOf(toFind1) + toFind1.Length;
                    int end = parts[2].IndexOf(toFind2, start); //Start after the index of 'my' since 'is' appears twice
                    string string2 = parts[2].Substring(start, end - start);

                    irecords.Add(new RobotInput(
                            n = serialNumber + "-" + parts[1].Trim(),
                            nr = int.Parse(string2.Trim(), NumberFormatInfo.InvariantInfo),
                            rid = serialNumber
                        ));

                }
                if (parts.Count() == 3 && definitionofexternalsentered && parts[0] == "SIGNAL" && parts[2].StartsWith("$OUT"))
                {
                    string toFind1 = "[";
                    string toFind2 = "]";
                    int start = parts[2].IndexOf(toFind1) + toFind1.Length;
                    int end = parts[2].IndexOf(toFind2, start); //Start after the index of 'my' since 'is' appears twice
                    string string2 = parts[2].Substring(start, end - start);
                    orecords.Add(new RobotOutput(
                        n = serialNumber + "-" + parts[1].Trim(),
                        nr = int.Parse(string2.Trim(), NumberFormatInfo.InvariantInfo),
                        rid = serialNumber
                    ));

                }
            }

        }

        public async void AddRobotIO(List<RobotInput> i, List<RobotOutput> o)
        {
            //Create Robot Type
            var request2 = new RestRequest("v2/entities/");
            request2.Method = Method.POST;
            request2.AddHeader("Content-Type", "application/json");
            string currentRobotId = "urn:ngsi-ld:Robot:" + serialNumber;
            request2.AddJsonBody(new
            {
                id = currentRobotId,
                type = "Robot",
                serialNumber = new { type = "Number", value = Convert.ToInt32(serialNumber) }
            });
            IRestResponse response2 = await m_client.ExecuteAsync(request2);
            CheckFiwareResponse(response2);

            foreach (RobotInput inp in i)
            {
                string currentInputId = "urn:ngsi-ld:Input:" + inp.Name;
                string timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"); // 2000-01-01T00:00:00Z
                var request = new RestRequest("v2/entities");
                request.Method = Method.POST;
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new
                {
                    id = currentInputId,
                    type = "Input",
                    inputValue = new { type = "Boolean", value = false },
                    inputNumber = new { type = "Number", value = inp.Inputnr },
                    dateCreated = new { type = "DateTime", value = timestamp },
                    robotSerialNumber = new { type = "Number", value = Convert.ToInt32(serialNumber) }
                });

                IRestResponse response = await m_client.ExecuteAsync(request);
                CheckFiwareResponse(response);

            }

            foreach (RobotOutput outp in o)
            {
                string currentOutputId = "urn:ngsi-ld:Output:" + outp.Name;
                string timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"); // 2000-01-01T00:00:00Z
                var request = new RestRequest("v2/entities");
                request.Method = Method.POST;
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new
                {
                    id = currentOutputId,
                    type = "Output",
                    outputValue = new { type = "Boolean", value = false },
                    outputNumber = new { type = "Number", value = outp.Outputnr },
                    dateCreated = new { type = "DateTime", value = timestamp },
                    robotSerialNumber = new { type = "Number", value = Convert.ToInt32(serialNumber) }
                });

                IRestResponse response = await m_client.ExecuteAsync(request);
                CheckFiwareResponse(response);
            }
        }

        public async void UpdateRobotIO(List<RobotInput> i, List<RobotOutput> o)
        {
            if (
                 myRobotIO.ConnectionStatus == eRobotConnectionState.ConnectedUDP
                 ||
                 myRobotIO.ConnectionStatus == eRobotConnectionState.ConnectedTCP
                 )
            {
                string IN = "";
                string OUT = "";
                myRobotIO.GetVar("$IN[]", ref IN);
                foreach (RobotInput inp in i)
                {
                    bool currentvalue = false;// IN[inp.inputnr];

                    if (IN[inp.Inputnr + 1] == '1') currentvalue = true;
                    var request = new RestRequest("v2/entities/urn:ngsi-ld:Input:" + inp.Name + "/attrs");
                    request.Method = Method.POST;
                    request.AddHeader("Content-Type", "application/json");
                    request.AddJsonBody(new
                    {
                        inputValue = new { type = "Boolean", value = currentvalue }
                    });
                    IRestResponse response = await m_client.ExecuteAsync(request);
                    CheckFiwareResponse(response);
                }
                myRobotIO.GetVar("$OUT[]", ref OUT);
                foreach (RobotOutput outp in o)
                {
                    bool currentvalue = false;// IN[inp.inputnr];
                    if (OUT[outp.Outputnr + 1] == '1') currentvalue = true;
                    var request = new RestRequest("v2/entities/urn:ngsi-ld:Output:" + outp.Name + "/attrs");
                    request.Method = Method.POST;
                    request.AddHeader("Content-Type", "application/json");
                    request.AddJsonBody(new
                    {
                        outputValue = new { type = "Boolean", value = currentvalue }
                    });

                    IRestResponse response = await m_client.ExecuteAsync(request);
                    CheckFiwareResponse(response);
                }
                timertickFinishedIO = true;
            }
            else if (myRobotIO.ConnectionStatus == eRobotConnectionState.Disabled || myRobotIO.ConnectionStatus == eRobotConnectionState.Error || myRobotIO.ConnectionStatus == eRobotConnectionState.Unreachable)
            {
                myRobotIO.Stop();
                myRobotIO.Start();
                timertickFinishedIO = true;
            }

        }
        #endregion
    }

    public class RobotInput
    {
        public string Name { get; set; }
        public int Inputnr { get; set; }
        public bool Value { get; set; }
        public string Robotid { get; set; }
        public RobotInput(string n, int nr, string rid)
        {
            Name = n;
            Inputnr = nr;
            Robotid = rid;
        }
    }
    public class RobotOutput
    {
        public string Name { get; set; }
        public int Outputnr { get; set; }
        public bool Value { get; set; }
        public string Robotid { get; set; }
        public RobotOutput(string n, int nr, string rid)
        {
            Name = n;
            Outputnr = nr;
            Robotid = rid;
        }
    }
}
