using System.Drawing;

namespace WindowsForms_Figures
{
    public interface IFigure
    {
        Point[] Points { get; set; }
        Size Size { get; set; }
        Color Color { get; set; }
        bool IsFilled { get; set; }
        bool IsCustom { get; set; }

    }
}
