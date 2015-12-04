using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlexaService.Intent;

namespace AlexaService.Intent
{
    public class GetMileage : IntentBase
    {
        public GetMileage()
        {
            Name = "GetMileage";
            PositiveResponseTemplate.Add("The gas mileage of {slot:Year} {slot:Make} {slot:Model} is {cityMpg} in the city and {highwayMpg} on the highway");
            PositiveResponseTemplate.Add("The city MPG of the {slot:Year} {slot:Make} {slot:Model} is {cityMpg} and the highway MPG is {highwayMpg}");


            //Encountered in the URL fails or MPG is unknown. Not prompting the user for further information
            NegativeResponseTemplate.Add("I don't know at this time.");

            Response["highwayMpg"] = "styles[0].MPG.highway";
            Response["cityMpg"] = "styles[0].MPG.city";
            
            //Encountered if the user starts app without declaring both make and model
            ErrorSlotResponse["Make"] = "What's the make of the car?";
            ErrorSlotResponse["Model"] = "What's the model of the car?";

            EdmundsUrlTemplate = "https://api.edmunds.com/api/vehicle/v2/{slot:Make}/{slot:Name}/{slot:Year}/styles?view=full&fmt=json&api_key=67t7jtrnvz8wyzgfpwgcqa3y";

        }
    }
}