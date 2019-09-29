using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarTrainer.AutoComposer
{
    public class ChordMapper
    {
        protected ComposeSettings composeSetting;
        protected Random random;

        public ChordMapper(ComposeSettings preference)
        {
            this.composeSetting = preference;
            random = new Random();
        }


        public void Map(Song song)
        {
            Bar bar = null;
            int randomVal = 0;
            short barCount = song.GetBarCount();

            //キーを決定する
            short keyDegree = 0;
            bool minorKeyFlag = false;

            if (composeSetting.randomKeyEnable)
            {
                //キーをランダム指定した場合
                if (composeSetting.minorKeyEnable)
                {
                    minorKeyFlag = (random.Next(2) == 0) ? true : false;
                }
                if (!composeSetting.sharpedKeyEnable)
                {
                    //#/bしたキーが許可されていない場合はCのダイアトニックから選択
                    List<short> cDiatonicList = Key.GetCDiatonicDegreeList();
                    keyDegree = cDiatonicList[random.Next(cDiatonicList.Count)];
                }
                else
                {
                    keyDegree = (short)(random.Next(12)+1);
                }
            }
            else
            {
                minorKeyFlag = composeSetting.minorKeyFlag;
                keyDegree = composeSetting.keyDegree;
            }
            Key key = new Key(keyDegree, minorKeyFlag);
            song.Key = key;


            for (short i = 0; i < barCount; i++)
            {
                bar = song.GetBarAt(i);

                //コードを選択する
                if (random.Next(2) == 0 && composeSetting.substituteChordEnable)
                {
                    //代理コードが有効な場合
                    bar.Chord = GetSubstituteChord(key, bar.ChordType);
                }
                else
                {
                    bar.Chord = GetFixedChord(key, bar.ChordType);
                }
            }
        }

        /**
         * コードの種類(T/SD/D)に応じて決められたコードを返す。
         * <param name="key">キー</param>
         * <param name="chordType">コードの種類</param>
         * <returns>コード</returns>
         */
        protected Chord GetFixedChord(Key key, Chord.ChordTypes chordType)
        {
            short degree = 0;
            switch (chordType)
            {
                case Chord.ChordTypes.TONIC:
                    degree = key.GetTonicChordDegree();
                    break;
                case Chord.ChordTypes.SUB_DOMINANT:
                    degree = key.GetSubDominantChordDegree();
                    break;
                case Chord.ChordTypes.DOMINANT:
                    degree = key.GetDominantChordDegree();
                    break;
            }

            Chord chord = key.GetChordByDegree(degree);
            return chord;
        }


        /**
         * コードの種類(T/SD/D)に応じて代理コードを返す。
         * <param name="key">キー</param>
         * <param name="chordType">コードの種類</param>
         * <returns>コード</returns>
         */
        protected Chord GetSubstituteChord(Key key, Chord.ChordTypes chordType)
        {
            short degree = 0;
            List<short> degreeList;
            int listCount = 0;

            switch (chordType)
            {
                case Chord.ChordTypes.TONIC:
                    degreeList = key.GetTonicChordDegreeList();
                    listCount = degreeList.Count;
                    degree = degreeList[random.Next(listCount)];
                    break;
                case Chord.ChordTypes.SUB_DOMINANT:
                    degreeList = key.GetSubDominantChordDegreeList();
                    listCount = degreeList.Count;
                    degree = degreeList[random.Next(listCount)];
                    break;
                case Chord.ChordTypes.DOMINANT:
                    degreeList = key.GetDominantChordDegreeList();
                    listCount = degreeList.Count;
                    degree = degreeList[random.Next(listCount)];
                    break;
            }

            Chord chord = key.GetChordByDegree(degree);
            return chord;
        }
    }
}
