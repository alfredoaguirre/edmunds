function onIntent(intentRequest, session, callback) {
    console.log("Called onIntent intent=" + intentRequest.intent + ", intentName=" + intentRequest.intent.name);
    
    var intent = intentRequest.intent,
        intentName = intentRequest.intent.name;
    
    var jsonIntent = JSON.stringify(intent);
    var url = "http://your.url.here:port/<your reference>?json=" + escape(jsonIntent);
    
    http.get(url, function (res) {
        var responseString = '';
        
        res.on('data', function (data) {
            responseString += data;
        })
        
        res.on('end', function () {
            var repromptText = "";
            
            var cardTitle = "Alexa HA Request";
            var sessionAttributes = {};
            
            var response = JSON.parse(responseString);
            var speechOutput = response.text;
            var shouldEndSession = response.shouldEndSession;
            
            callback(sessionAttributes, buildSpeechletResponse(cardTitle, speechOutput, repromptText, shouldEndSession));
        });
    });
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
    }
}

function buildResponse(sessionAttributes, speechletResponse) {
    return {
        version: "1.0",
        sessionAttributes: sessionAttributes,
        response: speechletResponse
    }
}