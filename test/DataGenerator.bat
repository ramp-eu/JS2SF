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


sleep 1

curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:VacuumPump:FlexEdgePump/attrs' \
  --header 'Content-Type: application/json' \
  --data '{
          "vacuumLevel":{"type":"Number", "value":0.77} }'

		  
curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:VacuumPump:DuoCutPump/attrs' \
  --header 'Content-Type: application/json' \
  --data '{
          "vacuumLevel":{"type":"Number", "value":0.65} 
		  }'
		  
sleep 1

curl -iX POST \
  'http://localhost:4200/_sql' \
  -H 'Content-Type: application/json' \
  -d '{"stmt":"SELECT * FROM etvacuumpump WHERE entity_id = '\''urn:ngsi-ld:VacuumPump:FlexEdgePump'\'' ORDER BY time_index ASC"}'
  
curl -iX POST \
  'http://localhost:4200/_sql' \
  -H 'Content-Type: application/json' \
  -d '{"stmt":"SELECT * FROM etvacuumpump WHERE entity_id = '\''urn:ngsi-ld:VacuumPump:DuoCutPump'\'' ORDER BY time_index ASC"}'
  
		  
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



curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
  "id": "urn:ngsi-ld:RoboticCell:DuoCut",
  "type": "RoboticCell",
  "refRobot": {
    "type": "Text",
    "value": "urn:ngsi-ld:Robot:DuoCutRobot"
  },
  "refVacuumPump": {
    "type": "Text",
    "value": "urn:ngsi-ld:VacuumPump:DuoCutPump"
  },
  "refIncomingPallet": {
    "type": "Text",
    "value": ""
  },
  "refOutgoingPallet": {
    "type": "Text",
    "value": "urn:ngsi-ld:Pallet:0005,urn:ngsi-ld:Pallet:0006,urn:ngsi-ld:Pallet:0007,urn:ngsi-ld:Pallet:0008,urn:ngsi-ld:Pallet:0009"
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
    "type": "Text",
    "value": ""
  },
  "refVacuumPump": {
    "type": "Text",
    "value": ""
  },
  "refIncomingPallet": {
    "type": "Text",
    "value": ""
  },
  "refOutgoingPallet": {
    "type": "Text",
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
    "type": "Text",
    "value": ""
  },
  "refVacuumPump": {
    "type": "Text",
    "value": ""
  },
  "refIncomingPallet": {
    "type": "Text",
    "value": ""
  },
  "refOutgoingPallet": {
    "type": "Text",
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


sleep 1


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
    "value": 11
  },
  "errorMessage": {
    "type" : "Text",
    "value": "Errormsg 11"
  }
}'  


sleep 1


curl -iX POST \
  'http://localhost:4200/_sql' \
  -H 'Content-Type: application/json' \
  -d '{"stmt":"SELECT * FROM etroboticcell WHERE entity_id = '\''urn:ngsi-ld:RoboticCell:FlexEdge'\'' ORDER BY time_index ASC"}'
  
curl -iX POST \
  'http://localhost:4200/_sql' \
  -H 'Content-Type: application/json' \
  -d '{"stmt":"SELECT * FROM etroboticcell WHERE entity_id = '\''urn:ngsi-ld:RoboticCell:DuoCut'\'' ORDER BY time_index ASC"}'
 

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

sleep 1


curl -iX POST \
  'http://localhost:4200/_sql' \
  -H 'Content-Type: application/json' \
  -d '{"stmt":"SELECT * FROM etroboticcell WHERE entity_id = '\''urn:ngsi-ld:RoboticCell:FlexEdge'\'' ORDER BY time_index ASC"}'
  

  
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
    "value": 1
  },
  "toolModel": {
    "type": "Text",
    "value": "Gripper"
  },
  "programName": {
    "type": "Text",
    "value": ""
  },
  "jobName": {
    "type": "Text",
    "value": "OPNEMEN.SRC"
  },
  "axis1": { 
	"type": "Number",
	"value": 122
  },
  "axis2": { 
	"type": "Number",
	"value": -25
  },
  "axis3": { 
	"type": "Number",
	"value": 87
  },
  "axis4": { 
	"type": "Number",
	"value": 104
  },
  "axis5": { 
	"type": "Number",
	"value": -55
  },
  "axis6": { 
	"type": "Number",
	"value": 12
  },
  "status": {
    "type": "Text",
	"value": "#P_FREE"
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


sleep 1


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

sleep 1

curl -iX POST \
  'http://localhost:4200/_sql' \
  -H 'Content-Type: application/json' \
  -d '{"stmt":"SELECT * FROM etrobot WHERE entity_id = '\''urn:ngsi-ld:Robot:FlexEdgeRobot'\'' ORDER BY time_index ASC"}'
  


curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:Robot:DuoCutRobot/attrs' \
  --header 'Content-Type: application/json' \
  --data '{
  "programName": {
    "type": "Text",
    "value": "AFLEGGEN.SRC"
  },
  "totalNumberOfPieces": {
    "type": "Integer",
	"value": 14
  },
  "currentPieceNumber": {
    "type": "Integer",
	"value": 6
  }  
}'  

sleep 1

curl -iX POST \
  'http://localhost:4200/_sql' \
  -H 'Content-Type: application/json' \
  -d '{"stmt":"SELECT * FROM etrobot WHERE entity_id = '\''urn:ngsi-ld:Robot:DuoCutRobot'\'' ORDER BY time_index ASC"}'
  




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


curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0011",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0011"
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

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0012",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0012"
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

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0013",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0013"
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

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0014",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0014"
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

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0015",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0015"
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

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0016",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0016"
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

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0017",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0017"
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

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0018",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0018"
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

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0019",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0019"
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

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
    "id": "urn:ngsi-ld:Pallet:0020",
    "type": "Pallet",
    "palletID":{
      "type" : "Text",
      "value": "0020"
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


sleep 1

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


sleep 1

curl -iX POST \
  'http://localhost:4200/_sql' \
  -H 'Content-Type: application/json' \
  -d '{"stmt":"SELECT * FROM etpallet WHERE entity_id = '\''urn:ngsi-ld:Pallet:0001'\'' ORDER BY time_index ASC"}'
  




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

curl -iX POST \
  'http://localhost:1026/v2/entities' \
  -H 'Content-Type: application/json' \
  -d '
{
  "id": "urn:ngsi-ld:Piece:DuoCutPiece",
  "type": "Piece",
  "pieceID": {
    "type": "Text",
    "value": "1234"
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
    "value": 42
  }
}'


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

sleep 1

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


curl -iX POST \
  --url 'http://localhost:1026/v2/entities/urn:ngsi-ld:Piece:DuoCutPiece/attrs' \
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
		"value": 128
	  }
}'  

sleep 1


curl -iX POST \
  'http://localhost:4200/_sql' \
  -H 'Content-Type: application/json' \
  -d '{"stmt":"SELECT * FROM etpiece WHERE entity_id = '\''urn:ngsi-ld:Piece:FlexEdgePiece'\'' ORDER BY time_index ASC"}'
  


curl -iX POST \
  'http://localhost:4200/_sql' \
  -H 'Content-Type: application/json' \
  -d '{"stmt":"SELECT * FROM etpiece WHERE entity_id = '\''urn:ngsi-ld:Piece:DuoCutPiece'\'' ORDER BY time_index ASC"}'
  



