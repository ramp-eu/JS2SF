# START TESTING

[Back](README.md)

### OCB and server
First thing to do is to get the OCB running as described in the docker section.
Also cratedb and quantumleap should be running after following these steps.

### Grafana
Install grafana on the server and load the json files uploaded tot the ramp platform.


### Factory side
For testing the setup there are two options:

#### Test with the limited robot simulator (live test)
   Run the limited Robot Simulator

​	Run the Rose AP

​    By changing the values in the Robot Simulator, you can see the changing values in the dashboards.

#### Test using example data (bash file)
To acquire example data the bash file, DataGenerator, can be run. This will load some data into the system and can be checked in the Grafana dashboard.

#### Test without virtual robot
The test is done independently from our virtual robots.
So for this option i provide some functions (written in c sharp, but convertible to json).
In the src directory you can find all the functions available for testing.:
These functions are independent from our hmi. By calling them you change one or more values on the ocb and you can see the reaction of the grafana dashboard.

Available functions :

    -  UpdateRobotInfo() : Displays values for the different axes from the robot and also a program name, the total number of pieces and the actual piece number
    - UpdateErrorInfo(5) : raises errornuber 5, message is allways the same (Demo Error)

### Recommendation to start your tests
When using the function  UpdateRobotInfo() you can update the values of the items on the OCB.
In the grafana dashboard you can find back the values you have put in the OCB.

