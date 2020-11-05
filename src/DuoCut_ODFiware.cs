using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;

namespace Renier_CL
{
    public class ODFiware
    {
        #region variables
        private readonly RestClient m_client;
        private readonly RestClient m_RenierServiceClient;
        private Form_main m_form;
        private bool m_destinationShopFloorAssigned = false;
        private int tickteller = 1;
        #endregion

        #region constructors
        public ODFiware(Form_main form)  
        {
            m_form = form;
            m_client = new RestClient("http://130.188.160.108:1026/");  // Orion Context Broker Server in Finland
            m_RenierServiceClient = new RestClient("http://192.168.111.12:4000/");  //FlexEdge HMI Renier Optidrive Web Service
            //m_client = new RestClient("http://127.0.0.1:1026/");
        }
        #endregion

        #region methods
        public void Tick()
        {
            
            switch (tickteller)
            {
                case 1:
                    UpdateQRM(); 
                    break;
                case 2:
                    UpdateRobotInfo();
                    break;
                case 3:
                    UpdatePalletInfo();
                    break;
                case 4:
                    UpdateErrorInfo();
                    break;
                case 5:
                    UpdateRoboticCellInfo();
                    break;
                case 6:
                    tickteller = 0;
                    break;

            }
            tickteller++;
        }

        private void UpdateRoboticCellInfo()
        {
            var request = new RestRequest("v2/entities/urn:ngsi-ld:RoboticCell:DuoCut/attrs");
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");

            string uitPaletten = "";
            int totalCount = m_form.m_uitlist.Count();
            for (int count = 0; count < totalCount; count++)
            {
                uitPaletten += "urn:ngsi-ld:Pallet:" + m_form.m_uitlist[count].palletnummer.ToString("D4");
                if ((count + 1) != totalCount)
                {
                    uitPaletten += ",";
                }
            }

            request.AddJsonBody(new
            {
                refOutgoingPallet = new
                {
                    type = "Text",
                    value = uitPaletten
                },
                refIncomingPallet = new {
                    type = "Text",
                    value = "urn:ngsi-ld:RoboticCell:BretonMachine" 
                }
            });
            //https://stackoverflow.com/questions/16898731/creating-a-json-array-in-c-sharp
            //value = m_cell.PalletList.Select(item => "urn:ngsi-ld:Pallet:" + item.ID)

            var response = m_client.Execute(request);
        }

        private void UpdateRobotInfo()
        {
            var request = new RestRequest("v2/entities/urn:ngsi-ld:Robot:DuoCutRobot/attrs");
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");

            string currentProgramName = "/R1/MAIN.SRC";
            string currentJobName =  (m_form.PeriRead[(int)Form_main.eReadState.ID]).ToString();
            int toolnr = 1;
            request.AddJsonBody(new
            {
                programName = new { type = "Text", value = currentProgramName },
                jobName = new { type = "Text", value = currentProgramName },
                toolID = new { type = "Integer", value = toolnr },
                axis1 = new { type = "Number", value = m_form.a1 },
                axis2 = new { type = "Number", value = m_form.a2 },
                axis3 = new { type = "Number", value = m_form.a3 },
                axis4 = new { type = "Number", value = m_form.a4 },
                axis5 = new { type = "Number", value = m_form.a5 },
                axis6 = new { type = "Number", value = m_form.a6 },
                status = new { type = "Text", value = m_form.lokalprostatenull },
                totalNumberOfPieces = new { type = "Integer", value = m_form.totaalAantalStukken  },
                currentPieceNumber  = new { type = "Integer", value = (m_form.totaalAantalStukken - m_form.IN.stuklist.Count) }
            });
            var response = m_client.Execute(request);
        }

        public void UpdateVacuumPumpInfo(bool isOn)
        {
            var request = new RestRequest("v2/entities/urn:ngsi-ld:VacuumPump:DuoCutPump/attrs"); 
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");

            double newVacuumLevel = 0.0;
            if (isOn)
            {
                newVacuumLevel = -0.87;
            }
            request.AddJsonBody(new
            {
                on = new { type = "Boolean", value = isOn },
                vacuumLevel = new { type = "Number", value = newVacuumLevel }
            });

            var response = m_client.Execute(request);
        }

        public void UpdateDuoCutPieceInfo(string stukID, bool stukOnrobot, double gewicht)
        {
            var request = new RestRequest("v2/entities/urn:ngsi-ld:Piece:DuoCutPiece/attrs");
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");
            
            string timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"); // 2000-01-01T00:00:00Z
            if (stukOnrobot)
            {
                request.AddJsonBody(new
                {
                    pieceID = new { type = "Text", value = stukID },
                    timeEnteredCell = new { type = "DateTime", value = timestamp },
                    weight = new { type = "Number", value = gewicht},
                    manufacturabilityOnFlexEdge = new { type = "Text", value = "CanProcess" }
                });
            } else
            {
                request.AddJsonBody(new
                {
                    pieceID = new { type = "Text", value = stukID },
                    timeLeftCell = new { type = "DateTime", value = timestamp },
                    weight = new { type = "Number", value = gewicht },
                    manufacturabilityOnFlexEdge = new { type = "Text", value = "CanProcess" }
                });

            }
            var response = m_client.Execute(request);
        }

        public void UpdateErrorInfo()
        {

            int errornr = m_form.PeriRead[(int)Form_main.eReadState.errornr];
            string errormsg = "";
            if (errornr != 0)
            {
                errormsg =  m_form.myLanguage.IniReadValue("taal1", "e" + errornr.ToString());
            }
            var request = new RestRequest("v2/entities/urn:ngsi-ld:RoboticCell:DuoCut/attrs");
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");


            request.AddJsonBody(new
            {
                errorNumber = new { type = "Integer", value = errornr },
                errorMessage = new { type = "Text", value = errormsg }
            });

            var response = m_client.Execute(request);
        }


        public void UpdatePalletInfo()
        {
        
            foreach (var pal in m_form.m_uitlist)
            {
                String pieceList = "";
                int totalCount = pal.stuklist.Count();
                for (int count = 0; count < totalCount; count++)
                {
                    pieceList += pal.stuklist[count].ordernummer;
                    if ((count + 1) != totalCount)
                    {
                        pieceList += ",";
                    }
                }

                string palletNummerString = pal.palletnummer.ToString("D4");

                if (totalCount == 1)
                {
                    string timestampTimeOfLoading = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"); // 2000-01-01T00:00:00Z

                    var request2 = new RestRequest("v2/entities/urn:ngsi-ld:Pallet:" + palletNummerString + "/attrs");
                    request2.Method = Method.POST;
                    request2.AddHeader("Content-Type", "application/json");

                    request2.AddJsonBody(new
                    {  
                        timeOfLoading = new { type = "DateTime", value = timestampTimeOfLoading }
                    });

                    var response2 = m_client.Execute(request2);

                }

               

                var request = new RestRequest("v2/entities/urn:ngsi-ld:Pallet:" + palletNummerString + "/attrs");
                request.Method = Method.POST;
                request.AddHeader("Content-Type", "application/json");


                //https://stackoverflow.com/questions/16898731/creating-a-json-array-in-c-sharp
                string timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"); // 2000-01-01T00:00:00Z

                request.AddJsonBody(new
                {
                    refPieceList = new
                    {
                        type = "Text",
                        value = pieceList
                    },
                    refCurrentLocation = new { type = "Text", value = "urn:ngsi-ld:RoboticCell:DuoCut" },
                    refGoingTo = new { type = "Text", value = "urn:ngsi-ld:RoboticCell:FlexEdge" },
                    refComingFrom = new { type = "Text", value = "urn:ngsi-ld:RoboticCell:BretonMachine" },
                    manufacturabilityOnFlexEdge = new { type = "Text", value = "CanProcess" }
                });

                var response = m_client.Execute(request);

            }
        }

        internal void ClearPalletInfo(decimal palletNumber)
        {
            String pieceList = "";
            string palletNummerString = Convert.ToInt32(palletNumber).ToString("D4");

            var request = new RestRequest("v2/entities/urn:ngsi-ld:Pallet:" + palletNummerString + "/attrs");
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody(new
            {
                refPieceList = new
                {
                    type = "Text",
                    value = pieceList
                },
                refCurrentLocation = new { type = "Text", value = "urn:ngsi-ld:RoboticCell:DuoCut" },
                refGoingTo = new { type = "Text", value = "urn:ngsi-ld:RoboticCell:FlexEdge" }
            });

            var response = m_client.Execute(request);

        }

        public void UpdateQRM()
        {
            if (m_destinationShopFloorAssigned) { 
                var request = new RestRequest("qrm");
                request.Method = Method.GET;
                var response = m_RenierServiceClient.Execute(request);
                m_destinationShopFloorAssigned = false;
            }
        }

        public void UpdatePalletDestinationShopFloor(int palletnr)
        {
            string palletNummerString = palletnr.ToString("D4");
            var request = new RestRequest("v2/entities/urn:ngsi-ld:Pallet:" + palletNummerString + "/attrs");
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                refCurrentLocation = new { type = "Text", value = "urn:ngsi-ld:RoboticCell:ShopFloor" },
                refGoingTo = new { type = "Text", value = "urn:ngsi-ld:RoboticCell:FlexEdge" },
                refComingFrom = new { type = "Text", value = "urn:ngsi-ld:RoboticCell:DuoCut" }
            });
            var response = m_client.Execute(request);
            m_destinationShopFloorAssigned = true;
        }
        #endregion
    }
}
