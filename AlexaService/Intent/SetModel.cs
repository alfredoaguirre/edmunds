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
            PositiveResponseTemplate.Add("The selected model is {slot:Model} ");

            NegativeResponseTemplate.Add("There is no selected model");
        }
    }
}
