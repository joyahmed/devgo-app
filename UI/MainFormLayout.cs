namespace DevGo;

using System.Drawing;

public partial class MainForm
{
    private Label lblTitle = null!;
    private Label lblSubTitle = null!;

    private Panel titleBar = null!;

    private TextBox txtSearch = null!;

    private ListBox listProjects = null!;

    private Button btnCode = null!;

    private Button btnTmux = null!;

    private Button btnBoth = null!;


    private void InitializeLayout()
    {
        ConfigureForm();

        ApplyRoundedCorners();

        CreateTitleBar();

        CreateSearchBox();

        CreateProjectList();

        CreateButtons();
    }
}