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
            PositiveResponseTemplate.Add("{slot:Name} have {highwayMpg}");
            PositiveResponseTemplate.Add("{slot:Make} {slot:Name} have {highwayMpg}");

            NegativeResponseTemplate.Add("I don't know");

            Response["highwayMpg"] = "styles[0].MPG.highway";
            Response["cityyMpg"] = "styles[0].MPG.city";

            ErrorSlotResponse["Name"] = "I don't know the name of th car";
            ErrorSlotResponse["Make"] = "I don't know the name of th Make";

            EdmundsUrlTemplate = "https://api.edmunds.com/api/vehicle/v2/{slot:Make}/{slot:Name}/{slot:Year}/styles?view=full&fmt=json&api_key=67t7jtrnvz8wyzgfpwgcqa3y";

        }
    }
}