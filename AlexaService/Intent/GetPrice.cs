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
            PositiveResponseTemplate.Add("{slot:Name} have a starting price of {price}");
            PositiveResponseTemplate.Add("{slot:Make} {slot:Name} have a starting price of {price}");

            NegativeResponseTemplate.Add("I don't know");

            Response["price"] = "styles[0].price.baseMSRP";

            EdmundsUrlTemplate = "https://api.edmunds.com/api/vehicle/v2/{slot:Make}/{slot:Name}/{slot:Year}/styles?view=full&fmt=json&api_key=67t7jtrnvz8wyzgfpwgcqa3y";

        }
    }
}