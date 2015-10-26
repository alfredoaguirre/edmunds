using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdmundsClient
{
    public static class EndPointsManager
    {
        public static string url = "http://api.edmunds.com/api/";
        public static string ApiKey = "api_key=67t7jtrnvz8wyzgfpwgcqa3y";
        public static string jsonFormat = "fmt=json";

        public static List<EndPoint> EndPoints = new List<EndPoint>();
        static EndPointsManager()
        {
            foreach (var property in typeof(Paths).GetFields())
            {
                var path = (string)property.GetValue(null);
                var endPoint = new EndPoint(path);
                EndPoints.Add(endPoint);
            }
            EndPoints.Sort();
        }

        static public string GetPath(Dictionary<string, string> InputArgs)
        {
            var endPoint = GetEndPoint(new List<string>(InputArgs.Keys));
            StringBuilder fullePath = new StringBuilder();
            fullePath.Append(url);
            fullePath.Append(endPoint.GetPath(InputArgs));
            fullePath.Append("?");
            fullePath.Append(jsonFormat);
            fullePath.Append("&");
            fullePath.Append(ApiKey);
            return fullePath.ToString();
        }

        static public EndPoint GetEndPoint(Dictionary<string, string> InputArgs)
        {
            return GetEndPoint(new List<string>(InputArgs.Keys));
        }

        static public EndPoint GetEndPoint(List<string> argNames)
        {
            int hashCode = 0;
            foreach (var arg in new List<string>(argNames))
            {
                hashCode += (arg.GetHashCode() * 17);
            }
            return EndPoints.FirstOrDefault(x => x.GetHashCode() == hashCode);
        }
    }
}
