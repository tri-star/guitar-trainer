using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarTrainer.AutoComposer
{
    public interface IChordChangeHandler
    {
        void OnChordChange(short index);
    }
}
