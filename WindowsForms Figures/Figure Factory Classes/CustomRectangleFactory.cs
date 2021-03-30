namespace WindowsForms_Figures
{
    public class CustomRectangleFactory : IFigureFactory
    {
        public IFigure GetFigure()
        {
            return new CustomRectangle();
        }
    }
}
