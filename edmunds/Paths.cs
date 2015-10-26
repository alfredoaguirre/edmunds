using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdmundsClient
{
    static public class Paths
    {
        public static string byModel = "vehicle/v2/{make}/{model}";
        public static string byMaker = "vehicle/v2/{make}/models";
        public static string byCountMaker = "vehicle/v2/{make}/models/count";
        public static string byMakeModelYear = "vehicle/v2/{make}/{model}/{year}";
    }
}
