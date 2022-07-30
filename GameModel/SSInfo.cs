using System.Drawing;

namespace Slot_Gamer.GameModel
{
    public class SSInfo
    {
        public bool HasColor { get; set; }
        public bool PostCapture { get; set; }
        //
        public int SpotY { get; set; }
        public int SpotX { get; set; }
        //
        public string Capturetime { get; set; }
        public string Up { get; set; }
        public string Down { get; set; }
        public string Log { get; set; }
        //
        public long Elapsed { get; set; }
        public Color SpotValue { get; set; }
        public Image image { get; set; }
        //
    }
}
