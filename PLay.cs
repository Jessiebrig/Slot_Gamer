using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Slot_Gamer.GameModel;


namespace Slot_Gamer
{
    public partial class Main
    {
        Random time = new Random();
        CancellationTokenSource autoplaytoken;
        CancellationTokenSource SStoken;

        public void LoadAutoModes()
        {
            Cmb_AutoPlay.Items.Add("Random from Min Max");
            Cmb_AutoPlay.Items.Add("Keep Adding Sleep");
            Cmb_AutoPlay.Items.Add("Random from Min Max and keep adding");
            Cmb_AutoPlay.Items.Add("Not Implemented");
        }

        public async void Autoplay()
        {
            LOG($"AutoPlaying using: {Cmb_AutoPlay.SelectedIndex}");
            
            Btn_Spin.Enabled = false;//Disable Spin of course!
            Btn_AutoPlay.Text = "Stop";
            Btn_AutoPlay.BackColor = Color.Red;
            autoplaytoken = new CancellationTokenSource();
            SStoken = new CancellationTokenSource();
            AsyncSS(SStoken.Token);//Make Checkbox to AddSS or not in autopay
            int autoplayindex = Cmb_AutoPlay.SelectedIndex;
            LeftClick(MouseX, MouseY);
            await Task.Run(() => play(autoplaytoken.Token, autoplayindex));
        }
        public void play(CancellationToken cancellationToken, int autoindex)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (autoindex == 0)//RandomClick
                    {
                        int random = time.Next(Min, Max);
                        LOG($"Random sleeping: {random}");
                        Thread.Sleep(random); 
                    }
                    else if (autoindex == 1)//KeepAdding the Sleep time
                    {
                        int sleep = Sleep + Add;
                        LOG($"Sleep: <{Sleep}> + Add: <{Add}> Total Sleep: <{sleep}>");
                        Thread.Sleep(sleep);
                        Sleep = sleep;//Set the new Sleep
                    }
                    else if (autoindex == 2)//Generate random and keep adding
                    {
                        int random = time.Next(Min, Max);
                        Add++;//Add 1millisec every cycle
                        int sleep = random + Add;
                        LOG($"Sleep random: <{random}> and keep adding: <{Add}> Total Sleep: <{sleep}>");
                        Thread.Sleep(sleep);
                    }
                    LeftClick(MouseX, MouseY);
                    cancellationToken.ThrowIfCancellationRequested();
                    LOG($"Sleep Cycle for: {PlayCycle}");
                    Thread.Sleep(PlayCycle);
                }
            }
            catch (Exception ex) //Cancellled
            { 
                LOG(ex.Message);
                main.Invoke(new Action(() => { Btn_AutoPlay.Enabled = true; }));
            }
        }

        public void StopAuto()
        {
            Btn_AutoPlay.Enabled = false;
            autoplaytoken.Cancel();
            //SStoken.Cancel();
            Btn_AutoPlay.Text = "AutoPlay";
            Btn_AutoPlay.BackColor = Color.DeepSkyBlue;
            Btn_Spin.Enabled = true;
        }
        public async void SpinAsync()
        {
            capturer.UpdateCapturerInfo();
            capturer.Opacity = .001;
            LOG("Spinning");
            await Task.Run(() => game.Spin());
            Btn_Spin.Text = "Spin";
            Btn_Spin.BackColor = Color.DeepSkyBlue;
            HotkeySpin = false;//signal in case1: hotkey that Spin alreadystop is done
            capturer.Opacity = .7;
            game.GameSS();
        }
        public void Stop()
        {
            game.Stop();
            Btn_Spin.Text = "Spin";
            Btn_Spin.BackColor = Color.DeepSkyBlue;
        }
        public void Spin()
        {
            Btn_Spin.Text = "Stop";
            Btn_Spin.BackColor = Color.Red;
            SpinAsync();
        }





    }
}
