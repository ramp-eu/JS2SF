## Rose-AP Connectivity Kit for Kuka  Robots             ![image-20210413114418187](docs/images/image-20210413114418187.png)
<hr />

(making use of the KukaConnect software option)

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/6809f2d81ba94342baff450a3a411e6b)](https://app.codacy.com/gh/ramp-eu/JS2SF?utm_source=github.com&utm_medium=referral&utm_content=ramp-eu/JS2SF&utm_campaign=Badge_Grade_Settings)
[![License: Apache](https://img.shields.io/github/license/ramp-eu/JS2SF.svg)](https://www.apache.org/licenses/LICENSE-2.0.html#redistribution)
<br/>
[![Documentation Status](https://readthedocs.org/projects/js2sf/badge/?version=latest)](https://js2sf.readthedocs.io/en/latest/?badge=latest)
[![CII Best Practices](https://bestpractices.coreinfrastructure.org/projects/4816/badge)](https://bestpractices.coreinfrastructure.org/projects/4816)

This software allows all owners of Kuka robots to connect to the Fiware-ecosystem and make use of all the software components available
in RAMP (ex. Grafana). The tutorials and the predefined use-cases are an inspiration to start with. In case of the simple implementation, just following the step by step tutorial will be sufficient. For more advanced implementations, a little more is needed but the solution can grow with more advanced requirements. In both cases the development time is considerable 
shorter by using this ROSE-AP.


This project is part of [DIH^2](http://www.dih-squared.eu/). For more information check the RAMP Catalogue entry for the
[components](https://github.com/xxx).

| :books: [Documentation](https://js2sf.readthedocs.io/en/latest/) | :whale: [Docker-compose File](https://github.com/ramp-eu/JS2SF/tree/master/docker) |
| --------------------------------------------- | ------------------------------------------------------------- |


## Contents

-   [Background](#background)
-   [Getting Started](#gettingstarted)
-   [Install](#install)
-   [Usage](#usage)
-   [API](#api)
-   [YouTube](#youtube)
-   [Testing](#testing)
-   [License](#license)


## Background<a name="background"></a>

This Rose-AP is an “Open Source” component which has a generic basis. A predefined set of robot-data is extracted from the robot and piped to a context broker. If other information needs to be available, you can follow a tutorial to extend the “open source” code with these extra features.

Software architecture :

The ROSE-AP will be defined as a software application (within the iPC infrastructure) and consists of :

·    The north port and its standard NGSI interface to communicate with the Context Broker as NGSI context consumer/provider. The interface consists of a set of CRUD Operations, queries, subscriptions and/or registrations on NGSI Context Data. 

 ·    The south port and its interface to communicate with the Kuka robot through the Kuka Connect Module. The south port interface determines the Kuka Connect Features your ROSE-AP is compliant with.



![image-20210413115019521](docs/images/image-20210413115019521.png)



More into detail there are two different kind of information (variables) that can be active in this :

1. System variables of the robot These variables are read only (cannot be written). Only a kind of registration in the Kuka Connect module is required.
2. In and outputs. These are accessible by the arrays $IN and $OUT through kuka Connect. In one of the following sections you can find instructions how to configure the i/o in the robot so that they become readable (robot inputs) and writable (robot outputs) from the south gate of our api. 

As an endpoint for our demonstrations and use cases we use the graphical user interface of Grafana. In this environment we developed a dashboard displaying all properties of the connected robot.

 ![image-20210413115152054](docs/images/image-20210413115152054.png)


## Getting Started<a name="gettingstarted"></a>

[In this section you will find a way to test the component. ](docs/getting-started.md)

## Install and configure<a name="install"></a>

More information about how to install the connectivity kit can be found at the corresponding section of the
[Installation & Administration Guide](docs/installationguide.md).

A `Docker-compose file` is also available for your use - further information can be found [here](https://github.com/ramp-eu/JS2SF/tree/master/docker)



## Usage<a name="usage"></a>



Information about how to use the the connectivity kit can be found in the [User & Programmers Manual](docs/usermanual.md).



## API<a name="api"></a>

More information about  the internal structure  (available functions and use data model)of the RoseAp can be found in [API documentation](docs/api.md)

## YouTube <a name="youtube"></a>

In these you tube video's, you can find demonstrations of the step by step tutorial.

- The video [RoseAP PartI SoftwareInstallation](https://www.youtube.com/watch?v=npqQ6tkVAN0) shows how to perform the [installation in windows](docs/getting-started.md#installation-in-windows) of all the needed software components.
- The next video [RoseAP PartII Starting the OrionContextBroker](https://www.youtube.com/watch?v=CTJp41MulPg) demonstrates what you have to do to set up the [context broker](docs/getting-started.md#starttheorioncontextbroker)
- The two following videos demonstrate how to set up [the grafana environment](docs/getting-started.md#grafana).  At the end of the first video : [RoseAP PartIII Configuring Grafana part1](https://www.youtube.com/watch?v=ISLZedSzbX0),  i run into a grafana configuration problem. In the next video: [RoseAP PartIVConfiguring Grafana part2](https://www.youtube.com/watch?v=vVRnSDgO8ZU) you can find the correction i had to make.
- In the video [RoseAP PartV Settingup the RoseAP + Robotsimulator](https://www.youtube.com/watch?v=BpVdY4MrLMw) you can find informtion about the setup for [the RoseAP and the Robotsimulator](docs/getting-started.md#roseap).
- [RoseAP PartVI Atomic tests](https://www.youtube.com/watch?v=Yc3v1ncs2xE) is about testing the RoseAP whitout the robotsimulator. More explanation are [here](docs/getting-started.md#atomictests)
- [RoseAP PartVII Clean up the containers](https://www.youtube.com/watch?v=eZWRKvjKiwA) explains a way to clean up the environment and [stop the context broker](docs/getting-started.md#stopping).




## Testing<a name="testing"></a>

When users download the API it is obvious that they do not have a virtual robot available to test with. This makes it impossible to create a use case that allows the user to see and examinate in detail the behaviour of the API. To overcome this we developped a limited robotsimulator.

##### Upfront testing with limited functionality

To allow people to test upfront an implementation of the ROSE-AP, we made a container-based (docker) implementation of the ROSE-AP as binaries that may be used for testing purposes. For this purpose a predefined set of robot-data is extracted from the robot and piped to the context broker.

[Following documents](docs/getting-started.md) will guide you through a quick test setup of the demonstrator.

By changing the values in the Robot Simulator, you can see the changing values in the dashboards.

##### Atomic tests

To allow the user to experiment with the data and the RoseAP a document is created with a set of simple test commands. As wel c# commands and Curl commands are foreseen. You can find them [here](docs/api.md) .


  

## License<a name="license"></a>

[Apache2.0](LICENSE) © 2021 Optidrive<TTE>
