# START TESTING

[Back](README.md)

### OCB and server
First thing to do is to get the OCB running as described in the docker section.
Also cratedb and quantumleap should be running after following these steps.

### Grafana
Install grafana on the server and load the three json files uploaded tot the ramp platform.

### Factory side
For testing the setup there are two options:

#### Test at optidrive (online)
We do it together using the virtual robots at Optidrive (using remote connections and using the VTT server). For this we have to fix a date. You will have control of all data flows and can take the time you need to invest what has been done.

#### Test using example data (bash file)
To acquire example data the bash file, DataGenerator, can be run. This will load some data into the system and can be checked in the Grafana dashboard.

#### Test without virtual robot
The test is done independly from our virtual robots.
In my opinion it is too complex to make a virtual robot and its hmi working at your site. So for this option i provide some functions (written in c sharp, but convertable to json).
In the src directory i made two files :
      - test_functions_duocut
      - test_functions_flexedge
  
In these files i made functions which are independent from our hmi. By calling them you change one or more values on the ocb and you can see the reaction of the grafana dashboard.
On top of the file there is the connection to the server which we use to connect to the server.

For the Flexedge robot :

    - UpdateVacuumPumpInfo(true) : Turns the vacuumpump on and changes the vac level to -0.85
    - UpdateVacuumPumpInfo(false) : Turns the vacuumpump off and changes the vac level to -0.05
    - UpdateRobotInfo() : Displays values for the different axes from the robot and also a program name, the total number of pieces and the actual piece number
    - UpdateRoboticCellInfo(11,12) : Changes the id of the in and output pallet to 11 and 12
    - UpdateFlexEdgePieceInfo() : updates the data of the actual piece on the robot
    - UpdateErrorInfo(5) : raises errornuber 5, message is allways the same (Demo Error)
    - UpdateQRM() : Runs the qrm service if it is running on the reachable webhost
    - UpdatePalletInfo(12) : Places 5 pieces on pallet 12
    - Palletuitcell(12) : Places the actual position of pallet 12 to unload and the next position to Load


For the duocut robot :

    - ClearAllPallets()    All pallets are empty now
    - UpdatePalletInfo()   Data is filled in Pallet 4 and the time of loading of the pallet is set
    - UpdatePalletDestinationShopFloor()   The destination of pallet 4 is set on to the shopfloor
    - UpdateErrorInfo()    Rises error7 on the duocut robot
    - UpdateVacuumPumpInfo(true)    Turns vacuumpump on
    - UpdateVacuumPumpInfo(false)    Turns vacuumpump off
    - UpdateRobotInfo()      Gives the robot cell values for axes 1-6

