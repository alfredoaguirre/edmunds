using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaService.Intent
{
    class Help : IntentBase
    {
        public Help()
        {
            Name = "Help";
            PositiveResponseTemplate.Add("");
            
            //Alfredo, write the code so it sends the user to the "Launch Request" Intent 
        }
    }
}
