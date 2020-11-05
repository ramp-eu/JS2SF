/*Following examples can be performed: 
 * 
 * UpdateVacuumPumpInfo(true) : Turns the vacuumpump on and changes the vac level to -0.85
 * UpdateVacuumPumpInfo(false) : Turns the vacuumpump off and changes the vac level to -0.05
 * UpdateRobotInfo() : Displays values for the different axes from the robot and also a program name, the total number of pieces and the actual piece number
 * UpdateRoboticCellInfo(11,12) : Changes the id of the in and output pallet to 11 and 12
 * UpdateFlexEdgePieceInfo() : updates the data of the actual piece on the robot
 * UpdateErrorInfo(5) : raises errornuber 5, message is allways the same (Demo Error)
 * UpdateQRM() : Runs the qrm service if it is running on the reachable webhost
 * UpdatePalletInfo(12) : Places 5 pieces on pallet 12
 * Palletuitcell(12) : Places the actual position of pallet 12 to unload and the next position to Load
 * 
 */



using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serialization;

namespace P0296_HMI
{
         
    public class ODFiware
    {
        #region variables
        private static readonly RestClient m_client = new RestClient("http://130.188.160.108:1026/");  // Orion Context Broker Server in Finland
        private static readonly RestClient m_RenierServiceClient = new RestClient("http://192.168.111.12:4000/");  // FlexEdge HMI Renier Optidrive Web Service
       
        #endregion

        #region constructors
        public ODFiware()
        {
           
        }
        #endregion

        #region methods
        
        private void UpdateVacuumPumpInfo(bool on)
        {
            var request = new RestRequest("v2/entities/urn:ngsi-ld:VacuumPump:FlexEdgePump/attrs", RestSharp.DataFormat.Json);
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");

            // https://stackoverflow.com/questions/43546287/how-to-create-a-complex-json-object-using-c-sharp/43546386

           
            double newVacuumLevel = -0.85; 
            if (!on) { newVacuumLevel = -0.05; }
            request.AddJsonBody(new
            {
                on = new { type = "Boolean", value = on },
                vacuumLevel = new { type = "Number", value = newVacuumLevel }
            });
            
            var response = m_client.Execute(request);
        }

        private void UpdateRobotInfo()
        {
            var request = new RestRequest("v2/entities/urn:ngsi-ld:Robot:FlexEdgeRobot/attrs", RestSharp.DataFormat.Json);
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");

            //m_robot100.robotProgramName = "/R1/MAIN"; remove quotes
            string currentProgramName = "DEMO Program";
            
            request.AddJsonBody(new
            {
                programName = new { type = "Text", value = currentProgramName },
                toolID = new { type = "Integer", value = 2},
                axis1 = new { type = "Number", value = 5 },
                axis2 = new { type = "Number", value = 15 },
                axis3 = new { type = "Number", value = 20 },
                axis4 = new { type = "Number", value = 25 },
                axis5 = new { type = "Number", value = 30},
                axis6 = new { type = "Number", value = 35 },
                status = new { type = "Text", value = "P_Demo" },
                totalNumberOfPieces = new { type = "Integer", value = 8}, 
                currentPieceNumber = new { type = "Integer", value = 4 }
            });
            var response = m_client.Execute(request);
        }

        
        private void UpdateRoboticCellInfo(int Barcode ,int  BarcodePalletOut)
        {
          
                var request = new RestRequest("v2/entities/urn:ngsi-ld:RoboticCell:FlexEdge/attrs", RestSharp.DataFormat.Json);
                request.Method = Method.POST;
                request.AddHeader("Content-Type", "application/json");

                //int randomPalletNr = new Random().Next(1, 29);

                // m_cell.Periferie.Rollen.Rol2_PalletNr.ToString("D4")
                //m_cell.Periferie.Rollen.Rol3_PalletNr.ToString("D4")

                request.AddJsonBody(new
                {
                    refIncomingPallet = new
                    {
                        type = "Text",
                        value = "urn:ngsi-ld:Pallet:" + Convert.ToInt32(Barcode).ToString("D4")
                    },
                    refOutgoingPallet = new
                    {
                        type = "Text",
                        value = "urn:ngsi-ld:Pallet:" + Convert.ToInt32(BarcodePalletOut).ToString("D4")
                    }
                });
                //https://stackoverflow.com/questions/16898731/creating-a-json-array-in-c-sharp
                //value = m_cell.PalletList.Select(item => "urn:ngsi-ld:Pallet:" + item.ID)



                var response = m_client.Execute(request);
           
        }

      

        public void UpdateFlexEdgePieceInfo()
        {
            var request = new RestRequest("v2/entities/urn:ngsi-ld:Piece:FlexEdgePiece/attrs");
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");

            string timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"); // 2000-01-01T00:00:00Z
            
                request.AddJsonBody(new
                {
                    pieceID = new { type = "Text", value = DEMOPIECE },
                    timeEnteredCell = new { type = "DateTime", value = timestamp },
                    weight = new { type = "Number", value = 5 },
                    manufacturabilityOnFlexEdge = new { type = "Text", value = "CanProcess" }
                });
            
            var response = m_client.Execute(request);
        }


        public void UpdateErrorInfo(int errornr)
        {
            
            string errormsg = "DEMO ERROR";
            
            var request = new RestRequest("v2/entities/urn:ngsi-ld:RoboticCell:FlexEdge/attrs");
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");


            request.AddJsonBody(new
            {
                errorNumber = new { type = "Integer", value = errornr },
                errorMessage = new { type = "Text", value = errormsg }
            });

            var response = m_client.Execute(request);
        }

        

        public void UpdateQRM()
        {
                var request = new RestRequest("qrm");
                request.Method = Method.GET;
                var response = m_RenierServiceClient.Execute(request);
        }

        
        public void UpdatePalletInfo(int palletNr)
        {
            if (palletNr != -1)
            {
                String pieceList = "";
               
                int totalCount = 5;
                for (int count = 0; count < totalCount; count++)
                {
                    pieceList += "Demopiece"+Convert.Tostring(count);
                    if ((count + 1) != totalCount)
                    {
                        pieceList += ",";
                    }
                }

                string palletNummerString = Convert.ToString(palletNr);

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
                    refCurrentLocation = new { type = "Text", value = "urn:ngsi-ld:RoboticCell:FlexEdge" },
                    refGoingTo = new { type = "Text", value = "urn:ngsi-ld:RoboticCell:Unload" },
                    manufacturabilityOnFlexEdge = new { type = "Text", value = "CanProcess" }
                });

                var response = m_client.Execute(request);

            }
        }
        public void Palletuitcell(int palletNr)
        {
            if (palletNr >0)
            {
                string palletNummerString = palletNr.ToString("D4");

                var request = new RestRequest("v2/entities/urn:ngsi-ld:Pallet:" + palletNummerString + "/attrs");
                request.Method = Method.POST;
                request.AddHeader("Content-Type", "application/json");

                string timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"); // 2000-01-01T00:00:00Z

                request.AddJsonBody(new
                {
                    refCurrentLocation = new { type = "Text", value = "urn:ngsi-ld:RoboticCell:Unload" },
                    refGoingTo = new { type = "Text", value = "urn:ngsi-ld:RoboticCell:Load" }
            
                });

                var response = m_client.Execute(request);

            }
        }


        #endregion


    }
}
