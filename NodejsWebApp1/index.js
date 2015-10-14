// For development/testing purposes

exports.handler = function( event, context ) {
    console.log("Running index.handler");
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
                respString = make + " " + model + " " + year + " highway: " + resp.styles[0].MPG.highway + " city: " + resp.styles[0].MPG.city;
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
                            "text": respString + "  do need another one?",
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

 //   context.succeed(response); 
}

