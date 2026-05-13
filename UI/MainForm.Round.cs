namespace DevGo;

using System.Drawing.Drawing2D;

public partial class MainForm
{
  private void ApplyRoundedCorners()
  {
    var path = new GraphicsPath();

    int radius = 30;

    path.StartFigure();

    path.AddArc(
        0,
        0,
        radius,
        radius,
        180,
        90
    );

    path.AddArc(
        Width - radius,
        0,
        radius,
        radius,
        270,
        90
    );

    path.AddArc(
        Width - radius,
        Height - radius,
        radius,
        radius,
        0,
        90
    );

    path.AddArc(
        0,
        Height - radius,
        radius,
        radius,
        90,
        90
    );

    path.CloseFigure();

    Region = new Region(path);
  }
}