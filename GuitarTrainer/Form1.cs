using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GuitarTrainer.AutoComposer;
using System.Text.RegularExpressions;

/**
 * TODO: ドミナントフィルタが適用されたコードに対し、フィルターによってコードが変更された時、
 *       変換後のコードに対して再度フィルタしなければならない
 * 
 */
namespace GuitarTrainer
{
    public partial class Form1 : Form
    {
        protected Song song;

        protected int errorMessageStatus = 0;
        protected List<ITickHandler> tickHandlers;


        public Form1()
        {
            song = new Song(8);
            InitializeComponent();

            tickHandlers = new List<ITickHandler>();

            Application.Idle += this.OnIdle;
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            paintScore(e.Graphics);
        }


        public void OnIdle(Object sender, EventArgs arg)
        {

            foreach (ITickHandler handler in tickHandlers)
            {
                handler.OnTick(sender, arg);
            }

            for (int i = 0; i < tickHandlers.Count; i++)
            {
                if (tickHandlers[i].IsEnd())
                {
                    tickHandlers.RemoveAt(i);
                    i--;
                }
            }
        }


        protected void paintScore(Graphics g)
        {
            if (!song.IsInitialized())
                return;

            int renderY = 10;
            int renderX = 10;
            Point point = new Point();
            Point pointEnd = new Point();

            Font font = new Font("MSゴシック", 10);

            //キーの表示
            point = new Point();
            point.X = renderX;
            point.Y = renderY;
            g.DrawString("Key = " + song.Key.GetKeyName(), font, Brushes.Black, point);

            for (short i = 0; i < song.GetBarCount(); i++)
            {
                renderX = ((i % 4) * 100) + 10;
                renderY = ((i / 4) * 50) + 50;

                point.X = renderX;
                point.Y = renderY;
                pointEnd.X = renderX;
                pointEnd.Y = renderY + 10;
                g.DrawLine(Pens.Black, point, pointEnd);

                point.X = renderX + 100;
                point.Y = renderY;
                pointEnd.X = renderX + 100;
                pointEnd.Y = renderY + 10;
                g.DrawLine(Pens.Black, point, pointEnd);

                point.X = renderX;
                point.Y = renderY + 10;
                pointEnd.X = renderX + 100;
                pointEnd.Y = renderY + 10;
                g.DrawLine(Pens.Black, point, pointEnd);

                //コード名
                point.X = renderX + 5;
                point.Y = renderY - 10;
                g.DrawString(song.GetBarAt(i).Chord.GetChordName(), font, Brushes.Black, point);
                
            }

            font.Dispose();
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<String> errors = new List<String>();
            if (!validateInput(errors))
            {
                errorMessage.Text = "";
                foreach(String errorStr in errors)
                {
                    errorMessage.Text += errorStr + "\n";
                }

                ControllerEffect.SlideDown effect = new ControllerEffect.SlideDown(ErrorMessagePanel, 150, 50);
                tickHandlers.Add(effect);
                return;
            }


            loadPreferenceFromInput();

            song = new Song( Int16.Parse(Preference.Get("compose.barCount")) );

            ChordTypeMapper chordTypeMapper = new ChordTypeMapper();
            chordTypeMapper.Map(song);

            ComposeSettings composeSetting = getComposeSettingFromPreference();
            ChordMapper chordMapper = new ChordMapper(composeSetting);
            chordMapper.Map(song);

            FlatSecondFilter flatSecondFilter = new FlatSecondFilter(Int16.Parse(Preference.Get("base_line_filter.frequency")));
            flatSecondFilter.Run(song);

            DominantFilter dominantFilter = new DominantFilter(Int16.Parse(Preference.Get("dominant_filter.frequency")));
            dominantFilter.Run(song);

            canvas.Invalidate();

        }


        private ComposeSettings getComposeSettingFromPreference()
        {
            ComposeSettings composeSetting = new ComposeSettings();

            if ((Preference.Get("compose.randomKeyEnable") == "1")) {
                composeSetting.randomKeyEnable = true;
                composeSetting.minorKeyEnable = (Preference.Get("compose.minorKeyEnable") == "1");
                composeSetting.sharpedKeyEnable = (Preference.Get("compose.sharpedKeyEnable") == "1");
            }
            else
            {
                composeSetting.keyDegree = Int16.Parse(Preference.Get("compose.keyDegree"));
                composeSetting.minorKeyFlag = (Preference.Get("compose.minorKeyFlag") == "1");
            }
            composeSetting.substituteChordEnable = (Preference.Get("compose.substituteChordEnable") == "1");

            return composeSetting;
        }


        private void loadPreferenceFromInput()
        {
            String checkedVal = new Int32Converter().ConvertToString(1);
            String uncheckedVal = new Int32Converter().ConvertToString(0);

            if (this.keyUseRandom.Checked)
            {
                Preference.Set("compose.randomKeyEnable", checkedVal);
                Preference.Set("compose.minorKeyEnable", (this.minorKeyEnable.Checked) ? checkedVal : uncheckedVal);
                Preference.Set("compose.sharpedKeyEnable", (this.sharpKeyEnable.Checked) ? checkedVal : uncheckedVal);
            }
            else if (this.keyUseFixed.Checked)
            {
                Key keyTest = Key.CreateKeyFromString(fixedKeyDegree.Text);

                Preference.Set("compose.randomKeyEnable", uncheckedVal);
                Preference.Set("compose.keyDegree", keyTest.Degree.ToString());
                Preference.Set("compose.minorKeyFlag", keyTest.MinorFlag ? "1" : "0");
            }
            Preference.Set("compose.substituteChordEnable", (this.substituteEnable.Checked) ? checkedVal : uncheckedVal);
            Preference.Set("compose.barCount", this.barCount.Text);

            Preference.Set("dominant_filter.frequency", dominantFilterFreq.Text);
            Preference.Set("base_line_filter.frequency", baseLineFilterFreq.Text);
            //baseLineFilterFreq
            Preference.Set("compose.barCount", this.barCount.Text);
        
        }


        private Boolean validateInput(List<String> errors)
        {
            errors.Clear();

            Regex reg = new Regex("^[0-9]+$");
            if (!reg.Match(barCount.Text).Success)
            {
                errors.Add("小節数は数値で入力してください。");
            }

            if(keyUseFixed.Checked)
            {
                Key keyTest = Key.CreateKeyFromString(fixedKeyDegree.Text);
                if (keyTest == null)
                {
                    errors.Add("キー名「" + fixedKeyDegree.Text + "」は無効です。");
                }
                else if (keyTest.MinorFlag)
                {
                    errors.Add("現在マイナーキーはサポートされていません。");
                }
            }

            return (errors.Count == 0);
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            //ウィンドウのリサイズに合わせて出力ペインのサイズ調整
            canvas.Width = this.Width - canvas.Left - 40;
            canvas.Height = this.Height - canvas.Top - 40;
        }

        private void errorMessageCloseButton_Click(object sender, EventArgs e)
        {
            ControllerEffect.SlideUp effect = new ControllerEffect.SlideUp(ErrorMessagePanel, 50);
            tickHandlers.Add(effect);
        }
    }
}
