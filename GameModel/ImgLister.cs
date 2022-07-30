using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System;

namespace Slot_Gamer.GameModel
{
    public class ImgLister
    {
        ImageList imageList;
        public static List<SSInfo> ssinfo;
        public static Image[] imagearray;//Store images from imageList to use for displaying Bitmap image in pbPictureBox
        //

        public ImgLister()
        {
            imageList = new ImageList();
            imageList.ImageSize = Main.capturer.CapturerSize;
            ssinfo = new List<SSInfo>();
        }
        string iftrue;
        public void LoadImagesInfo()//Load the Images Captured
        {
            Main.LOG($"Loading {ssinfo.Count} Images...");
            imagearray = new Image[ssinfo.Count];//initialize imagearray equal to imageList
            for (int i = 0; i < ssinfo.Count - 1; i++)
            {
                if (ssinfo[i].HasColor == false && ssinfo[i].PostCapture != true) { iftrue = $"_ <<<<< Tigger: True >>>>>"; }//imageinfo object that doenst have the target color and trigger the if function to stop
                imageList.Images.Add(ssinfo[i].image);
                Main.main.listView1.SmallImageList = imageList;
                string info = $" {ssinfo[i].Capturetime}  Image: {i} \n";
                info += $" Up: {ssinfo[i].Up} Down: {ssinfo[i].Down} | Elapsed: {ssinfo[i].Elapsed} millisec {iftrue}\n";
                if (ssinfo[i].HasColor == true && ssinfo[i].PostCapture != true)
                {
                    info += $" Has Target Color at: Y:{ssinfo[i].SpotY} --  X:{ssinfo[i].SpotX} -- Value: {ssinfo[i].SpotValue}\n";
                }
                info += ssinfo[i].Log;
                Main.main.listView1.Items.Add(info, i);
                iftrue = "";
            }
            var img = imageList.Images;
            try
            {
                for (int i = 0; i <= imageList.Images.Count - 1; i++)
                {
                    Image image = img[i];//Pass imagelist to image
                    imagearray[i] = image;//Pass image to image array[]
                }
            }
            catch (Exception) { }
            Main.LOG($"Loaded");
        }
        public void AddSS(int y, int x, Color value, Image img, string capturetime, long elapse, string up, string down, string log, bool hascolor)
        {
            ssinfo.Add(new SSInfo
            {
                SpotY = y,
                SpotX = x,
                SpotValue = value,
                image = img,
                Capturetime = capturetime,
                Elapsed = elapse,
                Up = up,
                Down = down,
                Log = log,
                HasColor = hascolor//The Target
            });
        }
        public void AddSS(string log, bool postcapture)//use live Capture
        {
            ssinfo.Add(new SSInfo
            {
                image = Main.capturer.QuickCapture(),
                Capturetime = Main.main.timemillisec(),
                Log = log,
                PostCapture = postcapture
            });
        }










    }
}
