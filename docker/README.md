# optidrive
The docker-compose file is used to start up the following containers:

- orion: The FIWARE Orion Context Broker which will receive requests using NGSI
- quantumleap: MongoDB database: Used by the Orion Context Broker to hold context data information such as data entities, subscriptions and registrations
- mongo-db: FIWARE QuantumLeap subscribe to context changes and persist them into a CrateDB database
- crate-db: CrateDB database: Used as a data sink to hold time-based historical context data, offers an HTTP endpoint to interpret time-based data queries
- grafana: Grafana time series analytics tool. Grafana is used to build and display dashboards 

All containers are configured to automatically start after a reboot.

The ports and versions are defined in a separate .env file.

A service script is provided to easily create, start and stop the containers.
To obtain the necessary Docker images locally use:

sudo ./services create    

To initialise and startup the containers use:

sudo ./services start

To stop the containers (data will be preserved) use :

sudo ./services stop

To clean up all data and images use:

docker-compose --log-level ERROR -p fiware down -v --remove-orphans



If you have a problem starting the crate-db container on Linux, check the logs:
sudo docker logs crate-db
[2020-10-26T10:27:51,427][INFO ][o.e.b.BootstrapChecks    ] [Marchkopf] bound or publishing to a non-loopback address, enforcing bootstrap checks
ERROR: [1] bootstrap checks failed
[1]: max virtual memory areas vm.max_map_count [65530] is too low, increase to at least [262144]

https://crate.io/docs/crate/howtos/en/latest/deployment/containers/docker.html#troubleshooting
The most common issue when running CrateDB on Docker is a failing bootstrap check because the memory map limit is too low. This can be adjusted on the host system.

https://crate.io/docs/crate/howtos/en/latest/admin/bootstrap-checks.html#bootstrap-checks

Edit /etc/sysctl.conf and configure:

vm.max_map_count = 262144

To apply this change, run:

$ sudo sysctl -p

This command will this reload all settings from /etc/sysctl.conf.

Now you can restart the docker container.
