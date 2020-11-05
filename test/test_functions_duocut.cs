
/*These functions are implemented to be used without the hmi running. They do not need extra data from the robot of hmi but fill out one or more values on the OCB*/
/*Following sequence is possible :
ClearAllPallets()    All pallets are empty now
UpdatePalletInfo()   Data is filled in Pallet 4 and the time of loading of the pallet is set
UpdatePalletDestinationShopFloor()   The destination of pallet 4 is set on to the shopfloor
UpdateErrorInfo()    Rises error7 on the duocut robot
UpdateVacuumPumpInfo(true)    Turns vacuumpump on
UpdateVacuumPumpInfo(false)    Turns vacuumpump off
UpdateRobotInfo()      Gives the robot cell values for axes 1-6
*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;

namespace Renier_CL
{
    private readonly RestClient m_client;
    private readonly RestClient m_RenierServiceClient;
    
    public ODFiware(Form_main form)  
        {
            
            m_client = new RestClient("http://130.188.160.108:1026/");  // Orion Context Broker Server in Finland
            m_RenierServiceClient = new RestClient("http://192.168.111.12:4000/");  //FlexEdge HMI Renier Optidrive Web Service
           
        }
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

        public void ClearAllPallets()
        {
            //function which empties all piecelist on the pâllets defined on the OCB
            for (int i = 1; i <= 20; i++)
            {
                ClearPalletInfo(i);
            }
        }
        public void UpdatePalletInfo()
        {
            int palletNummer = 4;
            String pieceList = "";
            int totalCount = 2;
            for (int count = 0; count < totalCount; count++)
            {
                pieceList += piece + Convert.ToString(count);
                if ((count + 1) != totalCount)
                {
                    pieceList += ",";
                }
            }

            string palletNummerString = palletnummer.ToString("D4");

            if (totalCount > 0) //registration of time of loading for test  we update very time this function is executed
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
        public void UpdatePalletDestinationShopFloor()
        {
            int palletnr = 4;
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
        }
        public void UpdateQRM() //only if the webserver is running
        {
            m_RenierServiceClient = new RestClient("http://192.168.111.12:4000/");

            var request = new RestRequest("qrm");
            request.Method = Method.GET;
            var response = m_RenierServiceClient.Execute(request);
            m_destinationShopFloorAssigned = false;
        }
        public void UpdateErrorInfo()
        {

            int errornr = 7;
            string errormsg = "Demo Error";
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

        private void UpdateRobotInfo()
        {
            var request = new RestRequest("v2/entities/urn:ngsi-ld:Robot:DuoCutRobot/attrs");
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");

            string currentProgramName = "/R1/MAIN.SRC";
            string currentJobName = "DEMOJOB";
            int toolnr = 1;
            request.AddJsonBody(new
            {
                programName = new { type = "Text", value = currentProgramName },
                jobName = new { type = "Text", value = currentProgramName },
                toolID = new { type = "Integer", value = toolnr },
                axis1 = new { type = "Number", value = 15 },
                axis2 = new { type = "Number", value = 20 },
                axis3 = new { type = "Number", value = 30 },
                axis4 = new { type = "Number", value = 40 },
                axis5 = new { type = "Number", value = 50 },
                axis6 = new { type = "Number", value = 60 },
                status = new { type = "Text", value = "P_DEMOACTIVE" },
                totalNumberOfPieces = new { type = "Integer", value = 5 },
                currentPieceNumber = new { type = "Integer", value = 2 }
            });
            var response = m_client.Execute(request);
        }
        private void UpdateRoboticCellInfo()
        {
            int palletnummer = 5;
            var request = new RestRequest("v2/entities/urn:ngsi-ld:RoboticCell:DuoCut/attrs");
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");

            string uitPaletten = "urn: ngsi - ld:Pallet: 5";

            request.AddJsonBody(new
            {
                refOutgoingPallet = new
                {
                    type = "Text",
                    value = uitPaletten
                },
                refIncomingPallet = new
                {
                    type = "Text",
                    value = "urn:ngsi-ld:RoboticCell:BretonMachine"
                }
            });
            #endregion


        }

    }
}
