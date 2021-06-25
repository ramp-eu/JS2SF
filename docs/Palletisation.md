





# Palletisation Tutorial

This is a step-by-step tutorial that will introduce in detail how to use the  data-models described in  [RoboticIndustrialActivities](https://github.com/smart-data-models/dataModel.RoboticIndustrialActivities ) in a production facility.  The data in the data-model will be published in a FIWARE Orion Context Broker using NGSI data models.

The goal is to store data, available in the production system, on an Orion Context Broker and visualize it using Grafana dashboards. It can be available within the factory for everyone who it can be useful for (and is granted to do so).

### Description of the production facility which is digitized in this tutorial

In this factory the transport of the production pieces  through the factory, from one prodcution cell (robotic cell) to the next is realized on custom-made pallets.  

In the factory we have two robotics cell and the following production flow :

The first robotic cell is unloading machine A and loads several products on the pallets. The robot is grouping orders together and filling pallets. This robot will be referred to as 'DuoCutrobot'. When the pallets are full they are transported to the shop floor where they have to dry for a while.

After some time the pallets are presented to the second robot. The second robot takes a piece from a pallet , performs a process step on it and places the product on another pallet. This robot will be referred to as 'FlexEdgerobot'.

When a complete pallet is processed. The pieces on the pallet are judged by an operator when he unloads the pallet and prepares the pieces for transportation to the customer.

### Data model

In order to digitize the factory we used the following data-models : 

- the piece , representing the piece to be manufactured . A piece has a production code (barcode). Unfortunately this code was not unique  so we created a unique identifier to distinguish between different pieces.  The piece is created by the first robot in our case. In order to retrieve the piece in the factory its model contains a location field. The possible values for this location are in our case : Duocutrobot, PalletID, FlexEdgeRobot.

```
curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
  "id": "urn:ngsi-ld:Piece:1234",
  "type": "Piece",
  "pieceID": {
    "type": "Text",
    "value": "1234"   
  },
  "dateCreated": {
      "type" : "DateTime",
      "value": "2000-01-01T00:00:00Z"
  },
  "manufacturabilityOnFlexEdge":{
      "type" : "Text",
      "value": "CanProcess"
  },
  "timeEstimatedOnFlexEdge":{
	  "type" : "Number",
	  "value": 600
  },
  "weight": {
    "type" : "Number",
    "value": 42
  },
  "sequenceNumber": {
    "type" : "Number",
    "value": 1
  },
  "refpieceLocation": {
    "type" : "Text",
    "value": "urn:ngsi-ld:Robot:DuoCutRobot"  
  },
  "status": {
    "type" : "Text",
    "value": "Created"  
  }
}'

```

- the pallet , containing several production pieces. It is a container of pieces. Every pallet has a unique identifier, this is a barcode  which is printed on the front side of the pallet. The first time the pallet appears in the system (when the first piece ever is put on it) an instance of the pallet is generated on the context broker. To retrieve the pallet in the factory its location is primordial. Possible values are :  Duocutrobot, Shopfloor, FlexEdgeRobot, Unloading.

```
curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0001",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0001"
    },
    "timeOfLoading": {
      "type" : "DateTime",
      "value": "2000-01-01T00:00:00Z"
    },											
    "refpalletLocation":{
      "type" : "Text",
      "value": "shopfloor"
    },
    "refGoingTo":{
      "type" : "Text",
      "value": "FlexEdgeRobot"
    },
    "manufacturabilityOnFlexEdge":{
      "type" : "Text",
      "value": "CanProcess"
    },
    "priority": {
        "type": "Integer",
        "value": 3
    },
	"status": {
        "type": "Text",
        "value": "empty"    
    }
}'
```

- the robot-arm itself, containing a limited data set of the industrial robot itself.  The datamodel in this tutorial is the previous (old) version of the robotarm datamodel. 


- the vacuum pump, with a vacuum level and the on/off signal


- the robotic cell, containing some pallets, a robot arm and a vacuum pump



In the [RoseAP](https://github.com/ramp-eu/JS2SF/blob/master/ReadMe.md) a more a further developed and more detailed model of the robotarm is used.

For the visualisation in Grafana we use a timebased database (cratedb). The data from the Orion Context Broker is put into this database making use of notifications on the ContextBroker and of quatumleap. However Quantumleap only handles the simple datatypes in a correct way. That is why we realized our datamodels avoiding the use of references and arrays. We replaced them by using strings and by splitting up arrays into single elements.




### Installation of the required components

The complete installation was done in a windows environment. Following guidelines are written from this point of view. By using a docker compose file this can be realized in an automatic way.

##### Installation of the necessary software components in Windows

First install following softwares to get the test environment  running :

- Download cygwin tools on windows from https://www.cygwin.com/.

  Safe the installation file in a directory on your pc .

  Install cygwin tools on windows, accept all default suggestions during installation

  After installation of Cygwin, add the dos2unix command to cygwin :

  - ​	Open a cmd window
  - ​	Browse to the directory where the cygwin  installer is located
  - ​    Execute following command :  setup-x86_64.exe -q -P dos2unix

- Install Docker Desktop on Windows from https://docs.docker.com/docker-for-windows/install/, choose a Hyper-V backend.

- Create a directory c:/SmartDataModelTestEnvironment and  copy the following files to this location:

  - https://github.com/ramp-eu/JS2SF/tree/master/docker/docker-compose.yml
  - https://github.com/ramp-eu/JS2SF/tree/master/docker/services
  - https://github.com/ramp-eu/JS2SF/tree/master/docker/DataGenerator_tutorial

### 

##### Start the Orion Context Broker

- Start a cygwin terminal and go to the folder containing the docker-compose.yml file, for example:

```
  cd /cygdrive/c/SmartDataModelTestEnvironment/docker
```

Make sure the script files have Linux line endings by executing :

```
dos2unix DataGenerator_palletisation
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

If the installation went well you'll get some Orion version information Also Cratedb, Quantumleap and Grafana should be running by now.

To create all entities and notifications on the Orion Context Broker a bash script file is prepared. Execute it using the following command :

```
./DataGenerator_palletisation
```

Now the Orion Contect Broker is up and running.

You can find more details in the document [docker_readme](https://github.com/ramp-eu/JS2SF/blob/master/docker/README.md). Here you find the explanation about setting up the OCB, cratedb and quantumleap components.

### 

##### Grafana

- The docker-compose file has started an instance of the Grafana UI listening on port 3003, so the login page can be found at: http://localhost:3003/login. The default username is admin and the default password is admin.

- Following steps allow you to configure grafana and use the predefined dashboard :

  - Connect with cratedb

    - After logging in, a PostgreSQL datasource must be set up at http://localhost:3003/datasources with the following values:

    - ```
      Name CrateDB
      Host crate-db:5432
      Database doc      // doc is the default database for subscriptions without service-path
      User crate
      SSL Mode disable
      ```

  [![image-20210506114035259](https://github.com/ramp-eu/JS2SF/raw/master/docs/images/image-20210506114035259.png)](https://github.com/ramp-eu/JS2SF/blob/master/docs/images/image-20210506114035259.png)

  - Import the dashboard (C:\KukaConnectivityKit\docker\TutorialRoboticCell.json) into the grafana environment .

    - On the left side of the screen select the Dashboards -> Manage option

    [![image-20210506114558605](https://github.com/ramp-eu/JS2SF/raw/master/docs/images/image-20210506114558605.png)](https://github.com/ramp-eu/JS2SF/blob/master/docs/images/image-20210506114558605.png)

    - Look for the import button and browse to the TutorialRoboticCell.json file

    - [![image-20210506114915276](https://github.com/ramp-eu/JS2SF/raw/master/docs/images/image-20210506114915276.png)](https://github.com/ramp-eu/JS2SF/blob/master/docs/images/image-20210506114915276.png)

    - Select the robot dashboard. Now select the correct serial number of the robot and you will see the actual state of the robot values.

    - Another available dashboards (to see the pallet states) is :

      - TutorialPallet.js

        

After completing these steps the test environment is running and we can execute some actions.

###  Changing values in the data : Test

The actions are organised in a chronicle way starting when the first pallet and piece is created in the first cell. While performing these changes you can inspect the dashboards and see the changing values. 

###### Creating Pallets

Start with loading 3 pallets with the following id's : pallet0001, pallet0002 and pallet0003



```
curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0001",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0001"
    },
    "timeOfLoading": {
      "type" : "DateTime",
      "value": "2000-01-01T00:00:00Z"
    },											
    "refpalletLocation":{
      "type" : "Text",
      "value": "shopfloor"
    },
    "refGoingTo":{
      "type" : "Text",
      "value": ""
    },
    "manufacturabilityOnFlexEdge":{
      "type" : "Text",
      "value": ""
    },
    "priority": {
        "type": "Integer",
        "value": 99
    },
	"status": {
        "type": "Text",
        "value": "empty"    
    }
}'

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0002",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0002"
    },
    "timeOfLoading": {
      "type" : "DateTime",
      "value": "2000-01-01T00:00:00Z"
    },											
    "refpalletLocation":{
      "type" : "Text",
      "value": "shopfloor"
    },
    "refGoingTo":{
      "type" : "Text",
      "value": ""
    },
    "manufacturabilityOnFlexEdge":{
      "type" : "Text",
      "value": ""
    },
    "priority": {
        "type": "Integer",
        "value": 99
    },
	"status": {
        "type": "Text",
        "value": "empty"    
    }
}'

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0003",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0003"
    },
    "timeOfLoading": {
      "type" : "DateTime",
      "value": "2000-01-01T00:00:00Z"
    },											
    "refpalletLocation":{
      "type" : "Text",
      "value": "shopfloor"
    },
    "refGoingTo":{
      "type" : "Text",
      "value": ""
    },
    "manufacturabilityOnFlexEdge":{
      "type" : "Text",
      "value": ""
    },
    "priority": {
        "type": "Integer",
        "value": 99
    },
	"status": {
        "type": "Text",
        "value": "empty"    
    }
}'


#putting data in item pallet 0001
curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:Pallet:0001/attrs' \
  --header 'Content-Type: application/json' \
  --data '{
  "refpalletLocation": {
    "type": "Text",
    "value": "urn:ngsi-ld:RoboticCell:FlexEdge"
	},
    "refGoingTo":{
      "type" : "Text",
      "value": "urn:ngsi-ld:Location:UnLoad"
    },
    "manufacturabilityOnFlexEdge":{
      "type" : "Text",
      "value": "Unknown"
    }, 
    "priority": {
      "type": "Integer",
      "value": 99
    },
	"status": {
        "type": "Text",
        "value": "filled"
	}
}'  

#request data from pallet0001
curl -iX POST \
  'http://localhost:4200/_sql' \
  -H 'Content-Type: application/json' \
  -d '{"stmt":"SELECT * FROM etpallet WHERE entity_id = '\''urn:ngsi-ld:Pallet:0001'\'' ORDER BY time_index ASC"}'
  
```

###### Piece definition

Define 5 pieces on pallet 1and 2

```
# Create FlexEdgePiece

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
  {
  "id": "urn:ngsi-ld:Piece:FlexEdgePiece",
  "type": "Piece",
	  "pieceID": {
		"type": "Text",
		"value": "56456"
	  },
	  "manufacturabilityOnFlexEdge":{
		"type" : "Text",
		"value": "CanPickupOnly"
	  },
	  "dateCreated": {
		"type" : "DateTime",
		"value": "2000-01-01T00:00:00Z"
	  },
	  "sequenceNumber": {
		"type" : "Number",
		"value": 1
	  },
	  "refpieceLocation": {
		"type" : "Text",
		"value": "urn:ngsi-ld:Pallet:0001"  
	  },
	  "weight": {
		"type" : "Number",
		"value": 74
	  },
	  "status": {
		"type" : "Text",
		"value": "Created"  
	  }
}'  

#Create a DuoCutPiece a piece on the Flexedgerobot
#sequenceNumber hoeveelste op het pallet
curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
  {
  "id": "urn:ngsi-ld:Piece:DuoCutPiece",
  "type": "Piece",
	  "pieceID": {
		"type": "Text",
		"value": "56456"
	  },
	  "manufacturabilityOnFlexEdge":{
		"type" : "Text",
		"value": "CanPickupOnly"
	  },
	  "dateCreated": {
		"type" : "DateTime",
		"value": "2000-01-01T00:00:00Z"
	  },
	  "sequenceNumber": {
		"type" : "Number",
		"value": 1
	  },
	  "refpieceLocation": {
		"type" : "Text",
		"value": "urn:ngsi-ld:Robot:FlexEdgeRobot"  
	  },
	  "weight": {
		"type" : "Number",
		"value": 74
	  },
	  "status": {
		"type" : "Text",
		"value": "Created"  
	  }
}'  

#request all data from a piece knowing its id
curl -iX POST \
  'http://localhost:4200/_sql' \
  -H 'Content-Type: application/json' \
  -d '{"stmt":"SELECT * FROM etpiece WHERE entity_id = '\''urn:ngsi-ld:Piece:UniquePieceID'\'' ORDER BY time_index ASC"}'
  


#command to look up the piece which is on the duocut or flexedge
#TODO
curl -iX POST \
  'http://localhost:4200/_sql' \
  -H 'Content-Type: application/json' \
  -d '{"stmt":"SELECT * FROM etpiece WHERE entity_id = '\''urn:ngsi-ld:Piece:DuoCutPiece'\'' ORDER BY time_index ASC"}'


curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
  "id": "urn:ngsi-ld:Piece:Piece0001",
  "type": "Piece",
  "pieceID": {
    "type": "Text",
    "value": "Piece0001"   
  },
  "dateCreated": {
      "type" : "DateTime",
      "value": "2000-01-01T00:00:00Z"
  },
  "manufacturabilityOnFlexEdge":{
      "type" : "Text",
      "value": "CanProcess"
  },
  "timeEstimatedOnFlexEdge":{
	  "type" : "Number",
	  "value": 600
  },
  "weight": {
    "type" : "Number",
    "value": 42
  },
  "sequenceNumber": {
    "type" : "Number",
    "value": 1
  },
  "refpieceLocation": {
    "type" : "Text",
    "value": "urn:ngsi-ld:Pallet:0001"  
  }
}'

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
  "id": "urn:ngsi-ld:Piece:Piece0002",
  "type": "Piece",
  "pieceID": {
    "type": "Text",
    "value": "Piece0002"   
  },
  "dateCreated": {
      "type" : "DateTime",
      "value": "2000-01-01T00:00:00Z"
  },
  "manufacturabilityOnFlexEdge":{
      "type" : "Text",
      "value": "CanProcess"
  },
  "timeEstimatedOnFlexEdge":{
	  "type" : "Number",
	  "value": 600
  },
  "weight": {
    "type" : "Number",
    "value": 42
  },
  "sequenceNumber": {
    "type" : "Number",
    "value": 1
  },
  "refpieceLocation": {
    "type" : "Text",
    "value": "urn:ngsi-ld:Pallet:0001"  
  }
}'


curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
  "id": "urn:ngsi-ld:Piece:Piece0003",
  "type": "Piece",
  "pieceID": {
    "type": "Text",
    "value": "Piece0003"   
  },
  "dateCreated": {
      "type" : "DateTime",
      "value": "2000-01-01T00:00:00Z"
  },
  "manufacturabilityOnFlexEdge":{
      "type" : "Text",
      "value": "CanProcess"
  },
  "timeEstimatedOnFlexEdge":{
	  "type" : "Number",
	  "value": 600
  },
  "weight": {
    "type" : "Number",
    "value": 42
  },
  "sequenceNumber": {
    "type" : "Number",
    "value": 1
  },
  "refpieceLocation": {
    "type" : "Text",
    "value": "urn:ngsi-ld:Pallet:0002"  
  }
}'
```

###### Change the location of the pallets

Put the location of a pallet to shopfloor

```
curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:Pallet:0001/attrs' \
  --header 'Content-Type: application/json' \
  --data '{
  "refpalletLocation": {
    "type": "Text",
    "value": "urn:ngsi-ld:Location:ShopFloor"
	}
}'  
```

###### Put a pallet in a robotic cell

Load the pallet with id pallet0001 in the Flexedgerobot.

```
curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:Pallet:0001/attrs' \
  --header 'Content-Type: application/json' \
  --data '{
  "refpalletLocation": {
    "type": "Text",
    "value": "urn:ngsi-ld:Robot:FlexEdge"
	}
}'  
```


###### Take the piece with the robot

Transfer piece from pallet to robot

```
curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:Piece:0001/attrs' \
  --header 'Content-Type: application/json' \
  --data '{
  "refpieceLocation": {
	"type" : "Text",
	"value": "urn:ngsi-ld:Robot:FlexEdge"
	}
}'  
```

###### Change the status of the robot

put the robot status to active, change its axis values 


```
curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:Robot:FlexEdgeRobot/attrs' \
  --header 'Content-Type: application/json' \
  --data '{
  "status": {
	"type": "Text",
	"value": "#P_ACTIVE"
  },
  "axis1": { 
	"type": "Number",
	"value": 30
  },
  "axis2": { 
	"type": "Number",
	"value": 14
  },
  "axis3": { 
	"type": "Number",
	"value": -55
  },
  "axis4": { 
	"type": "Number",
	"value": 174
  },
  "axis5": { 
	"type": "Number",
	"value": 145
  },
  "axis6": { 
	"type": "Number",
	"value": -37
  }
}'  
```

###### Control the vacuumpump data

put the vacuum on/ off, specify the vacuumlevel

```
#filling out some data for the Flexedgepump
curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:VacuumPump:FlexEdgePump/attrs' \
  --header 'Content-Type: application/json' \
  --data '{
          "vacuumLevel":{"type":"Number", "value":0.77} }'

#filling out some data for the DuoCutpump		  
curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:VacuumPump:DuoCutPump/attrs' \
  --header 'Content-Type: application/json' \
  --data '{
          "vacuumLevel":{"type":"Number", "value":0.65} 
		  }'
```

```
#request data from the FlexEdgePump
curl -iX POST \
  'http://localhost:4200/_sql' \
  -H 'Content-Type: application/json' \
  -d '{"stmt":"SELECT * FROM etvacuumpump WHERE entity_id = '\''urn:ngsi-ld:VacuumPump:FlexEdgePump'\'' ORDER BY time_index ASC"}'
#request data from the DuoCutPump
curl -iX POST \
  'http://localhost:4200/_sql' \
  -H 'Content-Type: application/json' \
  -d '{"stmt":"SELECT * FROM etvacuumpump WHERE entity_id = '\''urn:ngsi-ld:VacuumPump:DuoCutPump'\'' ORDER BY time_index ASC"}'
  
```



###### Put the piece on a pallet

Transfer piece from robot to pallet (pallet0003)

```
curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:Piece:0001/attrs' \
  --header 'Content-Type: application/json' \
  --data '{
  "refpieceLocation": {
	"type" : "Text",
	"value": "urn:ngsi-ld:Pallet:0003"
	}
}'  
```

###### Empty the pallet

Special measures had to be taken when a piece is unloaded from a pallet. We needed an indicator on the time based database (cratedb) to know if the piece was still on a pallet or has been unloaded. So before reusing the pallet you have to make sure that all  pieces are put on pallet9999 for a short while and are subsequently deleted. In this code we delete all pieces from pallet0003.

```
curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:Piece:0003/attrs' \
  --header 'Content-Type: application/json' \
  --data '{
  "refpieceLocation": {
	"type" : "Text",
	"value": "urn:ngsi-ld:Pallet:9999"
	}
}'  
```

###### Query used in grafana for piecelist on a pallet

Query grafana to select the actual pieces on the pallet using the data in the cratedb (timebased database). Problem here is that in the timebased database all history is stored but to know the actual list of pieces all old ones needs to be removed and put on another (non existing) pallet with id pallet9999.

```
select substr(pp.\"entity_id\",19,6) as \"dbindex\",pp.\"pieceid\" as \"piece\", pp.\"manufacturabilityonflexedge\", substr(pp.\"refpiecelocation\",13,11) as location, aa.\"MaxTime\" as \"Timestamp\"\r\nfrom (select \"entity_id\", max(\"time_index\") as \"MaxTime\" from \"doc\".\"etpiece\" group by \"entity_id\") aa, \"doc\".\"etpiece\" as pp\r\nWHERE pp.\"time_index\" = aa.\"MaxTime\" and pp.\"entity_id\" = aa.\"entity_id\" and substr(pp.\"refpiecelocation\",20,4) = $Pallet order by \"sequencenumber\" asc"
  
```

