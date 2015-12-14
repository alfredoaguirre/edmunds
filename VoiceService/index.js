exports.handler = function (event, context) {
    console.log("Called onIntent intent=" , event.request);
    
    var post_data = JSON.stringify(event)
    
    var post_options = {
        host: 'alfredodejesus.azurewebsites.net',
        //port: '80',
        path: '/alexa',
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            'Content-Length': Buffer.byteLength(post_data)
        }
    };
    var http = require('http');
    
    var post_req = http.request(post_options, function (res) {
        res.setEncoding('utf8');
        var responseString = '';
        
        res.on('data', function (data) {
            responseString += data;
        });
        
        res.on('end', function () {
            var repromptText = "";
            
            var cardTitle = "Alexa HA Request";
            var sessionAttributes = {};
            console.log("++<<" + responseString + ">>++");
            var response = JSON.parse(responseString);
            context.succeed(response);
        });
    });
    
    // post the data
    console.log("++<<---" + post_data + "-->>++");
    post_req.write(post_data);
    post_req.end();
}