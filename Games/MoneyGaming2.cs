using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Slot_Gamer.GameModel;

namespace Slot_Gamer
{
    public class MoneyGaming2 : IGameModel
    {
        ImgLister gameInfo;
        //
        public string GameName { get; set; }
        public bool lookingforgold { get; set; }
        //
        Button Btn_Spin = Main.main.Btn_Spin;
        //
        string Red, Green, Blue;
        public void SetColorparams()//Set for reference
        {
            Red = "Red > 150";
            Green = "";//Green = "G < 150";
            Blue = "";//Blue = "B > 80";
        }

        public void LoadImagesInfo()
        {
            gameInfo.LoadImagesInfo();
        }

        public void Spin(CancellationToken cancellationToken)
        {
            gameInfo = new ImgLister();//Cross Thread in setting Capturer Size
            Main.main.Clear();
            SetColorparams();//Set Color parameter
            Main.main.LeftClick(Main.main.MouseX, Main.main.MouseY, gameInfo);//SPIN
            Thread.Sleep(Main.main.Sleep);//wait for spin cooldown
            Main.LOG("Looking for: " + Red + " | " + Green + " | " + Blue);
            Main.LOG($"Looking for: { Red } | { Green } | { Blue}");
            Main.LOG("Running Loop...");
            try
            {
                int count = 0;
                while (HasColor())
                {
                    Main.main.ImagesCount = count;//To be check using Quickss SS if invoking the Lbl_Amount slowdown the while loop
                    cancellationToken.ThrowIfCancellationRequested();
                    count++;
                }
            }
            catch (Exception ex) { Main.LOG(ex.Message); Btn_Spin.Invoke(new Action(() => Btn_Spin.Text = "Spin")); }
        }

        Bitmap Captured;
        string capturetime;
        string up;
        string down;
        public bool HasColor()
        {
            up = ""; down = "";
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Captured = Main.capturer.QuickCapture();
                capturetime = Main.main.timemillisec();
                for (int y = 0; y < Captured.Height; y++)
                {
                    for (int x = 0; x < Captured.Width; x += Captured.Width / 10)
                    {
                        if (y == 0) { up += "X"; } else { down += "X"; }

                        Color color = Captured.GetPixel(x, y);
                        if (color.R > 150)//return true if has gold
                        {
                            watch.Stop();
                            gameInfo.AddSS(y, x, color, Captured, capturetime, watch.ElapsedMilliseconds, up, down, "", true);//Load to Listview after Clicking Stop
                            return true;
                        }
                    }
                    y += Captured.Height - 2;
                }
                watch.Stop();
                string log = "=====> No Target Color <=====";
                gameInfo.AddSS(0, 0, Color.Green, Captured, capturetime, watch.ElapsedMilliseconds, up, down, log, false);//true means the one that trigger the if function
                string nogold = $"No Target Color! _ < Capturetime: {capturetime} > _ < Total execution time: {watch.ElapsedMilliseconds} milliseconds >";//just getting elapsetime and log after LeftMouseClick()
                Main.LOG(nogold);
                Main.LOG($"Delaying: {Main.main.Sleep}ms | MAX Delay should only be 100ms. Expirement if delaying Click will work");
                Thread.Sleep(Main.main.Sleep);
                Main.main.LeftClick(Main.main.MouseX, Main.main.MouseY, gameInfo);//Try to make delay when clicking
                for (int i = 0; i < 100; i++)
                {
                    gameInfo.AddSS("100X_Post_Capture", true);
                }//Capture 10X after Clicking Stop just for reference
            }
            catch (Exception ex) { Main.LOG(ex.Message); }
            return false;
        }
    }
}
