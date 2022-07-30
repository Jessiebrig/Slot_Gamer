using System.Threading;

namespace Slot_Gamer.GameModel
{
    public interface IGameModel
    {
        public string GameName { get; set; }
        public bool lookingforgold { get; set; }
        public void Spin(CancellationToken cancellationToken);
        public void LoadImagesInfo();
    }
}
