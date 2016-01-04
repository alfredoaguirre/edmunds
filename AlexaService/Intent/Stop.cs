using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaService.Intent
{
    class Stop : IntentBase
    {
        public Stop()
        {
            Name = "Cancel";
            PositiveResponseTemplate.Add("");
            PositiveResponseTemplate.Add("");

            //Encountered in the URL fails or Price is unknown. Not prompting the user for further information
            NegativeResponseTemplate.Add("");

            Response[""] = "";

            //Encountered if the user starts app without declaring both make and model
            ErrorSlotResponse[""] = "";
            ErrorSlotResponse[""] = "";
            ErrorSlotResponse[""] = "";

            FollowingQuestiestionMissingSlot[""] = "";

            EdmundsUrlTemplate = "";

        }
    }
}

