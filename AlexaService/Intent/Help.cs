using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaService.Intent
{
    public class Help : LaunchRequest
    {
        public Help()
        {
            Name = "Help";
        }
    }
}
