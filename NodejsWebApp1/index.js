// For development/testing purposes

exports.handler = function( event, context ) {
    console.log( "Running index.handler" );
    console.log( "==================================");
    console.log( "event", event );
    console.log( "==================================");
    console.log("Stopping index.handler");
    var http = require('http');
    
    http://api.edmunds.com/api/vehicle/v2/{make}/{model}?fmt=json&api_key={67t7jtrnvz8wyzgfpwgcqa3y}
    

    var response = {
        "version": "1.0",
        "sessionAttributes": {},
        "response": {
            "outputSpeech": {
                "type": "PlainText",
                "text": "Welcome to the Alexa Skills Kit sample, Please tell me your favorite color by saying, my favorite color is red"
            },
            "card": {
                "type": "Simple",
                "title": "SessionSpeechlet - Welcome",
                "content": "SessionSpeechlet - Welcome to the Alexa Skills Kit sample, Please tell me your favorite color by saying, my favorite color is red"
            },
            "reprompt": {
                "outputSpeech": {
                    "type": "PlainText",
                    "text": "Please tell me your favorite color by saying, my favorite color is red"
                }
            },
            "shouldEndSession": false
        }
    };
    context.succeed(response); 
}

