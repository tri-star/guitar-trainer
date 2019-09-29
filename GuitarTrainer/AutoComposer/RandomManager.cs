using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarTrainer.AutoComposer
{
    public class RandomManager
    {
        static RandomManager instance = null;
        protected Random random;

        protected RandomManager()
        {
            random = new Random();
        }

        public static RandomManager GetInstance()
        {
            if(instance == null)
            {
                instance = new RandomManager();
            }
            return instance;
        }


        public Random GetObject()
        {
            return random;
        }
    }
}
