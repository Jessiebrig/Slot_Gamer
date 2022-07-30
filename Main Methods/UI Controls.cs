using Slot_Gamer.GameModel;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Slot_Gamer
{
    public partial class Main
    {
        private void Tbx_PlayCycle_TextChanged(object sender, EventArgs e)
        {
            playcycle = ParseTB(Tbx_PlayCycle);
        }
        private void Tbx_Sleep_TextChanged(object sender, EventArgs e)
        {
            sleep = ParseTB(Tbx_Sleep);
        }
        private void Tbx_Add_TextChanged(object sender, EventArgs e)
        {
            add = ParseTB(Tbx_Add);
        }

        private void Tbx_Min_TextChanged(object sender, EventArgs e)
        {
            min = ParseTB(Tbx_Min);
        }

        private void Tbx_Max_TextChanged(object sender, EventArgs e)
        {
            max = ParseTB(Tbx_Max);
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.FocusedItem == null) return;
            int f = listView1.FocusedItem.Index;
            pbCaptured.Image = ImgLister.imagearray[f];
        }
        public void Clear()
        {
            listView1.Invoke(new Action(() => listView1.Items.Clear()));
        }

        string text;
        int number_;
        public int ParseTB(TextBox textBox)//Convert TextBox.Text to int and return
        {
            main.Invoke(new Action(() =>
            {
                text = textBox.Text;
                Parse(text);
            }));
            return number_;
        }
        public int Parse(string string_)//Convert string to int and return
        {
            try
            {
                //LOG("Parsing string: " + string_);
                string[] num = new string[] { string_ };
                text = num.Last();
                text = text.Trim();
                number_ = int.Parse(text);
            }
            catch (FormatException) { LOG("Error while while parsing string: " + text); }
            return number_;
        }

        public string timemillisec()
        {
            DateTime dt = DateTime.Now;
            int ms = dt.Millisecond;
            string datetime = dt.ToString();
            string t = datetime.Remove(datetime.Length - 3) + "." + ms.ToString();
            string[] time = t.Split(' ');
            return time[1];
        }
        public static void LOG(string log)
        {
            main.Invoke(new Action(() =>
            {
                DateTime dt = DateTime.Now;
                int ms = dt.Millisecond;
                string datetime = dt.ToString();
                main.Tbx_LOG.AppendText(main.timemillisec() + "    " + log + Environment.NewLine);
                //main.Tbx_LOG.ScrollToCaret();
            }));
        }
    }
}
