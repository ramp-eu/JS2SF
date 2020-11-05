#Setup Overview

For visualistaion we use Grafana Dashboards

On the VTT server the following components have to be running :
  CrateDB, QuantumLeap,OCB and MongoDB

In our office we have 4PC's. Each of them is running a particular software:
  -vOne virtual robot for the Duo Cut installation (unloading pieces from a saw to pallets)
  - One virtual robot for the FlexEdge (take a piece of a pallet, finishes it and place it on anothe pallet)
  - One IPC running the HMI software for the Duo Cut installation. This HMI is connected to the context broker and update pallet states and its robotic cell state.
  - One IPC running the HMI software for the Flex Edge installation. This HMI is connected to the context broker and update pallet states and its robotic cell state.

The HMI software is developped in C sharp using the Visual Studio environment.

The connection between the virtual robot and the IPC is realized using Kuka Connect software.

