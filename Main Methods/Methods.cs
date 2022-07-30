using System;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;
using Slot_Gamer.GameModel;

namespace Slot_Gamer
{
    public partial class Main
    {
        public void ShowValues()
        {
            Clear();
            Bitmap img = capturer.QuickCapture();
            LOG("Image Width: " + img.Width.ToString() + " <-X-> height: " + img.Height.ToString());
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x += img.Width / 20)
                {
                    Color color = img.GetPixel(x, y);
                    string value = color.R.ToString() + "_" + color.G.ToString() + "_" + color.B.ToString();
                    LOG("Y: " + y.ToString() + " -- X: " + x.ToString() + " -- Pixel Color Value: " + value);//log the current XY being check
                }
                y += img.Height - 2;
            }
        }

        public void SaveImage()
        {
            for (int i = 0; i <= listView1.Items.Count - 1; i++)
            {
                //SaveFileDialog sfd = new SaveFileDialog();
                //sfd.CheckPathExists = true;
                //string dt = DateTime.Now.ToString().Replace(':', '_');
                //dt = dt.Replace('/', '_');
                //sfd.FileName = dt + "_" + i.ToString();
                //sfd.Filter = "PNG Image(*.png)|*.png|JPG Image(*.jpg)|*.jpg|BMP Image(*.bmp)|*.bmp";
                //if (sfd.ShowDialog() == DialogResult.OK)
                //{
                //    try
                //    {
                //        pbCaptured.Image = imgarray[i];
                //        pbCaptured.Image.Save(sfd.FileName);
                //        LOG(listView1.Items.Count.ToString());
                //    }
                //    catch (Exception) { }
                //}
                //else { break; }
            }
        }
        public void FocusItem(ListView listView, int index)
        {
            try
            {
                if (listView1.Items.Count > index | 0 >= index)
                {
                    listView.Items[index].Selected = true;
                    ListViewItem item = listView.Items[index];
                    item.Focused = true;
                }
                Tbx_Index.Clear();
            }
            catch (Exception) { }
        }

        public async void AsyncSS(CancellationToken token)
        {
            capturer.Opacity = .001;
            imgLister = new ImgLister();//Just creating new object of GameInfo to use the AddSS method to get ScreenShot
            await Task.Run(() =>
            {
                try
                {
                    int count = 0;
                    while (!token.IsCancellationRequested)
                    {
                        imgLister.AddSS($"QuickSS Image: {count}", true);
                        ImagesCount = count;
                        token.ThrowIfCancellationRequested();
                        count++;
                    }
                    LeftClick(main.MouseX, main.MouseY);
                }
                catch (Exception ex) { LOG(ex.Message); }
            });
            imgLister.LoadImagesInfo();
            capturer.Opacity = .7;
        }

        public void LoadDeafults()
        {
            Pnl_Manage.BringToFront();
            Pnl_Manage.Hide();
            LoadSets();//Load Capturer Sets
            LoadAutoModes();//Load AutoPlay Modes
            capturer = new Capturer(); capturer.Show();
            game = new Game();
            game.LoadGames();//Load the Available Games in Cmb_Games
            Cmb_Games.SelectedIndex = 0;
            Cmb_AutoPlay.SelectedIndex = 0;
            Min = 345; 
            Max = 675; 
            Add = 1;
            Sleep = 432; 
            PlayCycle = 9876;


        }
    }
}
