console.log('Hello world');
var http = require('http');

//The url we want is: 'www.random.org/integers/?num=1&min=1&max=10&col=1&base=10&format=plain&rnd=new'
var options = {
    host: 'api.edmunds.com',
    path: '/api/vehicle/v2/Acura/Integra?fmt=json&api_key=67t7jtrnvz8wyzgfpwgcqa3y'
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
    });
}

http.request(options, callback).end();
