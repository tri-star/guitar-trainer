using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarTrainer.AutoComposer
{
    public class ChordChangeHandlerBaseLine : IChordChangeHandler
    {
        protected FlatSecondFilter filter;
        protected Song song;

        protected short srcBarIndex;

        protected Chord orgChord;

        public ChordChangeHandlerBaseLine(FlatSecondFilter filter, Song song, short srcBarIndex, Chord orgChord)
        {
            this.filter = filter;
            this.song = song;
            this.srcBarIndex = srcBarIndex;
            this.orgChord = orgChord;
        }


        #region IChordChangeHandler �����o

        public void OnChordChange(short index)
        {
            if(!filter.modifyChord(song, srcBarIndex)) {
                //�R�[�h���ύX�ł��Ȃ������ꍇ��
                //���̃R�[�h�ɖ߂�
                song.GetBarAt(srcBarIndex).Chord = orgChord;
            }
        }

        #endregion
    }
}
