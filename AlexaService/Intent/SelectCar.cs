using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaService.Intent
{
    public class SelectCar : IntentBase
    {
        public SelectCar()
        {
            Name = "SelectCar";
            ResponseTemplate = new List<string>()
            {
                "Got it",
                "Ok"
            };
            ResponceSlots = new Dictionary<string, string>()
            {
                {"name", "" }
            };
        }
        public override string GetResponse()
        {
            return base.GetResponse();
        }
    }
}
