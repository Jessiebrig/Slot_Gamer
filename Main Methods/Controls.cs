using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Slot_Gamer.GameModel;

namespace Slot_Gamer
{
    public partial class Main
    {
        //
        //Hotkeys
        //
        private IntPtr thiswindow;
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string sClassname, string sAppName);
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        public enum FsModifiers { Alt = 0x0001, Control = 0x0002, Shift = 0x0004, Window = 0x0008, }
        //
        bool HotkeySpin = false;
        bool autoplay = false;
        protected override void WndProc(ref Message keyPressed)
        {
            if (keyPressed.Msg == 0x0312)
            {
                int id = keyPressed.WParam.ToInt32();
                switch (id)
                {
                    case 1://Spin Via Hotkey
                        if (HotkeySpin)//Check if there is a current spin from hotkey
                        {
                            HotkeySpin = false;
                            if (game.hassession)
                            {
                                Stop();
                            }
                        }
                        else
                        {
                            HotkeySpin = true;
                            if (Btn_Spin.Text == "Spin" || game.hassession == false)
                            {
                                Spin();
                            }
                        }
                        break;
                    case 2://Get mouse position
                        GetMouseXY();
                        break;
                    case 3://AutoPlay
                        if (autoplay)//Check if there is a current autoplay from hotkey
                        {
                            autoplay = false;
                            if (autoplaytoken != null)
                            {
                                StopAuto();
                            }
                        }
                        else
                        {
                            autoplay = true;
                            if (Btn_AutoPlay.Text == "AutoPlay" || autoplaytoken == null)
                            {
                                Autoplay();
                            }
                        }
                        break;
                    case 4://
                        //GetColor();
                        LeftClick(MouseX, MouseY);
                        Thread.Sleep(Sleep);
                        LeftClick(MouseX, MouseY);
                        break;
                    case 5://Ctrl + R to get mouseXY color
                        GetColor();
                        break;
                }
            }
            base.WndProc(ref keyPressed);
        }
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
            UnregisterHotKey(thiswindow, 1);
            UnregisterHotKey(thiswindow, 2);
            UnregisterHotKey(thiswindow, 3);
            UnregisterHotKey(thiswindow, 4);
            UnregisterHotKey(thiswindow, 5);
        }
        //
        //Mouse Control
        //
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        public void LeftClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            string leftdown = timemillisec();
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            string leftUp = timemillisec();
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
            LOG($"Click at: {MouseX} by {MouseY} _ < Mouse DOWN > {leftdown} _ < Mouse UP > {leftUp}");
        }
        public void LeftClick(int xpos, int ypos, ImgLister imgLister)
        {
            SetCursorPos(xpos, ypos);
            string leftdown = timemillisec();
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            imgLister.AddSS("=====> Mouse DOWN <=====", true);
            string leftUp = timemillisec();
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
            imgLister.AddSS("=====> Mouse UP <=====", true);
            LOG($"Click W/ SS at: {MouseX} by {MouseY} _ < Mouse DOWN > {leftdown} _ < Mouse UP > {leftUp}");
        }

        public void GetMouseXY()
        {
            main.MouseX = MousePosition.X;
            main.MouseY = MousePosition.Y;
            lblX.Text = main.MouseX.ToString();
            lblY.Text = main.MouseY.ToString();
            LOG($"Mouse XY Set to: {MouseX} by {MouseY}");
        }
        public Color GetColor()
        {
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            int x = MousePosition.X; int y = MousePosition.Y;
            Color color = bitmap.GetPixel(x, y);
            byte[] ARGB = new byte[4];
            ARGB[0] = color.A; ARGB[1] = color.B; ARGB[2] = color.G; ARGB[3] = color.B;
            LOG($"GetColor() Mouse XY: {x} by {y} -- Pixel Color Value: {color}");
            Tbx_Colors.Text = $"{ARGB[0]} {ARGB[1]} {ARGB[2]} {ARGB[3]}";
            return color;
        }
        //
        //
        //
    }
}
