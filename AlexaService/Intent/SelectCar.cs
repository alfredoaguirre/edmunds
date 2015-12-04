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
            PositiveResponseTemplate.Add("Got it {slot:Name}");
            PositiveResponseTemplate.Add("Got it {slot:Make} {slot:Name}");
            PositiveResponseTemplate.Add("Ok");

            Response["name"] = "";

            FollowingQuestiestionMissingSlot["slot:Name"] = "pleace tell me ";
        }
    }
}
