# Installation guidelines

All components used in midterm the demonstartion are uploaded on the RAMP platform using the upload link between Renier and Optidrive. With every uploaded component there is a short md file about the component and its installation.

We have to install following softwares to get the demo running :

- On the VTT server :  see docker file + explanation over there about setting up the OCB, cratedb and quantumleap

- In grafana : install the developped dahboards (is already done at the server). The 3 json file for this are uploaded on the ramp platform.

- On an IPC : HMI of the two robotic cells.The Human Machine Interface software makes connection with a Kuka Robot using Kuka Connect.
   * It allows configuration of jobs for the robot and sents the demanded parameters to the robot. A part of the HMI is performing the connection between the Orion Context Broker and the Robot and processs data.
   * For installation you have to copy the delivered files into a directory of your pc.
   * Also install the settingsfile on the C:/Optidrive directory.
   * To launch the program : Renier2.exe 

- Next to this you have to run two smart factury services (QRM and Manufacturability). To get this running you need to do the following :
  * Copy all files to a directory on your pc. 
  * Make sure there is a connection to the webserver and you will be able to run the executables :
      ** QRM.exe
      ** ManufacturabilityPallet.exe palletNumber.
      ** ManufacturabilityPiece.exe pieceID
      
  * These executables are called by a webhost (at this moment this host is running on our local server). The code to start and generate this server is in the filse server.js and index.html in the src directory. You will have to adapt paths to the right places to get this working.

  * Usage example : QRM.exe or http://192.168.111.12:4000/qrm in case the webserver is launched. This allows us to call this service from everywhere in our internal network.


- Two Virtual robots. Each one on a separate PC. Special software and license need to be available to get this working.

# Source code : 
In the source directory you can find a file duocut_odfiware and flexedge_odfiware. These are the source code of our two classes (one for each robotic cell) taking care of the data updates on the OCB. This code is written in C sharp and should make it possible to write data to the context broker. If doing so please let us know if we have to disconnect our virtual robot form the OCB otherwise data will be mixed by the the two systemes and results can be corrupted.

# OCB on the VTT server 
At this moment the OCB is filled with data coming from our virtual robots. However it is not possible to generate simulation data for the robot without having the virtual (or the real) robot up and running. 
If needed we can turn on the virtual robots for a while and start some programs running for some hours. During this time it is possible for you to investigate the behaviour of all the components of the demonstartion.
