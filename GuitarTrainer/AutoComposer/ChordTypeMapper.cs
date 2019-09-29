using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarTrainer.AutoComposer
{
    /**
     * 小節(Bar)に対しTonic,Sub Dominant, Dominantの分類を設定するクラス
     */
    class ChordTypeMapper
    {
        protected ComposeSettings preference;

        public ChordTypeMapper()
        {
        }


        public void Map(Song song)
        {
            Bar bar = null;
            Random random = new Random();
            int randomVal = 0;
            short barCount = song.GetBarCount();
            Chord.ChordTypes oldType = Chord.ChordTypes.DOMINANT;

           for (short i = 0; i < barCount; i++)
            {
                bar = song.GetBarAt(i);

                //T/SD/Dへの振り分け処理。この部分を調整可能にすると個性が出せるかもしれない
                if (oldType == Chord.ChordTypes.DOMINANT || i == barCount-1)
                {
                    bar.ChordType = Chord.ChordTypes.TONIC;
                }
                else {
                    randomVal = random.Next(3);
                    switch (randomVal)
                    {
                        case 0:
                            bar.ChordType = Chord.ChordTypes.TONIC;
                            break;
                        case 1:
                            bar.ChordType = Chord.ChordTypes.SUB_DOMINANT;
                            break;
                        case 2:
                            bar.ChordType = Chord.ChordTypes.DOMINANT;
                            break;
                    }
                }
                oldType = bar.ChordType;
            }
        }

    }
}
