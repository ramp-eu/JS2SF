# API



Available functions for the user

Internal functions

 

## Foreseen functions available in c#

Hereby a short description of the functions available for every robotic cell. These functions are integrated in our HMI. I put the source code in the src directory of this github.

#### Functions in ODfiware.cs

- private void UpdateRobotInfo() : updates the data of the Robot, data is known as global variables
- public void UpdateErrorInfo() : update errornr + message
- public void ParseRobotconfig(string path) : parse robot config.dat for input and output variables
- public async void AddRobotIO(List<RobotInput> i, List<RobotOutput> o) : Create the Inputs and outputs on the context broker.
- public async void UpdateRobotIO(List<RobotInput> i, List<RobotOutput> o) : Update the Inputs and outputs on the context broker.

## Available Curl commands



## Used data model

The data model exists of entities representing the status variables of the robot. To represent the I/o of the robot the necessary entities are created at startup of the api.





