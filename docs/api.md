# API usage

[Back](README.md)

## API Orion (NGSIv2)
The API for the Orion context broker (NGSIv2) can be found at [link to API of NGSIv2](https://fiware-orion.readthedocs.io/en/master/user/walkthrough_apiv2/index.html).

## Datamodel
The datamodel can be found at [link to datamodel](https://github.com/smart-data-models/incubated/tree/master/DIH2/JS2SF).

## API requests

### Creation of a VacuumPump

```
curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
  "id": "urn:ngsi-ld:VacuumPump:FlexEdgePump",
  "type": "VacuumPump",
  "vacuumLevel": {
    "type": "Number",
    "value": 0.95
  },
  "on": {
    "type": "Boolean",
    "value": false
  },
  "pumpModel": {
    "type": "Text",
    "value": "VP 123a"
  }
}'
```

### VacuumPump Subscription

```
curl -iX POST \
  'http://localhost:1026/v2/subscriptions/' \
  -H 'Content-Type: application/json' \
  -d '{
  "description": "Notify QuantumLeap on vacuum level and on/off changes on any vacuumpump",
  "subject": {
    "entities": [
      {
        "idPattern": "urn:ngsi-ld:VacuumPump.*"
      }
    ],
    "condition": {
      "attrs": [
        "vacuumLevel",
        "on"
      ]
    }
  },
  "notification": {
    "http": {
      "url": "http://quantumleap:8668/v2/notify"
    },
    "attrs": [
      "vacuumLevel", "on"
    ],
    "metadata": ["dateCreated", "dateModified"]
  }
}'
```

### Update vacuumLevel

```
curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:VacuumPump:FlexEdgePump/attrs' \
  --header 'Content-Type: application/json' \
  --data '{
          "vacuumLevel":{"type":"Number", "value":0.77} }'
```

### Creation of a RoboticCell

```		  
curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
  "id": "urn:ngsi-ld:RoboticCell:FlexEdge",
  "type": "RoboticCell",
  "refRobot": {
    "type": "Text",
    "value": "urn:ngsi-ld:Robot:FlexEdgeRobot"
  },
  "refVacuumPump": {
    "type": "Text",
    "value": "urn:ngsi-ld:VacuumPump:FlexEdgePump"
  },
  "refIncomingPallet": {
    "type": "Text",
    "value": "urn:ngsi-ld:Pallet:0003"
  },
  "refOutgoingPallet": {
    "type": "Text",
    "value": "urn:ngsi-ld:Pallet:0004"  
  },
  "errorNumber": {
    "type": "Integer",
    "value": 0
  },
  "errorMessage": {
    "type": "Text",
    "value": ""
  },
  "totalNumberOfPieces": {
    "type": "Integer",
	"value": 0
  },
  "currentPieceNumber": {
    "type": "Integer",
	"value": 0
  }  
}'
```

### RoboticCell subscription

```
curl -iX POST \
  'http://localhost:1026/v2/subscriptions/' \
  -H 'Content-Type: application/json' \
  -d '{
  "description": "Notify QuantumLeap on errorNumber changes on urn:ngsi-ld:RoboticCell.*",
  "subject": {
    "entities": [
      {
        "idPattern": "urn:ngsi-ld:RoboticCell.*"
      }
    ],
    "condition": {
      "attrs": [
        "errorNumber",
        "refIncomingPallet",
		"refOutgoingPallet",
		"totalNumberOfPieces",
		"currentPieceNumber"		
      ]
    }
  },
  "notification": {
    "http": {
      "url": "http://quantumleap:8668/v2/notify"
    },
    "attrs": [
      "errorNumber",
      "errorMessage",
      "refIncomingPallet",
  	  "refOutgoingPallet",
	  "totalNumberOfPieces",
	  "currentPieceNumber"			  
    ],
    "metadata": ["dateCreated", "dateModified"]
  }
}'
```

### Update of an errorNumber

```
curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:RoboticCell:FlexEdge/attrs' \
  --header 'Content-Type: application/json' \
  --data '{
  "errorNumber": {
    "type": "Integer",
    "value": 5
  },
  "errorMessage": {
    "type" : "Text",
    "value": "Errormsg FlexEdge 5"
  }
}'  
```

### Update of incoming and outgoing Pallet

```
curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:RoboticCell:FlexEdge/attrs' \
  --header 'Content-Type: application/json' \
  --data '{
  "refIncomingPallet": {
    "type": "Text",
    "value": "urn:ngsi-ld:Pallet:0006"
  },
  "refOutgoingPallet": {
    "type" : "Text",
    "value": "urn:ngsi-ld:Pallet:0007"
  }
}'  
```

### Creation of a Robot

```
curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
 {
  "id": "urn:ngsi-ld:Robot:FlexEdgeRobot",
  "type": "Robot",
  "robotID": {
    "type": "Text",
    "value": "FlexEdgeRobot"
  },
  "robotModel": {
    "type": "Text",
    "value": "KR300R3000"
  },
  "toolID": {
    "type": "Integer",
    "value": 0
  },
  "jobCurrentState": {
    "type": "Text",
    "value": ""  
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
  },
  "status": {
    "type": "Text",
	"value": "#P_ACTIVE"
  }
}'
```

### Robot subscription

```
curl -iX POST \
  'http://localhost:1026/v2/subscriptions/' \
  -H 'Content-Type: application/json' \
  -d '{
  "description": "Notify QuantumLeap on changes on urn:ngsi-ld:Robot.*",
  "subject": {
    "entities": [
      {
        "idPattern": "urn:ngsi-ld:Robot.*"
      }
    ],
    "condition": {
      "attrs": [
        "toolID",
        "jobCurrentstate",
		"robotModel",
	    "axis1",
	    "axis2",
	    "axis3",
	    "axis4",
	    "axis5",
	    "axis6",
        "status"
      ]
    }
  },
  "notification": {
    "http": {
      "url": "http://quantumleap:8668/v2/notify"
    },
    "attrs": [
      "toolID",
      "jobCurrentstate",
      "robotModel",
	  "axis1",
	  "axis2",
	  "axis3",
	  "axis4",
	  "axis5",
	  "axis6",
      "status"
    ],
    "metadata": ["dateCreated", "dateModified"]
  }  
}'
```

### Update Robot attributes

```
curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:Robot:FlexEdgeRobot/attrs' \
  --header 'Content-Type: application/json' \
  --data '{
  "toolID": {
    "type": "Integer",
    "value": 3
  },
  "jobCurrentState": {
    "type": "Text",
    "value": "polishing"
  }
}'  
```

### Creation of a Pallet

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
```

### Pallet subscription

```
curl -iX POST \
  'http://localhost:1026/v2/subscriptions/' \
  -H 'Content-Type: application/json' \
  -d '{
  "description": "Notify QuantumLeap of changes on any pallet",
  "subject": {
    "entities": [
      {
        "idPattern": "urn:ngsi-ld:Pallet.*"
      }
    ],
    "condition": {
      "attrs": [
        "timeOfLoading",
        "refpalletLocation",
		"refGoingTo",
		"manufacturabilityOnFlexEdge",
		"priority",
		"status"
      ]
    }
  },
  "notification": {
    "http": {
      "url": "http://quantumleap:8668/v2/notify"
    },
    "attrs": [
		"palletID",
		"timeOfLoading",
        "refpalletLocation",
		"refGoingTo",
		"manufacturabilityOnFlexEdge",
		"priority",
		"status"
    ],
    "metadata": ["dateCreated", "dateModified"]
  }
}'
```

### Update Pallet attributes

```
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
```

### Creation of a Piece

```
curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
  "id": "urn:ngsi-ld:Piece:UniquePieceID",
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

### Piece subscription

```
curl -iX POST \
  'http://localhost:1026/v2/subscriptions/' \
  -H 'Content-Type: application/json' \
  -d '{
  "description": "Notify QuantumLeap on changes on urn:ngsi-ld:Piece.*",
  "subject": {
    "entities": [
      {
        "idPattern": "urn:ngsi-ld:Piece.*"
      }
    ],
    "condition": {
      "attrs": [
        "refpieceLocation",
		"manufacturabilityOnFlexEdge",
		"status"
      ]
    }
  },
  "notification": {
    "http": {
      "url": "http://quantumleap:8668/v2/notify"
    },
    "attrs": [
      "pieceID",
      "dateCreated",
      "refpieceLocation",
      "manufacturabilityOnFlexEdge",
      "sequenceNumber",
	  "weight",
	  "status"
    ],
    "metadata": ["dateCreated", "dateModified"]
  }
}'
```

### Update Piece attributes

```
curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:Piece:UniquePieceID/attrs' \
  --header 'Content-Type: application/json' \
  --data '{
	  "manufacturabilityOnFlexEdge":{
		"type" : "Text",
		"value": "CanPickupOnly"
	  },
	  "weight": {
		"type" : "Number",
		"value": 74
	  }
}'  
```
