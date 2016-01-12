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
            PositiveResponseTemplate.Add("Car Details Stopping");
            

        }
    }
}

