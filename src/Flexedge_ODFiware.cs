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
        // new RestClient("http://127.0.0.1:1026/");
        private ODrobot100 m_robot100;
        private ODslab m_slab;
        private ODcell m_cell;
        private int nr_palletuit;
        private int nr_palletin;
        private int tickteller;
        #endregion

        #region constructors
        public ODFiware(ODrobot100 robot100, ODslab slab, ODcell cell)
        {
            m_robot100 = robot100;
            m_slab = slab;
            m_cell = cell;
            nr_palletuit = 0;
            tickteller = 1;
           
        }
        #endregion

        #region methods
        public void Tick()
        {
            switch (tickteller)
            {
                case 1:
                    UpdateVacuumPumpInfo();
                    break;
                case 2:
                    UpdateRobotInfo();
                    break;
                case 3:
                    UpdateRoboticCellInfo();
                    break;
                case 4:
                    UpdatePalletInInfo();
                    break;
                case 5:
                    UpdatePalletUitInfo();
                    break;
                case 6:
                    UpdateErrorInfo();
                    break;
                case 7:
                    tickteller = 0;
                    break;

                    //UpdatePieceInfo();
            }
            tickteller++;
        }

        private void UpdateVacuumPumpInfo()
        {
            var request = new RestRequest("v2/entities/urn:ngsi-ld:VacuumPump:FlexEdgePump/attrs", RestSharp.DataFormat.Json);
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");

            // https://stackoverflow.com/questions/43546287/how-to-create-a-complex-json-object-using-c-sharp/43546386

           
            double newVacuumLevel = m_cell.Robot100.VacuumNiveauPump; 

            request.AddJsonBody(new
            {
                on = new { type = "Boolean", value = m_robot100.Vacuum },
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
            string currentProgramName = m_robot100.robotProgramName;
            if (currentProgramName != null)
            {
                currentProgramName = currentProgramName.Replace("\"", "");
            }
            else
            {
                currentProgramName = "MAIN.SRC";
            }
            request.AddJsonBody(new
            {
                programName = new { type = "Text", value = currentProgramName },
                toolID = new { type = "Integer", value = m_robot100.ActiveToolNr},
                axis1 = new { type = "Number", value = m_robot100.AS1 },
                axis2 = new { type = "Number", value = m_robot100.AS2 },
                axis3 = new { type = "Number", value = m_robot100.AS3 },
                axis4 = new { type = "Number", value = m_robot100.AS4 },
                axis5 = new { type = "Number", value = m_robot100.AS5 },
                axis6 = new { type = "Number", value = m_robot100.AS6 },
                status = new { type = "Text", value = m_robot100.Prostatenull },
                totalNumberOfPieces = new { type = "Integer", value = m_cell.PalletInTotaal }, 
                currentPieceNumber = new { type = "Integer", value = (m_cell.PalletInTotaal - m_cell.SLAB.Stuklist.Count) }
            });
            var response = m_client.Execute(request);
        }

        
        private void UpdateRoboticCellInfo()
        {
            if ((m_cell.SLAB.ManualBarcode != "") && (m_cell.SLAB.ManualBarcodePalletOut != ""))
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
                        value = "urn:ngsi-ld:Pallet:" + Convert.ToInt32(m_cell.SLAB.ManualBarcode).ToString("D4")
                    },
                    refOutgoingPallet = new
                    {
                        type = "Text",
                        value = "urn:ngsi-ld:Pallet:" + Convert.ToInt32(m_cell.SLAB.ManualBarcodePalletOut).ToString("D4")
                    }
                });
                //https://stackoverflow.com/questions/16898731/creating-a-json-array-in-c-sharp
                //value = m_cell.PalletList.Select(item => "urn:ngsi-ld:Pallet:" + item.ID)



                var response = m_client.Execute(request);
            }
        }

        //private void UpdatePieceInfo()
        //{
        //    var request = new RestRequest("v2/entities/urn:ngsi-ld:Piece:FlexEdgePiece/attrs", RestSharp.DataFormat.Json);
        //    request.Method = Method.POST;
        //    request.AddHeader("Content-Type", "application/json");

        //    string currentPieceID = String.Empty;
        //    double currentLength = 0;
        //    double currentWidth = 0;
        //    double currentHeight = 0;
        //    if (m_slab.Sendstuk != null)
        //    {
        //        currentPieceID = m_slab.Sendstuk.ID.ToString();
        //        //currentLength = m_slab.Sendstuk.Length;
        //        //currentWidth = m_slab.Sendstuk.Width;
        //        //currentHeight = m_slab.Sendstuk.Height;
        //    }

        //    request.AddJsonBody(new
        //    {
        //        pieceID = new { type = "Text", value = currentPieceID },
        //    });

        //    //length = new { type = "Number", value = currentLength },
        //    //    width = new { type = "Number", value = currentWidth },
        //    //    height = new { type = "Number", value = currentHeight }


        //    var response = m_client.Execute(request);
        //}

        public void UpdateFlexEdgePieceInfo(string stukID, bool stukOnrobot, double gewicht)
        {
            var request = new RestRequest("v2/entities/urn:ngsi-ld:Piece:FlexEdgePiece/attrs");
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");

            string timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"); // 2000-01-01T00:00:00Z
            if (stukOnrobot)
            {
                request.AddJsonBody(new
                {
                    pieceID = new { type = "Text", value = stukID },
                    timeEnteredCell = new { type = "DateTime", value = timestamp },
                    weight = new { type = "Number", value = gewicht },
                    manufacturabilityOnFlexEdge = new { type = "Text", value = "CanProcess" }
                });
            }
            else
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
            int errornr = m_cell.Robot100.PeriRead1[(int)eRS1.errornr];
            string errormsg = "";
            if (errornr != 0)
            {
                errormsg = P0296_HMI.Properties.Resources.ResourceManager.GetString("e" + errornr);
            }
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

        public static async void UpdateErrorInfo(int errornr, string errormsg)
        {
            var request = new RestRequest("v2/entities/urn:ngsi-ld:RoboticCell:FlexEdge/attrs");
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");

            
            request.AddJsonBody(new
            {
                errorNumber = new { type = "Integer", value = errornr },
                errorMessage = new { type = "Text", value = errormsg }
            });

            IRestResponse response = null;
            await Task.Run(() =>
              response = m_client.Execute(request)
            );
            //Console.WriteLine(response);
            
        }


        public void UpdateQRM()
        {
                var request = new RestRequest("qrm");
                request.Method = Method.GET;
                var response = m_RenierServiceClient.Execute(request);
        }

        public void UpdatePalletInInfo()
        {
            if ((m_cell.SLAB.ManualBarcode != "") )
            {
                UpdatePalletInfo(Convert.ToInt32(m_cell.SLAB.ManualBarcode)); //m_cell.Periferie.Rollen.Rol2_PalletNr); //pallet In nr
                if (nr_palletin == 0) { nr_palletin = Convert.ToInt32(m_cell.SLAB.ManualBarcode); }

                if (nr_palletin != Convert.ToInt32(m_cell.SLAB.ManualBarcode))
                {
                    UpdateQRM();
                }
            }
        }

        public void UpdatePalletUitInfo()
        {
            if ( (m_cell.SLAB.ManualBarcodePalletOut != ""))
            {
                
                UpdatePalletInfo(Convert.ToInt32(m_cell.SLAB.ManualBarcodePalletOut)); // m_cell.Periferie.Rollen.Rol3_PalletNr); // pallet out nr

                if (nr_palletuit == 0) { nr_palletuit = Convert.ToInt32(m_cell.SLAB.ManualBarcodePalletOut); }

                if (nr_palletuit != Convert.ToInt32(m_cell.SLAB.ManualBarcodePalletOut))
                {
                    Palletuitcell(Convert.ToInt32(nr_palletuit)); // m_cell.Periferie.Rollen.Rol3_PalletNr); // pallet out nr
                }
            }
        }

        public void UpdatePalletInfo(int palletNr)
        {
            if (palletNr != -1)
            {
                String pieceList = "";
                ObservableCollection<P0296stuk> stukList = CreateStukList(palletNr.ToString());

                int totalCount = stukList.Count();
                for (int count = 0; count < totalCount; count++)
                {
                    pieceList += stukList[count].ID;
                    if ((count + 1) != totalCount)
                    {
                        pieceList += ",";
                    }
                }

                string palletNummerString = palletNr.ToString("D4");

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

        private ObservableCollection<P0296stuk> CreateStukList(string palletNr)
        {
            ObservableCollection<P0296stuk> stuklist = new ObservableCollection<P0296stuk>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ODdatabase.connectionstring))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.CommandText = "SELECT Id, PalletID, PieceID, Length, Width, Height, Merk, Klant, PalletPos, ODslabID,TimeAdded FROM T_Pallets WHERE PalletID = @PalletId ORDER BY PalletPos";
                    cmd.Parameters.AddWithValue("@palletid", palletNr);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;

                    reader = cmd.ExecuteReader();

                    //List<string> returner = new List<string>();
                    List<List<string>> returner = new List<List<string>>();
                    while (reader.Read())
                    {
                        returner.Add(new List<string>());
                        for (int i = 0; i < reader.FieldCount; ++i)
                        {
                            returner[returner.Count - 1].Add(reader[i].ToString());
                        }
                        P0296stuk stuk = new P0296stuk(returner[returner.Count - 1]);
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            stuklist.Add(stuk);
                        });
                    }
                }
            }
            catch (Exception ex)
            {
               
            }

            return stuklist;

        }

        #endregion


    }
}
