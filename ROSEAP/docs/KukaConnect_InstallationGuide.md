### Introduction

KUKA.Connect KRC provides a socket interface that other pc’s on the network can use to read or write variables in the robot controller.

The socket interface can communicate with UDP and TCP protocol. 

| At all times, fixed IP4 addresses are to be used for the server  level on the robot side as for the client side (external pc). |
| ------------------------------------------------------------ |
| Both the robot as the external pc must be placed in the same  network range and use the same network mask. It is advised to use a dedicated  network for the communication. |

 

 

### Kuka Connect : Installation

\- Unpack the received files and put them on an usb stick. 

\- Make sure to be in T1 level  (video)

\- Go to expert level on the robot controller  (video)

\- Install additional software Kuka Connect (video) 

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

 

### Kuka Connect : License

After receiving a license it can be entered on the Smartpad. Select MENU BUTTON > Start-Up > Licenses > CONNECT. The license key can be entered in the text box. Then press the “Validate” button. If the license key is correct then the textbox and validate button will disappear and instead it will show a “Valid license” message.

 

### Kuka Connect :  Network settings 

 

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





 