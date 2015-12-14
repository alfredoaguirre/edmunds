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
            Name = "SelectMake";
            PositiveResponseTemplate.Add("The selected make is {slot:Make} ");

            NegativeResponseTemplate.Add("There is no selected manufacturer");
        }
    }
}
