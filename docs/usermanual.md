# User Guide Lines

[Back](README.md)

In general there are two different structures where you can put data :
- pallets
- robotic cell

## Grafana
The first time you connect to grafana dashboard you will have to specify a user and pasword. At this moment we use 
       *user : admin and pasword :admin
       
You have to click once on the skip button before you see the grafana interface. 

For selecting another dashboard search for the manage button on the right side and you will see a list of available dashboards

## Pallet
The pallet data in the OCB contains following data :
      - ID (number)
      - list of pieces
      - Currentlocation
      - NextLocation
When loading/unloading the pallet the piece list is updated. 
With the current location field it becomes easy to retrieve the pallet in the factory. The field next destination indicates where it has to go in the next production step. Both fields are updated when a pallet is entering or leaving a robotic cell.

Following dashboards are available for pallets :        
* Pallet_Dashboard_Manager 
* Pallet_Dashboard_Operator 

On the top side of the dashboard you can select the id of the pallet you want to see.

For the handheld device : create a qr code from the url to the Pallet dashboard_operator. It can be scanned with a smartphone or tablet.

## Robotic cell and robot


We made one dashboard for the robot cell :   Roboticcell_Dashboard_final: 

On the top of the dashboard you can select which cell you want to be dispalyed in the dashboard.
                      
                 

## Foreseen functions
Hereby a short description of the functions available for every robotic cell. These functions are integrated in our HMI. I put the source code in the src directory of this github.

#### Functions in DuoCut_ODfiware.cs

- private void UpdateRoboticCellInfo() : updates the data of the vacuumpump, data is known as global variables
- private void UpdateRobotInfo() : updates the data of the Robot, data is known as global variables
- private void UpdateVacuumPumpInfo() : updates the data of the vacuumpump, data is known as global variables
- public void UpdateVacuumPumpInfo(bool isOn) : updates the  on state of the vacuumpump
- public void UpdateDuoCutPieceInfo(string stukID, bool stukOnrobot, double gewicht) : Updates piece infor of the piece which is now on the robot
- public void UpdateErrorInfo() : update errornr + message
- public void UpdatePalletInfo() : Update the info of all the pallets which are present in the cell. All data are globals
- internal void ClearPalletInfo(decimal palletNumber): Clears the piecelist of the pallet specified by a number
- public void UpdateQRM() : runnning the qrm service
- public void UpdatePalletDestinationShopFloor(int palletnr) : Place a pallet from duocut to shopfloor
       
 

#### Functions in Flexedge_ODfiware.cs

- private void UpdateVacuumPumpInfo() : updates the data of the vacuumpump, data is known as global variables
- private void UpdateRobotInfo() : updates the data of the Robot, data is known as global variables
- private void UpdateRoboticCellInfo() : updates the data of the Robotic cell, data is known as global variables
- public void UpdateFlexEdgePieceInfo(string stukID, bool stukOnrobot, double gewicht)
- public void UpdateErrorInfo() : update errornr
- public static async void UpdateErrorInfo(int errornr, string errormsg) : update error information
- public void UpdateQRM() : runnning the qrm service
- public void UpdatePalletInInfo() : changing the incoming pallet from the robot cell
- public void UpdatePalletUitInfo() : changing the outgoing pallet from the robot cell
- public void UpdatePalletInfo(int palletNr) : updates all info of the incoming pallets specified by palletNr
- public void Palletuitcell(int palletNr) : updates all info of the outgoing pallets specified by palletNr


