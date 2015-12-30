using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaService.Cache
{
    public static class CacheManager
    {
        public static Dictionary<string, string> Slots { get; private set; }
        public static Stack<Intent.IntentBase> Intent { get; private set; }
        static CacheManager()
        {
            Slots = new Dictionary<string, string>();
            Intent = new Stack<Intent.IntentBase>();
        }
        public static void Clean()
        {
            Slots.Clear();
        }

        public static void AddSlots(Dictionary<string, string> slots)
        {
            foreach (var d in slots)
            {
                Console.WriteLine("+" + d.Key + "+-  " + d.Value);
            }
            slots.Where(x => !string.IsNullOrEmpty(x.Value)).ToList().ForEach(x => Slots[x.Key] = x.Value);
            foreach (var d in Slots)
            {
                Trace.TraceInformation (d.Key + "-  " + d.Value);
            }
        }
    }
}
