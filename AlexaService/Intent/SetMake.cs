using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaService.Intent
{
    public class SetMake : IntentBase
    {
        public SetMake()
        {
            Name = "SetMake";
            PositiveResponseTemplate.Add("The selected make is {slot:Make}");

            NegativeResponseTemplate.Add("There is no selected manufacturer");
            
            //Encountered if the user starts app without declaring both make and model
            ErrorSlotResponse["Make"] = "What's the make of the car?";
            
            FollowingQuestiestionMissingSlot["Make"] = "Please tell me who manufactures the car.";
        }
    }
}
