using System;
using System.Collections.Generic;
using System.Linq;
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
            EndPoints.Sort();
        }
        static public  EndPoint GetEndPoint(Dictionary<string, string> InputArgs)
        {
            return GetEndPoint(new List<string>(InputArgs.Keys));
        }
        static public EndPoint GetEndPoint(List<string> argNames)
        {
            int hashCode = 0;
            foreach (var arg in new List<string>(argNames))
            {
                hashCode +=( arg.GetHashCode() * 17);
            }
            return EndPoints.FirstOrDefault(x => x.GetHashCode() == hashCode);
        }
    }

    public class EndPoint : IComparable<EndPoint>
    {
        public List<string> argNames = new List<string>();
        public string path;
        public EndPoint(string path)
        {
            this.path = path;
            var matches = Regex.Matches(path, @"\{(\w+)\}");
            foreach (var match in matches)
            {
                argNames.Add(((Match)match).Value.TrimStart('{').TrimEnd('}'));
            }
            argNames.Sort();
        }

        public int CompareTo(EndPoint other)
        {
            return this.GetHashCode() - other.GetHashCode();
        }

        public override int GetHashCode()
        {
            int hashCode = 0;

            foreach (var arg in argNames)
            {
                hashCode +=( arg.GetHashCode() * 17);
            }
            return hashCode;
        }
    }

    static public class Args
    {
        public static string url = "http://api.edmunds.com/api/";
        public static string ApiKey = "api_key=67t7jtrnvz8wyzgfpwgcqa3y";
        public static string jsonFormat = "fmt=json";
    }
}