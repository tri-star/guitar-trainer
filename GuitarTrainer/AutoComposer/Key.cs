using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarTrainer.AutoComposer
{
    /**
     * 曲のキー情報を格納するオブジェクト
     */
    public class Key
    {
        protected short degree;
        protected bool minorFlag;


        public Key(short degree, bool minorFlag)
        {
            this.degree = degree;
            this.minorFlag = minorFlag;
        }

        public short Degree
        {
            get { return degree; }
            set { degree = value; }
        }

        public bool MinorFlag
        {
            get { return minorFlag; }
            set { minorFlag = value; }
        }


        public string GetKeyName()
        {
            string keyDegreeName = GetNoteNameByDegree(1);

            return keyDegreeName + ((minorFlag) ? "m" : "");
        }


        /**
         * 度数からコードオブジェクトを作成して返す
         * <param name="degree">度数</param>
         * <returns>コードオブジェクト</returns>
         */
        public Chord GetChordByDegree(short degree)
        {
            return CreateMajorChord(degree);
        }


        protected Chord CreateMajorChord(short degree)
        {
            Chord chord = new Chord(this, degree);
            if (minorFlag)
            {
                degree -= 3;
                degree = RoundNote(degree);
            }

            switch (degree)
            {
                case 1:  //1st
                case 6:  //4th
                    chord.ThirdNoteType = Chord.ThirdNoteTypes.MAJOR;
                    chord.FifthNoteType = Chord.FifthNoteTypes.NORMAL;
                    chord.SeventhNoteType = Chord.SeventhNoteTypes.MAJOR;
                    break;
                case 3:  //2nd
                case 5:  //3rd
                case 10:  //6th
                    chord.ThirdNoteType = Chord.ThirdNoteTypes.MINOR;
                    chord.FifthNoteType = Chord.FifthNoteTypes.NORMAL;
                    chord.SeventhNoteType = Chord.SeventhNoteTypes.MINOR;
                    break;
                case 8:  //5th
                    chord.ThirdNoteType = Chord.ThirdNoteTypes.MAJOR;
                    chord.FifthNoteType = Chord.FifthNoteTypes.NORMAL;
                    chord.SeventhNoteType = Chord.SeventhNoteTypes.MINOR;
                    break;
                case 12:  //7th
                    chord.ThirdNoteType = Chord.ThirdNoteTypes.MINOR;
                    chord.FifthNoteType = Chord.FifthNoteTypes.FLATTED;
                    chord.SeventhNoteType = Chord.SeventhNoteTypes.MINOR;
                    break;
                default:

                    if (IsValidDegree(RoundNote((short)(degree+3))))
                    {
                        chord.ThirdNoteType = Chord.ThirdNoteTypes.MINOR;
                    }
                    else if (IsValidDegree(RoundNote((short)(degree + 4))))
                    {
                        chord.ThirdNoteType = Chord.ThirdNoteTypes.MAJOR;
                    }
                    if (IsValidDegree(RoundNote((short)(degree + 7))))
                    {
                        chord.FifthNoteType = Chord.FifthNoteTypes.NORMAL;
                    }
                    else if (IsValidDegree(RoundNote((short)(degree + 6))))
                    {
                        chord.FifthNoteType = Chord.FifthNoteTypes.FLATTED;
                    }
                    else if (IsValidDegree(RoundNote((short)(degree + 8))))
                    {
                        chord.FifthNoteType = Chord.FifthNoteTypes.SHARPED;
                    }
                    if (IsValidDegree(RoundNote((short)(degree + 11))))
                    {
                        chord.SeventhNoteType = Chord.SeventhNoteTypes.MAJOR;
                    }
                    else if (IsValidDegree(RoundNote((short)(degree + 10))))
                    {
                        chord.SeventhNoteType = Chord.SeventhNoteTypes.MINOR;
                    }
                    else if (IsValidDegree(RoundNote((short)(degree + 9))))
                    {
                        chord.SeventhNoteType = Chord.SeventhNoteTypes.DOUBLE_FLATTED;
                    }
                    break;
            }
            return chord;
        }


        public short GetTonicChordDegree()
        {
            return 1;
        }

        public short GetSubDominantChordDegree()
        {
            return 6;
        }

        public short GetDominantChordDegree()
        {
            return 8;
        }

        public List<short> GetTonicChordDegreeList()
        {
            List<short> list;
            list = new List<short>();
            list.Add(1);
            list.Add(5);
            list.Add(10);

            return list;
        }


        public List<short> GetSubDominantChordDegreeList()
        {
            List<short> list;
            list = new List<short>();
            list.Add(6);
            list.Add(3);

            return list;
        }


        public List<short> GetDominantChordDegreeList()
        {
            List<short> list;
            list = new List<short>();
            list.Add(8);
            list.Add(12);

            return list;
        }

        public static List<short> GetCDiatonicDegreeList()
        {
            List<short> list;
            list = new List<short>();
            list.Add(1);
            list.Add(3);
            list.Add(5);
            list.Add(6);
            list.Add(8);
            list.Add(10);
            list.Add(12);

            return list;
        }

        /**
         * 指定された度数がキー上で有効かどうかを返す
         * <param name="degree">度数</param>
         * <returns>有効な度数の場合true</returns>
         */
        public bool IsValidDegree(short degree)
        {
            if (minorFlag)
            {
                degree -= 3;
                degree = RoundNote(degree);
            }

            switch(degree)
            {
                case 1: //1st
                case 3: //2nd
                case 5: //3rd
                case 6: //4th
                case 8: //5th
                case 10: //6th
                case 12: //7th
                    return true;
            }
            return false;
        }

        /**
         * 指定された度数に対応する音名を返す。
         * <param name="degree">度数</param>
         * <returns>音名</returns>
         */
        public string GetNoteNameByDegree(short degree)
        {
            short keyDegree = (short)(this.degree-1);
            degree += keyDegree;
            degree = RoundNote(degree);

            switch (degree)
            {
                case  1: return "C";
                case  2: return "C#";
                case  3: return "D";
                case  4: return "Eb";
                case  5: return "E";
                case  6: return "F";
                case  7: return "F#";
                case  8: return "G";
                case  9: return "G#";
                case 10: return "A";
                case 11: return "Bb";
                case 12: return "B";
            }
            return "?";
        }

        /**
         * 12度を越える度数を丸めた値を返す。
         */
        public static short RoundNote(short degree)
        {
            if(degree > 12) {
                degree %= 13;
                degree++;
            }
            if (degree < 1)
            {
                degree += 12;
            }
            return degree;
        }

        /**
         * キー名の文字列からキーを解析して返す。
         */
        public static Key CreateKeyFromString(string keyName)
        {
            short key = 0, cursor=0;
            Boolean isMinor = false;

            //1文字目がA-Gの範囲外の場合は無効
            switch (keyName[cursor])
            {
                case 'C': key = 1; break;
                case 'D': key = 3; break;
                case 'E': key = 5; break;
                case 'F': key = 6; break;
                case 'G': key = 8; break;
                case 'A': key = 10; break;
                case 'B': key = 12; break;
                default: return null;
            }
            cursor++;

            if (keyName.Length == 1)
            {
                return new Key(key, isMinor);
            }

            if (keyName[cursor] == 'b')
            {
                key += Key.RoundNote((short)-1);
                cursor++;
            }
            else if (keyName[cursor] == '#')
            {
                key += Key.RoundNote((short)1);
                cursor++;
            }
            if (keyName.Length == cursor)
            {
                return new Key(key, isMinor);
            }
            if (keyName[cursor] == 'm')
            {
                isMinor = true;
            }
            else
            {
                return null;
            }

            return new Key(key, isMinor);
        }
    }
}
