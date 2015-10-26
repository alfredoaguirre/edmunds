using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EdmundsClient
{
    public class EndPoint : IComparable<EndPoint>
    {
        public List<string> ArgNames = new List<string>();
        public string Path;
        public EndPoint(string path)
        {
            this.Path = path;
            var matches = Regex.Matches(path, @"\{(\w+)\}");
            foreach (var match in matches)
            {
                ArgNames.Add(((Match)match).Value.TrimStart('{').TrimEnd('}'));
            }
            ArgNames.Sort();
        }

        public int CompareTo(EndPoint other)
        {
            return GetHashCode() - other.GetHashCode();
        }

        public string GetPath(Dictionary<string, string> argNames)
        {
            string path = this.Path;
            foreach (var arg in argNames)
            {
                path = path.Replace("{" + arg.Key + "}", arg.Value);
            }
            return path;
        }

        public override int GetHashCode()
        {
            int hashCode = 0;

            foreach (var arg in ArgNames)
            {
                hashCode += (arg.GetHashCode() * 17);
            }
            return hashCode;
        }
    }
}