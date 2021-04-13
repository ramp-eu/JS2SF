Introduction

KUKA.Connect KRC provides a socket interface that other pc’s on the network can use to read or write variables in the robot controller.

 

TEKENING

 

The socket interface can communicate with UDP and TCP protocol. 

| At all times, fixed IP4 addresses are to be used for the server  level on the robot side as for the client side (external pc). |
| ------------------------------------------------------------ |
| Both the robot as the external pc must be placed in the same  network range and use the same network mask. It is advised to use a dedicated  network for the communication. |

 

 

Kuka Connect : Installation

\- Put the received software on an usb stick, but not the zip files, the unpacked files (take on folder per software) 

\- Make sure to be in T1 level 

\- Go to expert level on the robot controller <robot><configuration><user group> 

\- Install additional software <startup><additional software> 

o <New software> button 

o <Configure> button 

o <Path Selection> button 

o Select path on usb drive 

o <Save button> 

\- Select software 

o Select button <Install software> 

o Reboot controller is started 

 

. 

After restarting the controller, the license key of KUKA.Connect KRC must be entered to activate all its functionality. See next Chapter : License.

 

Kuka Connect : License

All the information needed to order a license can be filled in on the SmartPad. 

Select MENU BUTTON > Start-Up > Licenses > CONNECT.. 

 In the bottom section of the screen there is a request license button. When you press this button you get the license request screen in which all the necessary data can be entered in order to request a license. 

Then by pressing the save button a file is written to 

c:\KRC\Roboter\Log\LicRequest.txt.

This file contains all the data needed to generate the license key and it can be attached to the official order of the product. 

After receiving a license it can be entered on the Smartpad. Select MENU BUTTON > Start-Up > Licenses > CONNECT. The license key can be entered in the text box. Then press the “Validate” button. If the license key is correct then the textbox and validate button will disappear and instead it will show a “Valid license” message.

 

Kuka Connect :  Network settings 

 

At all times fixed IP addresses should be used. In addition, the TCP-IP port or UDP port must be made available in the KLI interface. This is done over (look for the KUKA documentation on defining networks) 

Cancel all programs 

Set robot in T1 modus 

Home > Configuration > User Group 

 Administrator 

 

Home > Start-up > Network Configuration 

 Address Type : Fixed IP 

 Advanced 

o NAT 

o Permitted protcoll : 9901 tcp 

 

Note 

 Network mask must be same as for client computer 

 Client computer also must have fixed IP address 

 

Kuka Connect : Configuration

Configuration of the socket interface can be done through the socket configuration window. 

Select “Main menu” à “Configuration” à “Connect Settings”. 

Note that this option is only accessible in user mode expert or higher.

 

1) Local IP Address = the ip-address of the robot. 

2) Local UDP Port = the UDP port on which the robot is listening. If the first entry called “disabled” is selected then the robot is not listening to on any UPD port. As a default often 9902 is selected. 

3) Local TCP Port = the TCP port on which the robot is listening. If the first field entry “disabled” is selected then the robot is not listening on TCP any port. As a default often 9901 is selected. 

 

4) Show dialog messages when the socket filter is violated, to easily auto complete the contents of the socketfilter.ini. For safety reasons it is advised to deactivate this option during production. 

5) Connection status icon and message: shows the current state of the UDP and TCP connection 

6) The request textbox shows the latest xml message that the socket interface has received from another pc. 

7) The answer textbox shows the latest xml message that the socket interface has sent out. 

8) A tab panel to switch between the UDP Socket and TCP Socket request and answer messages. The incoming and outgoing telegrams can be observed. 

9) Below this is a part for the license, see the chapter about the license for more information about this part. 

10) The Save button will apply any changes made to the selected ports and restart the connection. 

11) Filter: This button opens an editor that allows you to edit the socket filter configuration. 

12) UDP log: a log file of UDP error messages 

13) TCP log: a log file of TCP error messages 

 

**Note**. When no valid license is available, then the fields such as Local IP Address cannot be filled in. 

**Note:** When configuring the network, avoid the following IP ranges, as they are used by the robot controller for internal purposes 

192.168.0.0 192.168.0.255 

172.16.0.0 172.16.255.255 

172.17.0.0 172.17.255.255 

Kuka Connect : Socket Filter

The socket filter defines which EndPoint (one other computer or multiple other computers) has access to which variables on the robot. The socket filter can be seen as a whitelist. The socket filter has to be configured by defining variables in an INI file on the robot controller. 

**C:\KRC\User\SocketFilter.ini**

This INI file can be edited using the Filter button. In this INI-file following settings must be made: 

 Host name or IP-address of pc’s in the network which should have permission to access the socket 

 The KRL variables that may be read through the socket interface 

 The KRL variables that may be written through the socket interface 

 

| **SocketFilter.ini:**                                        |
| ------------------------------------------------------------ |
| [RemoteEndPoint]   ;Host name or IP-address of pc’s in the network which should  have permission to access the socket   1=NB-KUKA167   2=200.0.0.123   [ReadableData]   ;The KRL Variables that may be read through the socket interface    1=$OV_PRO   2=$OV_JOG   3=$MODE_OP   [WritableData]   ;The KRL Variables that may be written through the socket  interface   1=$OV_PRO   2=$OV_JOG |

 

| Every  host can connect to the socket. Only the hosts that have permission as a  RemoteEndPoint will receive a correct answer telegram. A host with no  permission to access the socket always receives the following answer:   **Answer  telegram:** |
| ------------------------------------------------------------ |
| <?xml version="1.0" encoding="UTF-8"  standalone="no"?>   <Refused Message="You have no permission to access the  socket interface" /> |

 

Configuration of the robot (Config.dat)

​        How to define I/O ’s so that our API recognizes it

​        How to define user specific variables (array of values and names)

Configuration Kuka Connect (specify ip adress, variables for transfer of data)

 

Copy the config.dat file to the directory of the ROSE-AP.

 

OCB preparation : Docker based installation

 

Rose AP

​        XML file with settings (IP address …)

Available functions for the user

Internal functions

 

Grafana dashboards to look at the data

 