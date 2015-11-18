exports.handler = function (event, context) {
    console.log("Called onIntent intent=" + event.request + ", intentName=" + event.request.intent.name);
    
    var intent = event.request.intent,
        intentName = event.request.name;
    
    var jsonIntent = JSON.stringify(intent);
    var url = "https://alfredodejesus.azurewebsites.net/"; //+ escape(jsonIntent);
    
    var post_data = JSON.stringify(event)
    
    var post_options = {
        host: 'closure-compiler.appspot.com',
        port: '80',
        path: '/Alexa',
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Content-Length': Buffer.byteLength(post_data)
        }
    };
    
    console.log(post_options);
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
            console.log(responseString);
            var response = JSON.parse(responseString);
            var speechOutput = response.text;
            var shouldEndSession = response.shouldEndSession;
            console.log(responseString);
            context.succeed(response);

           // callback(sessionAttributes, buildSpeechletResponse(cardTitle, speechOutput, repromptText, shouldEndSession));
        });
    });
    
    // post the data
    post_req.write(post_data);
    post_req.end();


}

function buildSpeechletResponse(title, output, repromptText, shouldEndSession) {
    return {
        outputSpeech: {
            type: "PlainText",
            text: output
        },
        card: {
            type: "Simple",
            title: "SessionSpeechlet - " + title,
            content: "SessionSpeechlet - " + output
        },
        reprompt: {
            outputSpeech: {
                type: "PlainText",
                text: repromptText
            }
        },
        shouldEndSession: shouldEndSession
    };
}

function buildResponse(sessionAttributes, speechletResponse) {
    return {
        version: "1.0",
        sessionAttributes: sessionAttributes,
        response: speechletResponse
    };
}