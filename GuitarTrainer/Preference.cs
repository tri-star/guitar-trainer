using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarTrainer
{
    class Preference
    {
        private static Preference instance;

        protected Dictionary<string, string> settings;


        private Preference() 
        {
            settings = new Dictionary<string,string>();
        }


        public static Preference GetInstance()
        {
            if (instance == null)
            {
                instance = new Preference();
            }

            return instance;
        }


        public static String Get(String key)
        {
            Preference pref = Preference.GetInstance();
            return pref.GetInner(key);
        }


        public static void Set(String key, String value)
        {
            Preference pref = Preference.GetInstance();
            pref.SetInner(key, value);
        }


        public String GetInner(String key)
        {
            if (!settings.ContainsKey(key))
            {
                return null;
            }

            return settings[key];
        }


        public void SetInner(String key, String value)
        {
            settings[key] = value;
        }
    }
}
