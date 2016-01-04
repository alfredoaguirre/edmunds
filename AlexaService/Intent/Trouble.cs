using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexaService.Intent
{
    public class Trouble : IntentBase
    {
        public Trouble()
        {
            Name = "";
            PositiveResponseTemplate.Add("");

            //Encountered in the URL fails or car combo is unknown. Not prompting the user for further information
            NegativeResponseTemplate.Add("");

            ErrorSlotResponse[""] = "";
            FollowingQuestiestionMissingSlot[""] = "";
        }
    }
}