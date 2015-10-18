exports.handler = function( event, context ) {
    console.log( "event", event.request );
    console.log("Running index.handler");
	if (event.request.type === "LaunchRequest") {
	onLaunch(event.request,
			 event.session,
			 function callback(sessionAttributes, speechletResponse) {
				context.succeed(buildResponse(sessionAttributes, speechletResponse));
			 });
			  return;
			 }
					 
    var arg = event.request.intent.slots;
    console.log( "==================================");
    console.log( "event", arg );
    console.log( "==================================");
    console.log("Stopping index.handler");
	

					 
    var http = require('http');
    this.context = context;
    this.model = arg.model.value;
    this.make = arg.make.value;
    this.year = arg.year.value;
    
    var options = {
        host: 'api.edmunds.com',
        path: '/api/vehicle/v2/' + make + '/' + model + '/'+ year + '/styles?view=full&fmt=json&api_key=67t7jtrnvz8wyzgfpwgcqa3y'
    };
    
    callback = function (response) {
        var str = '';
        
        //another chunk of data has been recieved, so append it to `str`
        response.on('data', function (chunk) {
            str += chunk;
        });
        
        //the whole response has been recieved, so we just print it out here
        response.on('end', function () {
            console.log(str);
            var resp = JSON.parse(str);
            var respString = "";
            if (resp.styles[0])
                respString = make + " " + model + " " + year + " highway: " + resp.styles[0].MPG.highway + "mpg city: " + resp.styles[0].MPG.city +"mpg";
            else
                respString = "No data for " + make + " " + model + " " + year;
            var response = {
                "version": "1.0",
                "sessionAttributes": {},
                "response": {
                    "outputSpeech": {
                        "type": "PlainText",
                        "text": respString,
                    },
                    "card": {
                        "type": "Simple",
                        "title": "SessionSpeechlet - Welcome",
                        "content": respString
                    },
                    "reprompt": {
                        "outputSpeech": {
                            "type": "PlainText",
                            "text": respString + ";  do need another one?",
                        }
                    },
                    "shouldEndSession": false
                }
            };
            console.log("==================================");
            console.log(response.response);
            context.succeed(response); 
        });
    }
    
    http.request(options, callback).end();
}
/**
 * Called when the user launches the skill without specifying what they want.
 */
function onLaunch(launchRequest, session, callback) {
    console.log("onLaunch requestId=" + launchRequest.requestId
                + ", sessionId=" + session.sessionId);

    // Dispatch to your skill's launch.
    getWelcomeResponse(callback);
}


function getWelcomeResponse(callback) {
    // If we wanted to initialize the session to have some attributes we could add those here.
    var sessionAttributes = {};
    var cardTitle = "Welcome";
    var speechOutput = "Welcome to the Home Key Studios - Car Deets Test, "
                + "Please tell me your favorite car by saying, "
                + "my favorite car is a Honda Civic";
    // If the user either does not reply to the welcome message or says something that is not
    // understood, they will be prompted again with this text.
    var repromptText = "Please tell me your favorite car by saying, "
                + "my favorite color is a Honda Civic";
    var shouldEndSession = false;

    callback(sessionAttributes,
             buildSpeechletResponse(cardTitle, speechOutput, repromptText, shouldEndSession));
             
}

// --------------- Helpers that build all of the responses -----------------------

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

