using System;
using System.Windows.Forms;

namespace Slot_Gamer
{
    public partial class Main
    {
        int playcycle, sleep, add, min, max;
        public int PlayCycle
        {
            get { return playcycle; }
            set { InvokeTB(Tbx_PlayCycle, value); playcycle = value; }
        }
        public int Sleep
        {
            get { return sleep; }
            set { InvokeTB(Tbx_Sleep, value); sleep = value; }
        }
        public int Add
        {
            get { return add; }
            //get { return ParseTB(Tbx_Add); }
            set { InvokeTB(Tbx_Add, value); add = value; }
        }
        public int Min
        {
            get { return min; }
            //get { return ParseTB(Tbx_Min); }
            set { InvokeTB(Tbx_Min, value); min = value; }
        }
        public int Max
        {
            get { return max; }
            //get { return ParseTB(Tbx_Max); }
            set { InvokeTB(Tbx_Max, value); max = value; }
        }
        public int ImagesCount
        {
            set { 
                main.Invoke(new Action(() =>
                {
                    Lbl_ImgCount.Text = value.ToString();
                }));
            }
        }
        public int CapturerX {
            get { return ParseTB(Tbx_CapturerX); }
            set { Tbx_CapturerX.Text = value.ToString(); }
        }
        public int CapturerY
        {
            get { return ParseTB(Tbx_CapturerY); }
            set { Tbx_CapturerY.Text = value.ToString(); }
        }
        public int CapturerWidth
        {
            get { return ParseTB(Tbx_Width); }
            set { Tbx_Width.Text = value.ToString(); }
        }
        public int CapturerHeight
        {
            get { return ParseTB(Tbx_Height); }
            set { Tbx_Height.Text = value.ToString(); }
        }
        public int MouseX { get; set; }
        public int MouseY { get; set; }
        public void InvokeTB(TextBox textBox, int num)
        {
            main.Invoke(new Action(() =>
            {
                textBox.Text = num.ToString();
            }));
        }

    }
}
