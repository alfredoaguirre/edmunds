using AlexaService.Cache;
using System.Linq;

namespace AlexaService.Intent
{
    public class SelectCar : IntentBase
    {
        public SelectCar()
        {
            Name = "SelectCar";
            PositiveResponseTemplate.Add("The {slot:Model} manufactured by {slot:Make} was first made in {yearStart}. The last year the {slot:Model} was made was in {yearLast}.");
            PositiveResponseTemplate.Add("The {slot:Make} {slot:Model} was first introduced in {yearStart}. The last time a {slot:Model} was rolled out was in {yearLast}.");

            //Encountered in the URL fails or car combo is unknown. Not prompting the user for further information
            NegativeResponseTemplate.Add("I don't have that car in my records. Hm try again later.");

            Response["yearStart"] = "years[0].year";
            Response["yearLast"] = "years[-1:].year";

            //Encountered if the user starts app without declaring both make and model
            ErrorSlotResponse["Make"] = "What's the make of the car?";
            ErrorSlotResponse["Model"] = "What's the model of the car?";
            ErrorSlotResponse["Year"] = "What's the year of the car?";


            FollowingQuestiestionMissingSlot["Make"] = "Please tell me who manufactures the car.";
            FollowingQuestiestionMissingSlot["Model"] = "Please tell me the model of the car.";
            FollowingQuestiestionMissingSlot["Year"] = "Please tell me the year of the car.";

            EdmundsUrlTemplate = "api/vehicle/v2/{slot:Make}/{slot:Model}?view=basic&fmt=json";
        }

        /// <summary>
        ///  this will redirec the positove responce to the top of th stack
        /// </summary>
        /// <returns></returns>
        public override string GetPositiveResponseTemplate()
        {
            if (CacheManager.Intent.Count() > 0)
            {
                var oldIntent = CacheManager.Intent.Pop();
                return oldIntent.GetPositiveResponseTemplate();
            }

            return base.GetPositiveResponseTemplate();
        }
    }
}
