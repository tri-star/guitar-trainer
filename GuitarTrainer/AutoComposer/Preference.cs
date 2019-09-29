using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarTrainer.AutoComposer
{
    public class ComposeSettings
    {
        public bool randomKeyEnable;
        public bool sharpedKeyEnable;
        public bool minorKeyEnable;

        public short keyDegree;
        public bool minorKeyFlag;

        public bool substituteChordEnable;

        public bool randomStringEnable;
        public short stringNo;

        public bool randomFretEnable;
        public short fretNo;
    }
}
