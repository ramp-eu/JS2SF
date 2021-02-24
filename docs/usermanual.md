# User Guide Lines

[Back](README.md)

In general there are three different structures where you can put data :
- pieces
- pallets
- robotic cell

## Grafana
The first time you connect to grafana dashboard you will have to specify a user and pasword. At this moment we use 
       *user : admin and pasword :admin
       
You have to click once on the skip button before you see the grafana interface. 

For selecting another dashboard search for the manage button on the right side and you will see a list of available dashboards

## Piece
A piece in the OCB contains following data :
 	  * Piece ID (barcode indicating piece type, not unique)
	  * Creation date
	  * Manufacturability of the piece
	  * Process time estimation
	  * Weight
	  * Sequence number of position on Pallet
	  * Location (pallet ID, robotic cell ID )
A piece is created when it is added to a pallet by the DuoCut robot.
When emptying a pallet all pieces that were on the pallet are deleted.

## Pallet
 The pallet data in the OCB contains following data :
 
      * ID (number)
      * Time of loading
      * Currentlocation
      * NextLocation
      * Manufacturability of the pallet
      * Priority of the pallet
	  * Status (loading, unloading, empty, filled)
When loading/unloading the pallet the piece list is updated. 
Once the pallet is complete a Smart Factory Service is running and calculates if all pieces are suitable for the next production step. This value is stored in the manufacturability.
With the current location field it becomes easy to retrieve the pallet in the factory. The field next destination indicates where it has to go in the next production step. Both  fields are updated when a pallet is entering or leaving a robotic cell.
After every manipulation of a pallet the QRM logic is updating the priority of all the pallets which are on the shop floor.

Following dashboards are available for pallets :        
* Pallet_Overview 
* Pallet_Operation 

On the top side of the dashboard you can select the id of the pallet you want to see.

For the handheld device : create a qr code from the url to the Pallet dashboard_operator. It can be scanned with a smartphone or tablet.

## Robotic cell and robot

The robots are continously updating their states to the context broker. Main data which is transferred are :

      * Status of the robot (active, stopped ....)
      * Errornr + errormessages
      * Axis values (A1-A6)
      * Status + level of the vacuum
      * Number of pieces done and number of pieces to do
      * List of pallets present in the cell (in or outgoing pallet)
      
We made a dashboard for the robot and one for the cell :  Robot and Roboticcell: 

On the top of the dashboard you can select which robot or cell you want to be displayed in the dashboard.               
                 

## Foreseen functions
Hereby a short description of the functions available for every robotic cell. These functions are integrated in our HMI. I put the source code in the src directory of this github.

#### Functions in DuoCut_ODfiware.cs

- private void UpdateRoboticCellInfo() : updates the data of the vacuumpump, data is known as global variables
- private void UpdateRobotInfo() : updates the data of the Robot, data is known as global variables
- public void UpdateVacuumPumpInfo(bool isOn) : updates the on state of the vacuumpump, data is known as global variables
- public void UpdatePieceInfo(string stukID, string barcode, int palletPositie, int palletnummer) : Updates piece info of the piece which is now on the robot
- public void UpdateErrorInfo() : update errornr + message
- public void UpdatePalletDestinationDuoCut(int palletnr) : Pallet filling on DuoCut Robot
- public void UpdatePalletInfo() : Update the info of all the pallets which are present in the cell. All data are globals
- internal void ClearPalletInfo(decimal palletNumber) : Removes pieces belonging to the pallet specified by a number
- public void UpdateQRM() : runnning the qrm service
- public void UpdatePalletDestinationShopFloor(int palletnr) : Place a pallet from duocut to shopfloor
- public void AddPiece(stuk currentPiece) : Create piece located on DuoCut robot
- public void UpdatePieceManufacturabilityInfo(string pieceId, string barcode, int palletnr) : Update Manufacturability
- public void ParseRobotconfig(string path) : parse robot config.dat for input and output variables
- public async void AddRobotIO(List<RobotInput> i, List<RobotOutput> o) : Create the Inputs and outputs on the context broker.
- public async void UpdateRobotIO(List<RobotInput> i, List<RobotOutput> o) : Update the Inputs and outputs on the context broker.

#### Functions in Flexedge_ODfiware.cs

- private void UpdateVacuumPumpInfo() : updates the data of the vacuumpump, data is known as global variables
- private void UpdateRobotInfo() : updates the data of the Robot, data is known as global variables
- private void UpdateRoboticCellInfo() : updates the data of the Robotic cell, data is known as global variables
- public void UpdateFlexEdgePieceInfo(string stukID, bool stukOnrobot, double gewicht)
- public void UpdateErrorInfo() : update errornr
- public void UpdateQRM() : runnning the qrm service
- public void UpdatePalletInInfo() : changing the incoming pallet from the robot cell
- public void UpdatePalletUitInfo() : changing the outgoing pallet from the robot cell
- public void UpdatePalletIn(int palletNr) : updates all info of the incoming pallets specified by palletNr
- public void UpdatePalletUit(int palletNr) : updates all info of the outgoing pallets specified by palletNr 
- public void Palletuitcell(int palletNr) : updates all info of pallet leaving the robotic cell specified by palletNr
- public void ParseRobotconfig(string path) : parse robot config.dat for input and output variables
- public async void AddRobotIO(List<RobotInput> i, List<RobotOutput> o) : Create the Inputs and outputs on the context broker.
- public async void UpdateRobotIO(List<RobotInput> i, List<RobotOutput> o) : Update the Inputs and outputs on the context broker.


