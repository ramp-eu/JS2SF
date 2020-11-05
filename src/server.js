/*
Node js express webserver install.

Download en install node js for Windows van :
https://nodejs.org/en/
run
C:\Optidrive\HMI\P0296_HMI_Renier_ODFlexStone\Website\my-express-server>npm install express
C:\Optidrive\HMI\P0296_HMI_Renier_ODFlexStone\Website\my-express-server>npm install -g nodemonnode
https://expressjs.com/en/starter/hello-world.html

usage:
Open Command Prompt at C:\Optidrive\HMI\P0296_HMI_Renier_ODFlexStone\Website\my-express-server and run :
node server.js
*/

const express = require('express');
const app = express();
const port = 4000;

app.get('/', (req, res) => {
    res.sendFile(__dirname + "/index.html");
})

// http://192.168.111.12:4000/qrm 
app.get('/qrm', (req, res) => {
    var exec = require('child_process').exec;

    var fun = function () {

        exec('C:\\Optidrive\\HMI\\P0296_HMI_Renier_ODFlexStone\\QRM\\bin\\Debug\\QRM.exe', function (err, data) {
            if (err !== null) {
                res.send(err);
                console.log(err);
            } else {
                res.send(data);
                console.log(data.toString());
            }
        });
    };
    fun();
});

// http://192.168.111.12:4000/manufacturabilitypallet?palletnr=15
app.get('/manufacturabilitypallet', (req, res) => {
    var exec = require('child_process').exec;

    let palletnr = req.query.palletnr;
    var fun = function () {
        console.log("palletnr " + palletnr);
        exec('C:\\Optidrive\\HMI\\P0296_HMI_Renier_ODFlexStone\\ManufacturabilityPallet\\bin\\Debug\\ManufacturabilityPallet.exe ' + palletnr, function (err, data) {
            if (err !== null) {
                res.send(err);
                console.log(err);
            } else {
                res.send(data);
                console.log(data.toString());
            }
        });
    };
    fun();
});

// http://192.168.111.12:4000/manufacturabilitypiece?pieceid=FlexEdgePiece
app.get('/manufacturabilitypiece', (req, res) => {
    var exec = require('child_process').exec;

    let pieceid = req.query.pieceid;
    var fun = function () {
        console.log("pieceid " + pieceid);
        exec('C:\\Optidrive\\HMI\\P0296_HMI_Renier_ODFlexStone\\ManufacturabilityPiece\\bin\\Debug\\ManufacturabilityPiece.exe ' + pieceid, function (err, data) {
            if (err !== null) {
                res.send(err);
                console.log(err);
            } else {
                res.send(data);
                console.log(data.toString());
            }
        });
    };
    fun();
});

//https://stackoverflow.com/questions/19762350/execute-an-exe-file-using-node-js
//https://stackoverflow.com/questions/48361462/how-to-invoke-c-sharp-executable-from-node-js


app.listen(port, () => {
    console.log(`Renier Optidrive Service listening at http://localhost:${port}`);
});
