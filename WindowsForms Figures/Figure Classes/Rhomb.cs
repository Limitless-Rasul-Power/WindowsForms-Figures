using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms_Figures.Figure_Classes
{
    public class Rhomb : IFigure
    {
        public Point[] Points { get; set; } = new Point[4];
        public Size Size { get; set; }
        public Color Color { get; set; }
        public bool IsFilled { get; set; }
        public bool IsCustom { get; set; }
    }
}
