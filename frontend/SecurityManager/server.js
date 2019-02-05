const express = require('express');
const http = require('http');
const path = require('path');

const app = express();
const port = process.env.port || 4200;

app.use(express.static(__dirname + '/dist/Front'));
app.get('/*', (req, res) => {
    const filePath = path.join(__dirname, '/dist/Front/index.html');
    return res.sendFile(filePath);
});

const server = http.createServer(app);
server.listen(port, () => console.log('Running...'));