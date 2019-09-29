using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarTrainer.AutoComposer
{

    /**
     * コード1つ分の情報を含むクラス
     */
    public class Chord
    {

        public enum ChordTypes
        {
            TONIC=1,
            SUB_DOMINANT,
            DOMINANT
        };

        public enum ThirdNoteTypes 
        {
            MAJOR=1,
            MINOR,
            NONE
        };

        public enum FifthNoteTypes
        {
            SHARPED = 1,
            FLATTED,
            NORMAL,
            NONE
        };

        public enum SeventhNoteTypes
        {
            MAJOR = 1,
            MINOR,
            DOUBLE_FLATTED,
            NONE
        };

        protected Key key;
        protected short rootNote;
        protected short baseNote;
        protected ThirdNoteTypes thirdNoteType;
        protected FifthNoteTypes fifthNoteType;
        protected SeventhNoteTypes seventhNoteType;
        protected short tensionNote;


        /**
         * オブジェクトの初期化を行う
         * <param name="rootNote">度数</param>
         */
        public Chord(Key key, short rootNote)
        {
            this.key = key;
            this.rootNote = rootNote;
            baseNote = rootNote;
            thirdNoteType = ThirdNoteTypes.NONE;
            fifthNoteType = FifthNoteTypes.NONE;
            seventhNoteType = SeventhNoteTypes.NONE;
            tensionNote = 0;
        }

        public short RootNote
        {
            get { return rootNote; }
            set { rootNote = value; }
        }

        public short BaseNote
        {
            get { return baseNote; }
            set { baseNote = value; }
        }

        public ThirdNoteTypes ThirdNoteType
        {
            get { return thirdNoteType; }
            set { thirdNoteType = value; }
        }

        public FifthNoteTypes FifthNoteType
        {
            get { return fifthNoteType; }
            set { fifthNoteType = value; }
        }

        public SeventhNoteTypes SeventhNoteType
        {
            get { return seventhNoteType; }
            set { seventhNoteType = value; }
        }

        public short TensionNote
        {
            get { return tensionNote; }
            set { tensionNote = value; }
        }


        public string GetChordName()
        {
            string rootNoteName = GetRootNoteName();
            string chordName = rootNoteName;

            //ディミニッシュかどうかの判定
            if(thirdNoteType == ThirdNoteTypes.MINOR &&
               fifthNoteType == FifthNoteTypes.FLATTED &&
               seventhNoteType == SeventhNoteTypes.DOUBLE_FLATTED) {
                   return chordName + "dim";
            }

            //3度の判定
            if(thirdNoteType == ThirdNoteTypes.MINOR) {
                chordName += "m";
            }

            //5度の判定(1)
            if (fifthNoteType == FifthNoteTypes.SHARPED)
            {
                chordName += "aug";
            }

            //7度の判定
            if (seventhNoteType == SeventhNoteTypes.MAJOR)
            {
                chordName += "M7";
            }
            else if (seventhNoteType == SeventhNoteTypes.MINOR)
            {
                chordName += "7";
            }
            else if (seventhNoteType == SeventhNoteTypes.DOUBLE_FLATTED)
            {
                chordName += "6";
            }

            //5度の判定(2)
            if (fifthNoteType == FifthNoteTypes.FLATTED)
            {
                chordName += " -5";
            }

            //テンション

            //ベース音がルートと異なる場合はオンコード

            return chordName;
        }


        public string GetRootNoteName()
        {
            return key.GetNoteNameByDegree(this.rootNote);
        }

        public string GetBaseNoteName()
        {
            return "";
        }

        public string Get3rdNoteName()
        {
            return "";
        }

        public string Get5thNoteName()
        {
            return "";
        }

        public string Get7thNoteName()
        {
            return "";
        }

        public string GetTensionNoteName()
        {
            return "";
        }
    }
}
