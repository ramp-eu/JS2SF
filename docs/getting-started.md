# GETTING STARTED

[Back](README.md)

This page is a guide to test out the component in a quick way. It uses a preconfigured docker-compose file, a grafana dashboard and a preconfigured robotsimulator combined with the RoseAP.



Use a predefined set of i/o, use a config.dat file which is preconfigured for this.





In order to run the preconfigured test with the robotsimulator, you need to follow the following steps :

## Installation



We have to install following softwares to get the demo/test environment  running :

Install cygwin tools on windows from https://www.cygwin.com/, accept all default suggestions during installation.

After installation of Cygwin, add the dos2unix command to cygwin by executing in command prompt with access to the cygwin installer:

setup-x86_64.exe -q -P dos2unix

Install Docker Desktop on Windows from https://docs.docker.com/docker-for-windows/install/, choose a Hyper-V backend.

### OCBServer using docker-compose.yml

Start a cygwin terminal and go to the folder containing the docker-compose.yml file, for example:
```
cd /cygdrive/c/Fiware/RoseAP/
```
Make sure the script files have Linux line endings by executing :
```
dos2unix DataGeneratorRoseAP
dos2unix services
```
To get the Orion Context Broker running a service script is provided to easily create, start and stop the containers. 

To obtain the necessary Docker images locally use (this needs to be executed only once):
```
./services create
```
To initialise and startup the Fiware Orion Context Broker containers use:
```
./services start
```
You can test if the Orion Context Broker is running with:
```
curl localhost:1026/version
```
If the installation went well you'll get some Orion version information
Also Cratedb, Quantumleap and Grafana should be running by now. 


### RoseAP Configuration

- Configure the configuration file of the roseap (change the ip adres to  ???? )

Copy the RoseAPSettings.xml file to the folder "C:\Optidrive\HMISETTINGS\P0296_ROSE-AP\" and edit the file:

ContextBrokerUrl: replace the IP address and port to the location of your Context Broker running in Docker, for example http://192.168.16.40:1026/

LocalAddress: Set it to the IP address of the current PC.

RemoteAddress: Set it to the IP address of the current PC for the robot simulator, or use the IP address of your Kuka Robot.


### Test with the limited robot simulator (live test)

- Play with the data and  look at the behaviour of the dashboard.   By changing the values in the Robot Simulator, you can see the changing values in the dashboards.

- Launch the RoseAP, Launch the Robotsimulator


### Old

For this we created an docker compose file which is taking care of all components.

- Follow the installation guide to install an Orion Context Broker on your pc. Use this docker compose file.  [Install OCB]( https://hub.docker.com/r/fiware/orion/)

  This will install all necessary components on your pc. The OCB is accessible 

### Grafana

- If you used the provided docker-compose file , Grafana will already be installed on your system in is accessible at the following adress.
- In grafana : install the developed dashboards . The json file for this are available in the project.







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


​    
​    

### Recommendation to start your tests
In the examples above you can see the variables of the robot which are stored on the Orion Context Broker. When using the function  UpdateRobotInfo() you can update the values of the items on the OCB.
In the grafana dashboard you can find back the values you have put in the OCB.

###  Stop the Orion Context Broker

To stop the containers (data will be preserved) use :
```
./services stop
```
To clean up all data and images use (note that this will also remove your Grafana configuration) :
```
docker-compose --log-level ERROR -p fiware down -v --remove-orphans
```
