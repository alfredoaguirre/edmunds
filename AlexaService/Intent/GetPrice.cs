using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlexaService.Intent;

namespace AlexaService.Intent
{
    public class GetPrice : IntentBase
    {
        public GetPrice()
        {
            Name = "GetPrice";
            PositiveResponseTemplate.Add("The {slot:Year} {slot:Make} {slot:Model} has a starting price of {price}");
            PositiveResponseTemplate.Add("The {slot:Year} {slot:Make} {slot:Model} starts at {price} dollars");
            
            //Encountered in the URL fails or Price is unknown. Not prompting the user for further information
            NegativeResponseTemplate.Add("Hmm. I can't seem to find the price at this time.");

            Response["price"] = "styles[0].price.baseMSRP";

            //Encountered if the user starts app without declaring both make and model
            ErrorSlotResponse["Make"] = "What's the make of the car?";
            ErrorSlotResponse["Model"] = "What's the model of the car?";

            
            EdmundsUrlTemplate = "https://api.edmunds.com/api/vehicle/v2/{slot:Make}/{slot:Name}/{slot:Year}/styles?view=full&fmt=json&api_key=67t7jtrnvz8wyzgfpwgcqa3y";

        }
    }
}