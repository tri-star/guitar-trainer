using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace GuitarTrainer
{
    public abstract class ControllerEffectBase : IControllerEffect
    {
        protected Control control;
        protected int step = 0;
        protected int stepEnd = 0;


        protected int srcX;
        protected int srcY;
        protected int destX;
        protected int destY;
        protected int srcWidth;
        protected int srcHeight;
        protected int destWidth;
        protected int destHeight;

        public ControllerEffectBase(Control target, int stepCount)
        {
            control = target;
            stepEnd = stepCount;
            step = 0;
            srcX = control.Left;
            srcY = control.Top;
            srcWidth = control.Width;
            srcHeight = control.Height;
            destX = srcX;
            destY = srcY;
            destWidth = srcWidth;
            destHeight = srcHeight;
        }


        #region ITickHandler メンバ

        public void OnTick(object sender, EventArgs arg)
        {
            if (IsEnd())
            {
                this.OnEnd();
                return;
            }

            if (step == 0)
            {
                this.OnInit();
            }
            else
            {
                this.OnAnimate((double)step / stepEnd);
            }

            //control.Invalidate();
            step++;
            if (IsEnd())
            {
                this.OnEnd();
                return;
            }
        }

        public bool IsEnd()
        {
            return (step >= stepEnd);
        }

        #endregion

        public abstract void OnInit();

        public abstract void OnAnimate(double position);

        public abstract void OnEnd();
    }
}
