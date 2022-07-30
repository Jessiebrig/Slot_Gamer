using System;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Slot_Gamer.GameModel;

namespace Slot_Gamer
{
    public partial class Main : Form
    {
        ImgLister imgLister;
        public Game game;
        public static Capturer capturer;
        public static Main main;//for accessing UI like TextBox from other Class

        public Main()
        {
            InitializeComponent();
            main = this;
            listView1.Columns.Add("Images", 650);
        }
        private void Main_Load(object sender, EventArgs e)
        {
            LOG("July 23, 2022 Version.");
            LOG($"(CTRL+) <Left> Spin | <Up> GetMouse XY | <Right> Mouse Click | <R> Get Mouse Pixel Color Value");
            thiswindow = FindWindow(null, "Slot_Gamer");
            RegisterHotKey(thiswindow, 1, (uint)FsModifiers.Control, (uint)Keys.Left);//Auto_SPIN
            RegisterHotKey(thiswindow, 2, (uint)FsModifiers.Control, (uint)Keys.Up);//GET Mouse XY
            RegisterHotKey(thiswindow, 3, (uint)FsModifiers.Control, (uint)Keys.Right);//Mouse Click
            RegisterHotKey(thiswindow, 4, (uint)FsModifiers.Control, (uint)Keys.Down);//Get mouse XY Pixel Value
            RegisterHotKey(thiswindow, 5, (uint)FsModifiers.Control, (uint)Keys.R);//Get mouse XY Pixel Value
            LoadDeafults();
            

        }

        CancellationTokenSource quick;
        private void QuickSS_Click(object sender, EventArgs e)
        {
            if (QuickSS.Text == "QuickSS")
            {
                quick = new CancellationTokenSource();
                QuickSS.Text = "Stop";
                QuickSS.BackColor = Color.Red;
                LeftClick(main.MouseX, main.MouseY);
                AsyncSS(quick.Token);
            }
            else
            {
                QuickSS.Text = "QuickSS";
                QuickSS.BackColor = Color.DeepSkyBlue;
                quick.Cancel();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveImage();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Tbx_LOG.Clear();
            Clear();
        }
        
        private void Btn_Spin_Click(object sender, EventArgs e)
        {
            if (Btn_Spin.Text == "Spin")
            {
                Spin();
            }
            else
            {
                Stop();
            }
        }

        
        private void Btn_Play_Click(object sender, EventArgs e)
        {
            Pnl_Manage.Hide();
        }

        private void Btn_Manage_Click(object sender, EventArgs e)
        {
            Pnl_Manage.Show();
            ReloadEditor();
        }

        private void Btn_Hide_Click(object sender, EventArgs e)
        {
            if (Btn_Hide.Text == "Hide") { Btn_Hide.Text = "Show"; capturer.Hide(); }
            else { Btn_Hide.Text = "Hide"; capturer.Show(); }
        }

        

        private void btnSave_Click_1(object sender, EventArgs e)
        {

        }

        private void Btn_SaveCaptureInfo_Click(object sender, EventArgs e)
        {
            int x = CapturerX; int y = CapturerY;
            int w = CapturerWidth; int h = CapturerHeight;
            string info = $"Details Capturer: {x} {y} {w} {h}";
            Properties.Settings.Default.Configs.Add(info);
            Properties.Settings.Default.Save();
            ReloadEditor();
        }

        private void Btn_SaveConfig_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ConfigArray = Tbx_Editor.Text.Split('\n');
                ConfigArray = ConfigArray.Where(s => s.Trim() != string.Empty).ToArray();
                Properties.Settings.Default.Configs.Clear();
                Tbx_Editor.Clear();
                foreach (string config in ConfigArray)
                {
                    Properties.Settings.Default.Configs.Add(config);
                    Tbx_LOG.Text += config + Environment.NewLine;
                }
                Properties.Settings.Default.Save();
                LOG("Save Successfully..");
            }
            catch (Exception ex) { LOG($"Error Saving.. \n {ex}"); }
            ReloadEditor();
        }

        private void Btn_Reload_Click(object sender, EventArgs e)
        {
            ReloadEditor();
        }

        public void ReloadEditor()
        {
            Tbx_Editor.Clear();
            foreach (string config in Properties.Settings.Default.Configs)
            {
                Tbx_Editor.Text += config + Environment.NewLine;
            }
            LoadSets();
        }

        private void Cmb_Sets_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Set = Cmb_Sets.SelectedItem.ToString();
            string[] data = Set.Split(" ");
            capturer.SetWindow(Parse(data[2]), Parse(data[3]), Parse(data[4]), Parse(data[5]));
        }
        public void LoadSets()
        {
            Cmb_Sets.Items.Clear();
            foreach (string config in Properties.Settings.Default.Configs)
            {
                if (!String.IsNullOrWhiteSpace(config))
                {
                    if (config.Contains("Capturer:"))
                    {
                        string[] array = config.Split(':');
                        string[] details = array[0].Split(' ');//remove Capturer: string
                        Cmb_Sets.Items.Add($"{details[0]} {array[1]}");
                    }
                }
            }
        }

        private void Tbx_Index_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                int index = ParseTB(Tbx_Index);
                FocusItem(listView1, index);
            }
        }
        
        private void Btn_Up_Click(object sender, EventArgs e)
        {
            if (listView1.FocusedItem == null) { FocusItem(listView1, 0); }
            FocusItem(listView1, listView1.FocusedItem.Index - 1);
        }

        private void Btn_Down_Click(object sender, EventArgs e)
        {
            if (listView1.FocusedItem == null) { FocusItem(listView1, 0); }
            FocusItem(listView1, listView1.FocusedItem.Index + 1);
        }
        
        private void Cmb_Games_SelectedIndexChanged(object sender, EventArgs e)
        {
            game.Start(Cmb_Games.SelectedIndex);//Set the Game to be played using index
            LOG($"Playing: {game.GameName}");
        }

        

        private void Btn_Test_Click(object sender, EventArgs e)
        {
            game.LoadGames();
        }
        private void Btn_AutoPlay_Click(object sender, EventArgs e)
        {
            
            if (Btn_AutoPlay.Text == "AutoPlay")
            {
                Autoplay();
            }
            else
            {
                StopAuto();
            }

        }

        private void Btn_CheckValues_Click(object sender, EventArgs e)
        {
            ShowValues();
        }
    }
}
