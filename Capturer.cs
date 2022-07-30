using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slot_Gamer
{
    public partial class Capturer : Form
    {
        public Size CapturerSize { get; set; }
        //
        //Moving window by click-drag on a control https://stackoverflow.com/a/13477624/5260872
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        //How to resize a form without a border? https://stackoverflow.com/a/32261547/5260872
        public Capturer()
        {
            InitializeComponent();
            this.Opacity = .7;//Make trasparent
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true); // this is to avoid visual artifaGameToken
            UpdateCapturerInfo();
        }

        private const int
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;

        const int _ = 10; // you can rename this variable if you like

        new Rectangle Top { get { return new Rectangle(0, 0, this.ClientSize.Width, _); } }
        new Rectangle Left { get { return new Rectangle(0, 0, _, this.ClientSize.Height); } }
        new Rectangle Bottom { get { return new Rectangle(0, this.ClientSize.Height - _, this.ClientSize.Width, _); } }
        new Rectangle Right { get { return new Rectangle(this.ClientSize.Width - _, 0, _, this.ClientSize.Height); } }

        Rectangle TopLeft { get { return new Rectangle(0, 0, _, _); } }
        Rectangle TopRight { get { return new Rectangle(this.ClientSize.Width - _, 0, _, _); } }
        Rectangle BottomLeft { get { return new Rectangle(0, this.ClientSize.Height - _, _, _); } }

        private void Capturer_Load(object sender, EventArgs e)
        {

        }

        Rectangle BottomRight { get { return new Rectangle(this.ClientSize.Width - _, this.ClientSize.Height - _, _, _); } }

        private void Capturer1_SizeChanged(object sender, EventArgs e)
        {
            UpdateCapturerInfo();
        }

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84) // WM_NCHITTEST
            {
                var cursor = this.PointToClient(Cursor.Position);

                if (TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                else if (TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

                else if (Top.Contains(cursor)) message.Result = (IntPtr)HTTOP;
                else if (Left.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                else if (Right.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                else if (Bottom.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
            }
        }
        private void panelDrag_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
            UpdateCapturerInfo();
        }

        public Bitmap QuickCapture()
        {
            //C#: how to take a screenshot of a portion of screen https://stackoverflow.com/a/3306633/5260872
            Rectangle rect = new Rectangle(this.Location.X, this.Location.Y, this.Width, this.Height);
            Bitmap image = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(image);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, this.Size, CopyPixelOperation.SourceCopy);
            return image;
        }

        public void UpdateCapturerInfo()
        {
            Main.main.CapturerX = this.Location.X;
            Main.main.CapturerY = this.Location.Y;
            Main.main.CapturerWidth = this.Width;
            Main.main.CapturerHeight = this.Height;
            //
            int maxSize;
            Size original = new Size(this.Width, this.Height);
            if (original.Width > 256)
            {
                maxSize = 256;//Width of Listview coloum1
            }
            else//Use Capturer window width if lower that 256
            {
                maxSize = original.Width;
            }
            float percent = (new List<float> { (float)maxSize / (float)original.Width, (float)maxSize / (float)original.Height }).Min();
            Size resultSize = new Size((int)Math.Floor(original.Width * percent), (int)Math.Floor(original.Height * percent));
            CapturerSize =  new Size(resultSize.Width, resultSize.Height);
        }
        public void SetWindow(int x, int y, int width, int height)
        { 
        this.Location = new Point(x, y);
        this.Size = new Size(width, height);
        }
    }
}