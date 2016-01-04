using System;
using System.Collections.Generic;
using System.Linq;

namespace AlexaService.Intent
{
    public static class IntentManager
    {
        public static List<IntentBase> Intents { get; set; }
        static IntentManager()
        {
            Intents = new List<IntentBase>();
            foreach (var i in typeof(IntentManager).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(IntentBase))))
                Intents.Add((IntentBase)Activator.CreateInstance(i));
        }

        public static IntentBase GetIntent(string name)
        {
            return Intents.FirstOrDefault(x => x.Name == name);
        }
    }
}
