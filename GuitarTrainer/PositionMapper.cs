using System;
using System.Collections.Generic;
using System.Text;
using GuitarTrainer.AutoComposer;

namespace GuitarTrainer
{
    /**
     * ギターのフレット、弦の位置情報などを決定するクラス
     */
    public class PositionMapper
    {
        /**
         * 位置情報と、位置に対応する値を格納するオブジェクト
         */
        protected class PositionInfo
        {
            public int position;
            public int value;
        }

        protected List<PositionInfo> positionData;


        public PositionMapper()
        {
            positionData = new List<PositionInfo>();
        }


        /**
         * 位置情報を生成するための初期化を行う
         * <param name="length">全体の長さ(小節数)</param>
         * <param name="rangeMin">値の最小値</param>
         * <param name="rangeMax">値の最大値</param>
         * <param name="interval">位置情報を変更する間隔(単位：小節)</param>
         */
        public void Init(int length, int rangeMin, int rangeMax, int interval)
        {
            positionData.Clear();
            RandomManager randomManager = RandomManager.GetInstance();

            PositionInfo positionInfo;
            for (int i = 0; i < length; i += interval)
            {
                positionInfo = new PositionInfo();
                positionInfo.position = i;
                positionInfo.value = randomManager.GetObject().Next(rangeMin, rangeMax);
                positionData.Add(positionInfo);
            }
        }


        /**
         * 指定された位置(小節)上の値を取得して返す。
         * <param name="position">位置情報(小説番号)</param>
         * <returns>指定された小節上の値</returns>
         */ 
        public int GetValueAt(int position)
        {
            if (positionData.Count < 1)
            {
                return 0;
            }

            int current = positionData[0].value;
            int listLength = positionData.Count;
            for (int i = 0; i < listLength; i++)
            {
                if (positionData[i].position > position)
                {
                    return current;
                }
                current = positionData[i].value;
            }

            return current;
        }

    }
}
