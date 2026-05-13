namespace DevGo;

public partial class MainForm
{
  private void LoadProjects()
  {
    projects.Clear();

    var workspaces =
        WorkspaceService.LoadWorkspaces();

    foreach (var workspace in workspaces)
    {
      var loadedProjects =
          ProjectService.LoadProjects(
              workspace
          );

      projects.AddRange(
          loadedProjects
      );
    }

    RenderProjects(projects);

    if (listProjects.Items.Count > 0)
    {
      listProjects.SelectedIndex = 0;
    }
  }
  private void RenderProjects(
      IEnumerable<Project> items
  )
  {
    listProjects.Items.Clear();

    var projectList =
        items.OrderBy(x => x.Name).ToList();

    foreach (var item in projectList)
    {
      listProjects.Items.Add(item.Name);
    }

    lblEmptyState.Visible =
        listProjects.Items.Count == 0;
  }

  private Project? GetSelectedProject()
  {
    if (listProjects.SelectedItem == null)
    {
      return null;
    }

    var name =
        listProjects.SelectedItem.ToString();

    return projects.FirstOrDefault(
        x => x.Name == name
    );
  }
}