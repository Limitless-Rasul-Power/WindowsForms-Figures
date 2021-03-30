using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsForms_Figures.Figure_Classes;
using WindowsForms_Figures.Figure_Factory_Classes;

namespace WindowsForms_Figures
{

    public partial class Form1 : Form
    {
        private IFigureFactory _figureFactory;
        private readonly List<IFigure> _figures = new List<IFigure>();
        private readonly List<string> _figureNames = default;
        private Color _figureColor = Color.Red;
        private Point _tempPoint = new Point();

        public Form1()
        {
            InitializeComponent();
            JsonFileHelper.JSONDeSerialization(ref _figureNames, "Figure Names");
            FigureNamesCmbBx.DataSource = _figureNames;
            FigureNamesCmbBx.SelectedIndex = 0;
        }

        private void FigureNamesCmbBx_SelectedIndexChanged(object sender, EventArgs e)
        {
            RectangleModeGrpBx.Visible = false;

            if ((FigureNamesCmbBx.SelectedItem as string) == FigureNames.Rectangle)
            {
                _figureFactory = new CustomRectangleFactory();
                RectangleModeGrpBx.Visible = true;
            }
            else if ((FigureNamesCmbBx.SelectedItem as string) == FigureNames.Circle)
            {
                _figureFactory = new CircleFactory();
                AdjustWidthAndHeightTxtBx(true);
            }
            else if ((FigureNamesCmbBx.SelectedItem as string) == FigureNames.Rhomb)
            {
                _figureFactory = new RhombFactory();
                AdjustWidthAndHeightTxtBx(true);
            }
            else
            {
                _figureFactory = new TriangleFactory();
                AdjustWidthAndHeightTxtBx(true);
            }
            
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            var figure = _figureFactory.GetFigure();

            int.TryParse(WidthTxtBx.Text, out int width);
            int.TryParse(HeightTxtBx.Text, out int height);

            if (width > 0 && height > 0)
            {
                if (figure is CustomRectangle)
                {
                    AdjustFigureSettings(figure, new Size(width, height));

                    if (OffRdBtn.Checked)
                    {
                        figure.Points[0] = e.Location;
                    }

                }

                else if (figure is Circle)
                {
                    AdjustFigureSettings(figure, new Size(width, height));
                    figure.Points[0] = e.Location;
                }

                else if (figure is Rhomb)
                {
                    AdjustFigureSettings(figure, new Size(width, height));

                    figure.Points[0] = e.Location;

                    figure.Points[1].X = figure.Points[0].X - width;
                    figure.Points[1].Y = figure.Points[0].Y + height;

                    figure.Points[2].X = figure.Points[0].X;
                    figure.Points[2].Y = figure.Points[1].Y + height;

                    figure.Points[3].X = figure.Points[0].X + width;
                    figure.Points[3].Y = figure.Points[2].Y - height;
                }

                else
                {
                    AdjustFigureSettings(figure, new Size(width, height));

                    if ((FigureNamesCmbBx.SelectedItem as string) == FigureNames.UpsideDownTriangle)
                    {
                        figure.Points[0] = e.Location;
                        figure.Points[1].X = e.Location.X - width;
                        figure.Points[1].Y = e.Location.Y - height;

                        figure.Points[2].X = e.Location.X + width;
                        figure.Points[2].Y = e.Location.Y - height;
                    }
                    else
                    {
                        figure.Points[0] = e.Location;
                        figure.Points[1].X = e.Location.X - width;
                        figure.Points[1].Y = e.Location.Y + height;

                        figure.Points[2].X = e.Location.X + width;
                        figure.Points[2].Y = e.Location.Y + height;
                    }


                }

                _figures.Add(figure);
                this.Refresh();

            }
        }

        private void ColorBtn_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            var result = colorDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                _figureColor = colorDialog.Color;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            using (var graphic = e.Graphics)
            {
                foreach (var figure in _figures)
                {
                    SolidBrush solidBrush = new SolidBrush(figure.Color);
                    Pen pen = new Pen(figure.Color, 3);

                    if (figure is CustomRectangle rectangle && rectangle.IsCustom == false)
                    {
                        if (rectangle.IsFilled)
                        {
                            graphic.FillRectangle(solidBrush, rectangle.Points[0].X, rectangle.Points[0].Y, rectangle.Size.Width, rectangle.Size.Height);
                        }
                        else
                        {
                            graphic.DrawRectangle(pen, rectangle.Points[0].X, rectangle.Points[0].Y, rectangle.Size.Width, rectangle.Size.Height);
                        }
                    }

                    else if (figure is CustomRectangle customRectangle && customRectangle.IsCustom == true)
                    {
                        if (customRectangle.IsFilled)
                        {
                            graphic.FillPolygon(solidBrush, customRectangle.Points);
                        }
                        else
                        {
                            graphic.DrawPolygon(pen, customRectangle.Points);
                        }
                    }

                    else if (figure is Circle circle)
                    {
                        if (circle.IsFilled)
                        {
                            graphic.FillEllipse(solidBrush, circle.Points[0].X, circle.Points[0].Y, circle.Size.Width, circle.Size.Height);
                        }
                        else
                        {
                            graphic.DrawEllipse(pen, circle.Points[0].X, circle.Points[0].Y, circle.Size.Width, circle.Size.Height);
                        }
                    }

                    else if (figure is Triangle triangle)
                    {
                        if (triangle.IsFilled)
                        {
                            graphic.FillPolygon(solidBrush, triangle.Points);
                        }
                        else
                        {
                            graphic.DrawPolygon(pen, triangle.Points);
                        }
                    }

                    else if (figure is Rhomb rhomb)
                    {
                        if (rhomb.IsFilled)
                        {
                            graphic.FillPolygon(solidBrush, rhomb.Points);
                        }
                        else
                        {
                            graphic.DrawPolygon(pen, rhomb.Points);
                        }
                    }
                }
            }

        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete)))
                e.Handled = true;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            var figure = _figureFactory.GetFigure();

            if (figure is CustomRectangle && OnRdBtn.Checked)
            {
                _tempPoint.X = e.Location.X;
                _tempPoint.Y = e.Location.Y;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            var figure = _figureFactory.GetFigure();

            if (figure is CustomRectangle && OnRdBtn.Checked)
            {
                figure.Color = _figureColor;
                figure.IsCustom = true;
                figure.IsFilled = FillRdBtn.Checked;


                figure.Points[0].X = _tempPoint.X;
                figure.Points[0].Y = _tempPoint.Y;

                figure.Points[1].X = e.Location.X;
                figure.Points[1].Y = figure.Points[0].Y;

                figure.Points[2].X = e.Location.X;
                figure.Points[2].Y = e.Location.Y;

                figure.Points[3].X = figure.Points[0].X;
                figure.Points[3].Y = e.Location.Y;

                _figures.Add(figure);
                this.Refresh();
            }
        }

        private void OnRdBtn_CheckedChanged(object sender, EventArgs e)
        {
            AdjustWidthAndHeightTxtBx(false);

            WidthTxtBx.Text = default;
            HeightTxtBx.Text = default;
        }

        private void OffRdBtn_CheckedChanged(object sender, EventArgs e)
        {
            AdjustWidthAndHeightTxtBx(true);
        }

        private void AdjustWidthAndHeightTxtBx(bool flag)
        {
            WidthTxtBx.Enabled = flag;
            HeightTxtBx.Enabled = flag;
        }

        private void AdjustFigureSettings(IFigure figure, Size size)
        {
            figure.Color = _figureColor;
            figure.Size = size;
            figure.IsFilled = FillRdBtn.Checked;
        }
    }
}
