using System;
using System.Collections.Generic;
using System.Text;
using GuitarTrainer.AutoComposer;

namespace GuitarTrainer
{
    /**
     * �M�^�[�̃t���b�g�A���̈ʒu���Ȃǂ����肷��N���X
     */
    public class PositionMapper
    {
        /**
         * �ʒu���ƁA�ʒu�ɑΉ�����l���i�[����I�u�W�F�N�g
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
         * �ʒu���𐶐����邽�߂̏��������s��
         * <param name="length">�S�̂̒���(���ߐ�)</param>
         * <param name="rangeMin">�l�̍ŏ��l</param>
         * <param name="rangeMax">�l�̍ő�l</param>
         * <param name="interval">�ʒu����ύX����Ԋu(�P�ʁF����)</param>
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
         * �w�肳�ꂽ�ʒu(����)��̒l���擾���ĕԂ��B
         * <param name="position">�ʒu���(�����ԍ�)</param>
         * <returns>�w�肳�ꂽ���ߏ�̒l</returns>
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
