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
            PositiveResponseTemplate.Add("Car Details encountered trouble. Goodbye.");

           
        }
    }
}