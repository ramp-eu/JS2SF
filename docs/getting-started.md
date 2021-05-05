# GETTING STARTED

[Back](README.md)

This page is a guide to test out the component in a quick way. It uses a preconfigured docker-compose file, a grafana dashboard and a preconfigured robotsimulator combined with the RoseAP.



Use a predefined set of i/o, use a config.dat file which is preconfigured for this.





In order to run the preconfigured test with the robotsimulator, you need to follow the following steps :

## Installation



We have to install following softwares to get the demo/test environment  running :

### OCBServer

First thing to do is to get the Orion Context Broker running. Also cratedb and quantumleap should be running after following these steps. For this we created an docker compose file which is taking care of all components.

- Follow the installation guide to install an Orion Context Broker on your pc. Use this docker compose file.  [Install OCB]( https://hub.docker.com/r/fiware/orion/)

  This will install all necessary components on your pc. The OCB is accessible 

### Grafana

- If you used the provided docker-compose file , Grafana will already be installed on your system in is accessible at the following adress.
- In grafana : install the developed dashboards . The json file for this are available in the project.



### RoseAP

- Configure the configuration file of the roseap (change the ip adres to  ???? )

- Launch the RoseAP, Launch the Robotsimulator



### Test with the limited robot simulator (live test)

- Play with the data and  look at the behaviour of the dashboard.   By changing the values in the Robot Simulator, you can see the changing values in the dashboards.




### Test without the robot simulator
For testing the setup there are two options:

##### Test using example data (bash file)
To acquire example data the bash file, DataGenerator, can be run. This will load some data into the system and can be checked in the Grafana dashboard. We provided two different bash files to show different data flows.

##### Test with c# functions
The test is done independently from the robotsimulator.
So for this option there are some functions (written in c sharp, but convertible to json) available for testing (here)

By calling these functions you can change one or more values on the ocb and you can see the reaction of the grafana dashboard. Or you can ask the actual value of some variables on the OCB.

Available functions :

    -  UpdateRobotInfo() : Displays values for the different axes from the robot and also a program name, the total number of pieces and the actual piece number
    - UpdateErrorInfo(5) : raises errornuber 5, message is allways the same (Demo Error)
    
    List of separate functions for every 
    
    
    

### Recommendation to start your tests
In the examples above you can see the variables of the robot which are stored on the Orion Context Broker. When using the function  UpdateRobotInfo() you can update the values of the items on the OCB.
In the grafana dashboard you can find back the values you have put in the OCB.

