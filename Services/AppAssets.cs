namespace DevGo;

using System.Drawing;
using System.Reflection;

public static class AppAssets
{
    private const string IconResourceName = "DevGo.assets.icon.ico";
    private const string LogoResourceName = "DevGo.assets.logo.png";

    public static Icon? LoadAppIcon()
    {
        using var stream = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream(IconResourceName);

        if (stream == null)
        {
            return null;
        }

        return new Icon(stream);
    }

    public static Image? LoadLogoImage()
    {
        using var stream = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream(LogoResourceName);

        if (stream == null)
        {
            return null;
        }

        using var image = Image.FromStream(stream);
        return new Bitmap(image);
    }
}
