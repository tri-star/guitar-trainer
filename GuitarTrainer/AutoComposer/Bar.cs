using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarTrainer.AutoComposer
{
    /**
     * 1小節分の情報を表すクラス
     */
    public class Bar
    {
        protected Song song;
        protected short barIndex;

        protected Chord.ChordTypes chordType;
        protected Chord chord;

        protected List<IChordChangeHandler> chordChangeHandlers;


        public Bar(Song song, short barIndex)
        {
            chordType = Chord.ChordTypes.TONIC;
            chord = null;
            this.song = song;
            this.barIndex = barIndex;

            chordChangeHandlers = new List<IChordChangeHandler>();
        }

        public Chord.ChordTypes ChordType
        {
            get { return chordType; }
            set { chordType = value; }
        }

        public Chord Chord
        {
            get { return chord; }
            set 
            {
                chord = value;
                foreach (IChordChangeHandler handler in chordChangeHandlers)
                {
                    handler.OnChordChange(barIndex);
                }
            }
        }


        public void AddChordChangeHandler(IChordChangeHandler handler)
        {
            chordChangeHandlers.Add(handler);
        }

    }
}
