using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsForms_Figures.Figure_Classes;

namespace WindowsForms_Figures.Figure_Factory_Classes
{
    class RhombFactory : IFigureFactory
    {
        public IFigure GetFigure()
        {
            return new Rhomb();
        }
    }
}
