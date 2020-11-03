# API usage

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
  },
  "throttling": 1
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
		"refOutgoingPallet"
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
  	  "refOutgoingPallet"	  
    ],
    "metadata": ["dateCreated", "dateModified"]
  },
  "throttling": 1
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
    "value": "KR 12345b"
  },
  "toolID": {
    "type": "Integer",
    "value": 0
  },
  "toolModel": {
    "type": "Text",
    "value": ""
  },
  "programName": {
    "type": "Text",
    "value": "MAIN.SRC"
  },
  "jobName": {
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
        "programName",
		"jobName",
	    "axis1",
	    "axis2",
	    "axis3",
	    "axis4",
	    "axis5",
	    "axis6",
        "status",
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
      "toolID",
      "programName",
      "jobName",
      "toolModel",
      "robotModel",
	  "axis1",
	  "axis2",
	  "axis3",
	  "axis4",
	  "axis5",
	  "axis6",
      "status",
	  "totalNumberOfPieces",
	  "currentPieceNumber"		
    ],
    "metadata": ["dateCreated", "dateModified"]
  },
  "throttling": 1
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
  "toolModel": {
    "type" : "Text",
    "value": "Gripper 3 vacuum zones"
  },
  "totalNumberOfPieces": {
    "type": "Integer",
	"value": 9
  },
  "currentPieceNumber": {
    "type": "Integer",
	"value": 7
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
    "refCurrentLocation":{
      "type" : "Text",
      "value": ""
    },
    "refComingFrom":{
      "type" : "Text",
      "value": ""
    },
    "refGoingTo":{
      "type" : "Text",
      "value": ""
    },
    "manufacturabilityOnFlexEdge":{
      "type" : "Text",
      "value": ""
    }, 
    "refPieceList": {
        "type": "Text",
        "value": ""
    }, 
    "priority": {
        "type": "Integer",
        "value": 99
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
        "refCurrentLocation",
		"refComingFrom",
		"refGoingTo",
		"manufacturabilityOnFlexEdge",
		"refPieceList",
		"priority"
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
        "refCurrentLocation",
		"refComingFrom",
		"refGoingTo",
		"manufacturabilityOnFlexEdge",
		"refPieceList",
		"priority"
    ],
    "metadata": ["dateCreated", "dateModified"]
  },
  "throttling": 1
}'
```

### Update Pallet attributes

```
curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:Pallet:0001/attrs' \
  --header 'Content-Type: application/json' \
  --data '{
  "refCurrentLocation": {
    "type": "Text",
    "value": "urn:ngsi-ld:RoboticCell:FlexEdge"
	},
    "refComingFrom":{
      "type" : "Text",
      "value": "urn:ngsi-ld:RoboticCell:DuoCut"
    },
    "refGoingTo":{
      "type" : "Text",
      "value": "urn:ngsi-ld:RoboticCell:UnLoad"
    },
    "manufacturabilityOnFlexEdge":{
      "type" : "Text",
      "value": "CanProcess"
    }, 
    "refPieceList": {
        "type": "Text",
        "value": "45564,66887,99434,12346"
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
  "id": "urn:ngsi-ld:Piece:FlexEdgePiece",
  "type": "Piece",
  "pieceID": {
    "type": "Text",
    "value": "3456"
  },
  "manufacturabilityOnFlexEdge":{
      "type" : "Text",
      "value": "CanProcess"
  },
  "timeEnteredCell": {
    "type" : "DateTime",
    "value": "2000-01-01T00:00:00Z"
  },
  "timeLeftCell": {
    "type" : "DateTime",
    "value": "2000-01-01T00:00:00Z"
  },
  "printed": {
    "type" : "Text",
    "value": ""
  },
  "weight": {
    "type" : "Number",
    "value": 37
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
        "timeLeftCell"
      ]
    }
  },
  "notification": {
    "http": {
      "url": "http://quantumleap:8668/v2/notify"
    },
    "attrs": [
      "pieceID",
      "timeEnteredCell",
      "timeLeftCell",
      "manufacturabilityOnFlexEdge",
      "printed",
	  "weight"
    ],
    "metadata": ["dateCreated", "dateModified"]
  },
  "throttling": 1
}'
```

### Update Piece attributes
```
curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:Piece:FlexEdgePiece/attrs' \
  --header 'Content-Type: application/json' \
  --data '{
	  "pieceID": {
		"type": "Text",
		"value": "56456"
	  },
	  "manufacturabilityOnFlexEdge":{
		"type" : "Text",
		"value": "CanPickupOnly"
	  },
	  "timeLeftCell": {
		"type" : "DateTime",
		"value": "2020-10-12T12:00:00Z"
	  },
	  "printed": {
		"type" : "Text",
		"value": "Printed 123"
	  },
	  "weight": {
		"type" : "Number",
		"value": 74
	  }
}'  
```

