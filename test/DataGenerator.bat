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

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
  "id": "urn:ngsi-ld:VacuumPump:DuoCutPump",
  "type": "VacuumPump",
  "vacuumLevel": {
    "type": "Number",
    "value": 0.88
  },
  "on": {
    "type": "Boolean",
    "value": false
  },
  "pumpModel": {
    "type": "Text",
    "value": "VP 234b"
  }
}'



curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
  "id": "urn:ngsi-ld:RoboticCell:FlexEdge",
  "type": "RoboticCell",
  "refRobot": {
    "type": "Relationship",
    "value": "urn:ngsi-ld:Robot:FlexEdgeRobot"
  },
  "refVacuumPump": {
    "type": "Relationship",
    "value": "urn:ngsi-ld:VacuumPump:FlexEdgePump"
  },
  "refPiece": {
    "type": "Relationship",
    "value": ""
  },
  "refIncomingPallet": {
    "type": "Relationship",
    "value": ""
  },
  "refOutgoingPallet": {
    "type": "Relationship",
    "value": ""
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



curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
  "id": "urn:ngsi-ld:RoboticCell:DuoCut",
  "type": "RoboticCell",
  "refRobot": {
    "type": "Relationship",
    "value": "urn:ngsi-ld:Robot:DuoCutRobot"
  },
  "refVacuumPump": {
    "type": "Relationship",
    "value": "urn:ngsi-ld:VacuumPump:DuoCutPump"
  },
  "refPiece": {
    "type": "Relationship",
    "value": ""
  },
  "refIncomingPallet": {
    "type": "Relationship",
    "value": ""
  },
  "refOutgoingPallet": {
    "type": "Relationship",
    "value": ""
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

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
  "id": "urn:ngsi-ld:RoboticCell:Load",
  "type": "RoboticCell",
  "refRobot": {
    "type": "Relationship",
    "value": ""
  },
  "refVacuumPump": {
    "type": "Relationship",
    "value": ""
  },
  "refPiece": {
    "type": "Relationship",
    "value": ""
  },
  "refIncomingPallet": {
    "type": "Relationship",
    "value": ""
  },
  "refOutgoingPallet": {
    "type": "Relationship",
    "value": ""
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

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
  "id": "urn:ngsi-ld:RoboticCell:UnLoad",
  "type": "RoboticCell",
  "refRobot": {
    "type": "Relationship",
    "value": ""
  },
  "refVacuumPump": {
    "type": "Relationship",
    "value": ""
  },
  "refPiece": {
    "type": "Relationship",
    "value": ""
  },
  "refIncomingPallet": {
    "type": "Relationship",
    "value": ""
  },
  "refOutgoingPallet": {
    "type": "Relationship",
    "value": ""
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

curl -iX POST \
  'http://localhost:1026/v2/subscriptions/' \
  -H 'Content-Type: application/json' \
  -d '{
  "description": "Notify QuantumLeap on errorNumber changes on urn:ngsi-ld:RoboticCell:FlexEdge",
  "subject": {
    "entities": [
      {
        "idPattern": "urn:ngsi-ld:RoboticCell:FlexEdge"
      }
    ],
    "condition": {
      "attrs": [
        "errorNumber"
      ]
    }
  },
  "notification": {
    "http": {
      "url": "http://quantumleap:8668/v2/notify"
    },
    "attrs": [
      "errorNumber",
	  "errorMessage"
    ],
    "metadata": ["dateCreated", "dateModified"]
  },
  "throttling": 1
}'

curl -iX POST \
  'http://localhost:1026/v2/subscriptions/' \
  -H 'Content-Type: application/json' \
  -d '{
  "description": "Notify QuantumLeap on errorNumber changes on urn:ngsi-ld:RoboticCell:DuoCut",
  "subject": {
    "entities": [
      {
        "idPattern": "urn:ngsi-ld:RoboticCell:DuoCut"
      }
    ],
    "condition": {
      "attrs": [
        "errorNumber"
      ]
    }
  },
  "notification": {
    "http": {
      "url": "http://quantumleap:8668/v2/notify"
    },
    "attrs": [
      "errorNumber",
	  "errorMessage"
    ],
    "metadata": ["dateCreated", "dateModified"]
  },
  "throttling": 1
}'


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



curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:RoboticCell:DuoCut/attrs' \
  --header 'Content-Type: application/json' \
  --data '{
  "errorNumber": {
    "type": "Integer",
    "value": 10
  },
  "errorMessage": {
    "type" : "Text",
    "value": "Errormsg 10"
  }
}'  



curl -iX POST \
  'http://localhost:4200/_sql' \
  -H 'Content-Type: application/json' \
  -d '{"stmt":"SELECT * FROM etroboticcell WHERE entity_id = '\''urn:ngsi-ld:RoboticCell:FlexEdge'\'' ORDER BY time_index ASC"}'
  
curl -iX POST \
  'http://localhost:4200/_sql' \
  -H 'Content-Type: application/json' \
  -d '{"stmt":"SELECT * FROM etroboticcell WHERE entity_id = '\''urn:ngsi-ld:RoboticCell:DuoCut'\'' ORDER BY time_index ASC"}'
  
  
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
    "value": ""
  },
  "jobName": {
    "type": "Text",
    "value": ""
  }
}'
  

 curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
 {
  "id": "urn:ngsi-ld:Robot:DuoCutRobot",
  "type": "Robot",
  "robotID": {
    "type": "Text",
    "value": "DuoCutRobot"
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
    "value": ""
  },
  "jobName": {
    "type": "Text",
    "value": ""
  }
}'


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
      "type" : "Relationship",
      "value": ""
    },
    "refComingFrom":{
      "type" : "Relationship",
      "value": ""
    },
    "refGoingTo":{
      "type" : "Relationship",
      "value": ""
    },
    "manufactureabilityOnFlexEdge":{
      "type" : "Text",
      "value": ""
    }, 
    "refPieceList": {
        "type": "Relationship",
        "value": ""
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
    "refCurrentLocation":{
      "type" : "Relationship",
      "value": ""
    },
    "refComingFrom":{
      "type" : "Relationship",
      "value": ""
    },
    "refGoingTo":{
      "type" : "Relationship",
      "value": ""
    },
    "manufactureabilityOnFlexEdge":{
      "type" : "Text",
      "value": ""
    }, 
    "refPieceList": {
        "type": "Relationship",
        "value": ""
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
    "refCurrentLocation":{
      "type" : "Relationship",
      "value": ""
    },
    "refComingFrom":{
      "type" : "Relationship",
      "value": ""
    },
    "refGoingTo":{
      "type" : "Relationship",
      "value": ""
    },
    "manufactureabilityOnFlexEdge":{
      "type" : "Text",
      "value": ""
    }, 
    "refPieceList": {
        "type": "Relationship",
        "value": ""
    }
}'

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0004",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0004"
    },
	"timeOfLoading": {
	  "type" : "DateTime",
      "value": "2000-01-01T00:00:00Z"
    },
    "refCurrentLocation":{
      "type" : "Relationship",
      "value": ""
    },
    "refComingFrom":{
      "type" : "Relationship",
      "value": ""
    },
    "refGoingTo":{
      "type" : "Relationship",
      "value": ""
    },
    "manufactureabilityOnFlexEdge":{
      "type" : "Text",
      "value": ""
    }, 
    "refPieceList": {
        "type": "Relationship",
        "value": ""
    }
}'

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0005",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0005"
    },
	"timeOfLoading": {
	  "type" : "DateTime",
      "value": "2000-01-01T00:00:00Z"
    },
    "refCurrentLocation":{
      "type" : "Relationship",
      "value": ""
    },
    "refComingFrom":{
      "type" : "Relationship",
      "value": ""
    },
    "refGoingTo":{
      "type" : "Relationship",
      "value": ""
    },
    "manufactureabilityOnFlexEdge":{
      "type" : "Text",
      "value": ""
    }, 
    "refPieceList": {
        "type": "Relationship",
        "value": ""
    }
}'

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0006",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0006"
    },
	"timeOfLoading": {
	  "type" : "DateTime",
      "value": "2000-01-01T00:00:00Z"
    },
    "refCurrentLocation":{
      "type" : "Relationship",
      "value": ""
    },
    "refComingFrom":{
      "type" : "Relationship",
      "value": ""
    },
    "refGoingTo":{
      "type" : "Relationship",
      "value": ""
    },
    "manufactureabilityOnFlexEdge":{
      "type" : "Text",
      "value": ""
    }, 
    "refPieceList": {
        "type": "Relationship",
        "value": ""
    }
}'

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0007",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0007"
    },
	"timeOfLoading": {
	  "type" : "DateTime",
      "value": "2000-01-01T00:00:00Z"
    },
    "refCurrentLocation":{
      "type" : "Relationship",
      "value": ""
    },
    "refComingFrom":{
      "type" : "Relationship",
      "value": ""
    },
    "refGoingTo":{
      "type" : "Relationship",
      "value": ""
    },
    "manufactureabilityOnFlexEdge":{
      "type" : "Text",
      "value": ""
    }, 
    "refPieceList": {
        "type": "Relationship",
        "value": ""
    }
}'

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0008",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0008"
    },
	"timeOfLoading": {
	  "type" : "DateTime",
      "value": "2000-01-01T00:00:00Z"
    },
    "refCurrentLocation":{
      "type" : "Relationship",
      "value": ""
    },
    "refComingFrom":{
      "type" : "Relationship",
      "value": ""
    },
    "refGoingTo":{
      "type" : "Relationship",
      "value": ""
    },
    "manufactureabilityOnFlexEdge":{
      "type" : "Text",
      "value": ""
    }, 
    "refPieceList": {
        "type": "Relationship",
        "value": ""
    }
}'

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0009",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0009"
    },
	"timeOfLoading": {
	  "type" : "DateTime",
      "value": "2000-01-01T00:00:00Z"
    },
    "refCurrentLocation":{
      "type" : "Relationship",
      "value": ""
    },
    "refComingFrom":{
      "type" : "Relationship",
      "value": ""
    },
    "refGoingTo":{
      "type" : "Relationship",
      "value": ""
    },
    "manufactureabilityOnFlexEdge":{
      "type" : "Text",
      "value": ""
    }, 
    "refPieceList": {
        "type": "Relationship",
        "value": ""
    }
}'

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0010",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0010"
    },
	"timeOfLoading": {
	  "type" : "DateTime",
      "value": "2000-01-01T00:00:00Z"
    },
    "refCurrentLocation":{
      "type" : "Relationship",
      "value": ""
    },
    "refComingFrom":{
      "type" : "Relationship",
      "value": ""
    },
    "refGoingTo":{
      "type" : "Relationship",
      "value": ""
    },
    "manufactureabilityOnFlexEdge":{
      "type" : "Text",
      "value": ""
    }, 
    "refPieceList": {
        "type": "Relationship",
        "value": ""
    }
}'


curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
  "id": "urn:ngsi-ld:Piece:FlexEdgePiece",
  "type": "Piece",
  "pieceID": {
    "type": "Text",
    "value": ""
  },
  "manufacturabilityOnFlexEdge":{
      "type" : "Text",
      "value": ""
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
  }
}'

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
  "id": "urn:ngsi-ld:Piece:DuoCutPiece",
  "type": "Piece",
  "pieceID": {
    "type": "Text",
    "value": ""
  },
  "manufacturabilityOnFlexEdge":{
      "type" : "Text",
      "value": ""
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
  }
}'


curl -iX POST \
  'http://localhost:1026/v2/subscriptions/' \
  -H 'Content-Type: application/json' \
  -d '{
  "description": "Notify QuantumLeap on pieceId changes on the EdgeFlexPiece",
  "subject": {
    "entities": [
      {
        "idPattern": "urn:ngsi-ld:Piece:EdgeFlexPiece"
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
      "manufactueabilityOnFlexEdge",
	  "printed"
    ],
    "metadata": ["dateCreated", "dateModified"]
  },
  "throttling": 1
}'


curl -iX POST \
  'http://localhost:1026/v2/subscriptions/' \
  -H 'Content-Type: application/json' \
  -d '{
  "description": "Notify QuantumLeap on pieceId changes on the DuoCutPiece",
  "subject": {
    "entities": [
      {
        "idPattern": "urn:ngsi-ld:Piece:DuoCutPiece"
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
      "manufactueabilityOnFlexEdge",
	  "printed"
    ],
    "metadata": ["dateCreated", "dateModified"]
  },
  "throttling": 1
}'


