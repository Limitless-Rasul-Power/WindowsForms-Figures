using System.Drawing;

namespace WindowsForms_Figures
{
    public class CustomRectangle : IFigure
    {
        public Point[] Points { get; set; } = new Point[4];
        public Size Size { get; set; } 
        public Color Color { get; set; }
        public bool IsFilled { get; set; }        
        public bool IsCustom { get; set; }

    }
}
