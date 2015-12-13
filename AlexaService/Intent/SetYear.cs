﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaService.Intent
{
   public class SetYear : IntentBase
    {
        public SetYear()
        {
            Name = "SetYear";
            PositiveResponseTemplate.Add("the seleced year is {slot:Year} ");

            //Encountered in the URL fails or car combo is unknown. Not prompting the user for further information
            NegativeResponseTemplate.Add("there is no selected year");

        }
    }
}
