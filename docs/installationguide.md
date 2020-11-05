All components necessary to do this are uploaded on the RAMP platform using the upload link between Renier and Optidrive. With every uploaded component ther is a short md file about the component and its installation.

We have to install following softwares to get the demo running :

On the VTT server :  see docker file + explanation over there about setting up the OCB, cratedb and quantumleap

In grafana : install the developped dahboards (is already done at the server). The 3 json file for this are on the ramp server.

On an IPC : HMI of the two robotic cells.The Human Machine Interface software makes connection with a Kuka Robot using Kuka Connect.
It allows configuration of jobs for the robot and sents the demanded parameters to the robot. A part of the HMI is performing the connection between the Orion Context Broker and the Robot and processs data.
For installation you have to copy the delivered files into a directory of your pc.
Also install the settingsfile on the C:/Optidrive directory.
To launch the program : Renier2.exe 

Next to this you have to run two smart factury services (QRM and Manufacturability). To get this running you need to do the following :
Copy all files to a directory on your pc. Make sure there is a connection to the webserver and run the executable :
   QRM.exe
   ManufacturabilityPallet.exe palletNumber.
   ManufacturabilityPiece.exe pieceID
This executable is calling a webhost (at this moment this host is running on our local server). The code to start and generate this server is in the file server.js in the src directory

Usage: QRM.exe

Installation : Copy all files to a directory on your pc. Make sure there is a  connection to the webserver and run the executable


Two Virtual robots. Each one on a separate PC. Special software and license need to be available to get this working.



At this moment the OCB is filled with data coming from our virtual robots. However it is not possible to generate simulation data for the robot without having the virtual (or the real) robot up and running. 
If needed we can turn on the virtual robots for a while and start some programs running for some hours. During this time it is possible for you to investigate the behaviour of all the components of the demonstartion.
