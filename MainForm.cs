namespace DevGo;

public partial class MainForm : Form
{
    private readonly List<Project> projects = new();

    public MainForm()
    {
        InitializeLayout();

        WorkspaceService.LoadWorkspaces();

        LoadProjects();

        ApplyRoundedCorners();
    }
}
