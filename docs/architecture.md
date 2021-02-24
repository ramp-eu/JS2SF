# Architecture

[Back](README.md)

![Overview Architecture](./images/architecture.png)


The user interface of our experiment is created in Grafana. We created four different Dashboards.
All data on the dashboard is taken from a timebased database (cratedb) and is written into this database by the OCB

- Roboticcell: 

                             - represents the state of the robotic cell
                             - on top you can select which cell you want to see
                             - we show following data : number of pieces, state of the robot, state of vacuum, error info

- Robot: 

                             - represents the state of the robot
                             - on top you can select which robot you want to see
                             - we show following data : state of the robot, state of vacuum, tool info, state of inputs and outputs
                             
- Pallet_Overview : 

                            - show detailed information about a pallet
                            - on top you can select the pallet nr of the pallet you want to see
                            - shows the id of all the pieces on the pallet
                            - shows the manufacturability and priority of this pallet (based on SFS)
                            - shows all logged actions on this pallet

- Pallet_Operation : is similar to the Pallet_Overview but does not show the logged data
                            
By scanning the barcode on the pallet (using a smart phone or tablet) the operator can see the grafana dashboard from that pallet.

At this moment the deployment on the VTT server is limited to QuantumLeap, CrateDB and the OCB. 
Configuration of the datamodel and subscriptions are explained in the docker compose part.

All data models we use to communicate with the OCB we try to realise following the NGSI. More info on the used data model can be found at https://github.com/smart-data-models/incubated/tree/master/DIH2/JS2SF

From within the HMI we are running the SFS on the data in the context broker :
- QRM : Everytime a pallet is placed on/ or taken from the shopfloor this service is called and recalculates the priority of all pallets.
- Manufacturability : Everytime a pallet is completely loaded all pieces are calculated and the manufacturability of the pallet is written to the OCB
