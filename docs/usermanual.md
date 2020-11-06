# User Guide Linesµ

In general there are two different structures where you can put data :
- pallets
- robotic cell

## Pallet
To get a good overview and to understand what is happening on the pallets, the best thing to do when starting is to clear the pallets. At this moment pallets are defined with a number from 1 to 20.
First fill out the item current position and eventually the going to field.
Then start filling the pallets by adding items to the piecelist of the pallets. In the grafana dashboard you should see the piecelist growing and that destinations have the right values.

When a pallet is filled you can move it to the shopfloor : UpdatePalletDestinationShopFloor(int palletnr) on the duocut_odfiware functions.



## Robotic cell and robot
When using the functions UpdateRoboticCellInfo() and UpdateRobotInfo() you can update the values of thes items on the OCB. Both functions exist for both robotic cells.
In the grafana dashboard you can find back the values you have put in the OCB.





## Foreseen functions
Hereby a short description of the functions available for every robotic cell.

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


