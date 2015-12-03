using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaService.Cache
{
    public static class CacheManager
    {
        public static Dictionary<string, string> Slots { get; private set; }
        static CacheManager()
        {
            Slots = new Dictionary<string, string>();
        }

        public static void AddSlots(Dictionary<string, string> slots)
        {
            slots.Where(x => !string.IsNullOrEmpty(x.Value)).ToList().ForEach(x => Slots[x.Key] = x.Value);
        }
    }
}
