using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GuitarTrainer
{
    public interface ITickHandler
    {
        void OnTick(Object sender, EventArgs arg);
        Boolean IsEnd();

    }
}
