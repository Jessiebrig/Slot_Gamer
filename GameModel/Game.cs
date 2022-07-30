using System.Linq;
using System.Threading;
using Slot_Gamer.Games;
using System.Collections.Generic;

namespace Slot_Gamer.GameModel
{
    public class Game
    {
        public List<IGameModel> Gamemodel;//List of Games ready to be play//And Select the game to be played from Cmb_Game
        //
        public CancellationTokenSource sessiontoken;
        //
        public int GameIndex { get; set; }

        public string GameName { get; set; }
        //
        public int Count()
        {
            return Gamemodel.Count();
        }
        public Game()
        {
        }
        public void Start(int gameindex)
        {
            GameIndex = gameindex;
            GameName = Gamemodel[GameIndex].GameName;

        }

        public void Spin()
        {
            sessiontoken = new CancellationTokenSource();
            Gamemodel[GameIndex].Spin(sessiontoken.Token);
        }
        public void Stop()
        {
            if (hassession)
            {
                sessiontoken.Cancel();
            }
            Main.LOG("Stop");
        }

        public void GameSS()
        {
            Gamemodel[GameIndex].LoadImagesInfo();
        }


        public bool hassession//Check if Theres a running sessiontoken
        {
            get
            {
                if (sessiontoken != null)
                {
                    return true;
                }
                else { return false; }
            }
        }

        public void LoadGames()//Load the Game Classes using List<IGameModel>();
        {
            Gamemodel = new List<IGameModel>();
            Gamemodel.Add(new MoneyGaming2 { GameName = "MoneyGaming" });
            Gamemodel.Add(new __777 { GameName = "777" });
            Gamemodel.Add(new SevenSevenSeven { GameName = "SevenSevenSeven" });
            foreach (IGameModel game in Gamemodel)
            {
                Main.main.Cmb_Games.Items.Add(game.GameName);
            }
            Main.LOG($"Available Game Classes: {Gamemodel.Count}");
        }

    }
}
