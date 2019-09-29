using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using GuitarTrainer.AutoComposer;

namespace GuitarTrainerTest
{
    public class KeyTest
    {
        [Test(Description="IIm のテスト")]
        public void GetChordByNoteDegreeTest()
        {
            Key key = new Key(3, false);

            Chord chord = key.GetChordByDegree(3);

            Assert.AreEqual(Chord.ThirdNoteTypes.MINOR, chord.ThirdNoteType);
            Assert.AreEqual(Chord.FifthNoteTypes.NORMAL, chord.FifthNoteType);
            Assert.AreEqual(Chord.SeventhNoteTypes.MINOR, chord.SeventhNoteType);
        }

        [Test(Description = "Key=Dの時の短3度")]
        public void GetNoteNameByDegreeTest()
        {
            Key key = new Key(3, false);

            string noteName = key.GetNoteNameByDegree(4);

            Assert.AreEqual("F", noteName);
        }


        [Test(Description = "Key=Dの時の長7度")]
        public void GetNoteNameByDegreeTest2()
        {
            Key key = new Key(3, false);

            string noteName = key.GetNoteNameByDegree(12);

            Assert.AreEqual("C#", noteName);
        }


        [Test(Description = "Em7の表示テスト")]
        public void GetChordName()
        {
            Key key = new Key(3, false);
            Chord chord = key.GetChordByDegree(3);

            
            Assert.AreEqual("Em7", chord.GetChordName());
        }

        [Test(Description = "GM7の表示テスト")]
        public void GetChordName2()
        {
            Key key = new Key(3, false);
            Chord chord = key.GetChordByDegree(6);


            Assert.AreEqual("GM7", chord.GetChordName());
        }

        [Test(Description = "C#dimの表示テスト")]
        public void GetChordName3()
        {
            Key key = new Key(3, false);
            Chord chord = key.GetChordByDegree(12);
            chord.ThirdNoteType = Chord.ThirdNoteTypes.MINOR;
            chord.FifthNoteType = Chord.FifthNoteTypes.FLATTED;
            chord.SeventhNoteType = Chord.SeventhNoteTypes.DOUBLE_FLATTED;

            Assert.AreEqual("C#dim", chord.GetChordName());
        }
    }
}
