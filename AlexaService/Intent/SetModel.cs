using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaService.Intent
{
    public class SetModel : IntentBase
    {
        public SetModel()
        {
            Name = "SetModel";
            PositiveResponseTemplate.Add("the seleced year is {slot:Year} ");

            NegativeResponseTemplate.Add("there is no selected year");
        }
    }
}
