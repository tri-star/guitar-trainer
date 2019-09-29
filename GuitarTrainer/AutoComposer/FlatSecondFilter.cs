using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarTrainer.AutoComposer
{
    /**
     * 前後のコード間の度数の差が完全1度の場合に、裏コード(bII7)への置き換えを行うフィルター
     */ 
    public class FlatSecondFilter
    {
        protected int frequency;


        public FlatSecondFilter(int frequency)
        {
            this.frequency = frequency;
        }

        public void Run(Song song)
        {
            RandomManager randomManager = RandomManager.GetInstance();
            Chord orgChord;
            List<short> modifiedBars = new List<short>();
            ChordChangeHandlerBaseLine observer;

            for (short i = 1; i < song.GetBarCount()-1; i++)
            {
                if(modifiedBars.Contains(i))
                {
                    continue;
                }

                if (randomManager.GetObject().Next(100) > frequency)
                {
                    continue;
                }

                orgChord = song.GetBarAt(i).Chord;

                if (!modifyChord(song, i))
                {
                    continue;
                }

                //前後の小節(Bar)のコードが他のフィルターに変更された時のために
                //Observerを登録
                observer = new ChordChangeHandlerBaseLine(this, song, i, orgChord);
                song.GetBarAt((short)(i - 1)).AddChordChangeHandler(observer);
                song.GetBarAt((short)(i + 1)).AddChordChangeHandler(observer);


                modifiedBars.Add(i);
                modifiedBars.Add((short)(i + 1));
            }
        }


        public bool modifyChord(Song song, short index)
        {
            if(index < 1 || index >= song.GetBarCount()) {
                return false;
            }

            RandomManager randomManager = RandomManager.GetInstance();
            Bar nextBar;
            Bar currentBar;
            Bar prevBar;
            Chord nextChord;
            Chord prevChord;
            Chord newChord;
            short modifiedDegree = 0;

            nextBar = song.GetBarAt((short)(index + 1));
            if (nextBar == null)
            {
                return false;
            }
            prevBar = song.GetBarAt((short)(index - 1));
            if (prevBar == null)
            {
                return false;
            }
            nextChord = nextBar.Chord;
            prevChord = prevBar.Chord;

            if (nextChord.BaseNote - prevChord.BaseNote == 2)
            {
                modifiedDegree = (short)(prevChord.BaseNote + 1);
            }
            else if (prevChord.BaseNote - nextChord.BaseNote == 2)
            {
                modifiedDegree = (short)(nextChord.BaseNote + 1);
            }
            else
            {
                return false;
            }

            newChord = new Chord(song.Key, modifiedDegree);
            newChord.BaseNote = song.GetBarAt(index).Chord.RootNote;
            newChord.RootNote = newChord.BaseNote;
            newChord.ThirdNoteType = Chord.ThirdNoteTypes.MAJOR;
            newChord.FifthNoteType = Chord.FifthNoteTypes.NORMAL;
            newChord.SeventhNoteType = Chord.SeventhNoteTypes.MINOR;

            song.GetBarAt(index).Chord = newChord;

            return true;
        }

    }
}
