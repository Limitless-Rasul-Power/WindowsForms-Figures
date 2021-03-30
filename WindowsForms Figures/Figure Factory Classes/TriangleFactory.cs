namespace WindowsForms_Figures
{
    public class TriangleFactory : IFigureFactory
    {
        public IFigure GetFigure()
        {
            return new Triangle();
        }
    }
}
