using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EdmundsService
{
    static public class Paths
    {
        public static string byModel = "vehicle/v2/{make}/{model}";
        public static string byMaker = "vehicle/v2/{make}/models";
        public static string byCountMaker = "vehicle/v2/{make}/models/count";
        public static string byMakeModelYear = "vehicle/v2/{make}/{model}/{year}";
    }

    public static class EndPointsManager
    {
        public static List<EndPoint> EndPoints = new List<EndPoint>();
        static EndPointsManager()
        {
            foreach (var property in typeof(Paths).GetFields())
            {
                var path = (string)property.GetValue(null);
                var endPoint = new EndPoint(path);
                EndPoints.Add(endPoint);
            }
        }
    }

    public class EndPoint
    {
        public List<string> argNames = new List<string>();
        public string path;
        public EndPoint(string path)
        {
            this.path = path;
            var matches = Regex.Matches(path, @"\{(\w+)\}");
            foreach (var match in matches)
            {
                argNames.Add(((Match)match).Value);
            }
        }
    }

    static public class Args
    {
        public static string url = "http://api.edmunds.com/api/";
        public static string ApiKey = "api_key=67t7jtrnvz8wyzgfpwgcqa3y";
        public static string jsonFormat = "fmt=json";
    }
}