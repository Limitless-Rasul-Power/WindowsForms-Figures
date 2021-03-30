using System.Drawing;

namespace WindowsForms_Figures
{
    public class Triangle : IFigure
    {
        public Point[] Points { get; set; } = new Point[3];
        public Size Size { get; set; }
        public Color Color { get; set; }
        public bool IsFilled { get; set; }
        public bool IsCustom { get; set; }

        public bool IsUpsideDown { get; set; }

    }
}
