using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarTrainer.AutoComposer
{
    /**
     * 任意のコードの前にドミナントモーションを挿入するフィルタ
     */
    public class DominantFilter
    {

        protected int frequency;


        public DominantFilter(int frequency)
        {
            this.frequency = frequency;
        }

        public void Run(Song song)
        {
            RandomManager randomManager = RandomManager.GetInstance();
            Bar nextBar;
            Bar currentBar;
            Chord nextChord;
            Chord newChord;
            List<short> modifiedBars = new List<short>();

            for (short i = 1; i < song.GetBarCount()-1; i++)
            {
                if (randomManager.GetObject().Next(100) > frequency)
                {
                    continue;
                }
                if(modifiedBars.Contains(i))
                {
                    continue;
                }

                nextBar = song.GetBarAt((short)(i + 1));
                if (nextBar == null)
                {
                    continue;
                }
                nextChord = nextBar.Chord;

                newChord = new Chord(song.Key, song.GetBarAt(i).Chord.RootNote);
                newChord.RootNote = Key.RoundNote((short)(nextChord.RootNote + 7));
                newChord.BaseNote = song.GetBarAt(i).Chord.RootNote;
                newChord.ThirdNoteType = Chord.ThirdNoteTypes.MAJOR;
                newChord.FifthNoteType = Chord.FifthNoteTypes.NORMAL;
                newChord.SeventhNoteType = Chord.SeventhNoteTypes.MINOR;

                song.GetBarAt(i).Chord = newChord;

                modifiedBars.Add((short)(i + 1));
            }
        }

    }
}
