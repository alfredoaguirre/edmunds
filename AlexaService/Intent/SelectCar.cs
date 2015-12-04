using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaService.Intent
{
    public class SelectCar : IntentBase
    {
        public SelectCar()
        {
            Name = "SelectCar";
            PositiveResponseTemplate.Add("The {slot:Model} manufactured by {slot:Make} was first made in {yearStart}. The last year the {slot:Model} was made was in {yearLast}.");
            PositiveResponseTemplate.Add("The {slot:Make} {slot:Model} was first introduced in {yearStart}. The last time a {slot:Model} was rolled out was in {yearLast}.");
            
            //Encountered in the URL fails or car combo is unknown. Not prompting the user for further information
            NegativeResponseTemplate.Add("I don't have that car in my records. Hm try again later.");

            Response["yearStart"] = "styles[0].Year Start";
            Response["yearLast"] = "styles[0].Year Last Proceed";
            
            //Encountered if the user starts app without declaring both make and model
            ErrorSlotResponse["Make"] = "Who manufacturer of the car?";
            ErrorSlotResponse["Model"] = "Which model are you interested in?";



            FollowingQuestiestionMissingSlot["slot:Name"] = "pleace tell me ";

            EdmundsUrlTemplate = https://api.edmunds.com/api/vehicle/v2/{slot:Make}/{slot:Model}?view=basic&fmt=json&api_key=67t7jtrnvz8wyzgfpwgcqa3y
        }
    }
}
