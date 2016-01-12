using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaService.Intent
{
    class Cancel : IntentBase
    {
        public Cancel()
        {
            Name = "Cancel";
            PositiveResponseTemplate.Add("Cancelling");
           

        }
    }
}

