using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GuitarTrainer.ControllerEffect
{
    public class SlideUp : ControllerEffectBase
    {
        public SlideUp(Control target, int stepCount)
            : base(target, stepCount)
 
        {
            srcX = target.Left;
            srcY = target.Top;
            srcWidth = target.Width;
            srcHeight = target.Height;
            destX = srcX;
            destY = srcY;
            destWidth = srcWidth;
            this.destHeight = 1;
        }


        public override void OnInit()
        {
            control.Visible = true;

            control.Left = srcX;
            control.Top = srcY;
            control.Width = srcWidth;
            control.Height = srcHeight;
        }


        public override void OnAnimate(double position)
        {
            int newX = srcX,
                newY = srcY,
                newW = srcWidth,
                newH = srcHeight;

            newX = (int)(srcX + (double)((destX - srcX) * position));
            newY = (int)(srcY + (double)((destY - srcY) * position));
            newW = (int)(srcWidth + (double)((destWidth - srcWidth) * position));
            newH = (int)(srcHeight + (double)((destHeight - srcHeight) * position));

            control.Left = newX;
            control.Top = newY;
            control.Width = newW;
            control.Height = newH;
            control.Invalidate();
        }


        public override void OnEnd()
        {
            control.Visible = false;
            control.Invalidate();
            return;
        }

    }
}
