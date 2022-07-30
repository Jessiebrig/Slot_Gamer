using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Slot_Gamer.GameModel;

namespace Slot_Gamer.Games
{
    public class __777 : IGameModel
    {
        public string GameName { get; set; }
        public bool lookingforgold { get; set; }

        public void LoadImagesInfo()
        {
            throw new NotImplementedException();
        }

        public void Spin(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
