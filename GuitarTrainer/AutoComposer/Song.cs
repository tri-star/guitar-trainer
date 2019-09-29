using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarTrainer.AutoComposer
{
    /**
     * 複数の小節のコンテナ。キー情報なども含む
     */
    public class Song
    {
        protected List<Bar> bars;
        protected Key key;


        public Song(short length)
        {
            bars = new List<Bar>();

            length = Math.Max(length, (short)0);
            for (short i = 0; i < length; i++)
            {
                bars.Add(new Bar(this, i));
            }
        }


        public Bar GetBarAt(short index)
        {
            if (index < 0 || index >= bars.Count)
            {
                return null;
            }
            return bars[index];
        }


        public short GetBarCount()
        {
            return (short)bars.Count;
        }


        public Key Key
        {
            get { return key; }
            set { key = value; }
        }

        public bool IsInitialized()
        {
            return (key != null);
        }
    }
}
