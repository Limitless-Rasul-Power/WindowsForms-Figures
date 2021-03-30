namespace WindowsForms_Figures
{
    public class CircleFactory : IFigureFactory
    {
        public IFigure GetFigure()
        {
            return new Circle();
        }
    }
}
