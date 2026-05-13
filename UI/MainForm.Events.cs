namespace DevGo;

public partial class MainForm
{
  private void BtnCode_Click(
      object? sender,
      EventArgs e
  )
  {
    var project = GetSelectedProject();

    if (project == null)
    {
      return;
    }

    DevLauncher.OpenVSCode(project);
  }

  private void BtnTmux_Click(
      object? sender,
      EventArgs e
  )
  {
    var project = GetSelectedProject();

    if (project == null)
    {
      return;
    }

    DevLauncher.OpenTmux(project);
  }

  private void BtnBoth_Click(
      object? sender,
      EventArgs e
  )
  {
    var project = GetSelectedProject();

    if (project == null)
    {
      return;
    }

    DevLauncher.OpenBoth(project);
  }

  private void ListProjects_DoubleClick(
      object? sender,
      EventArgs e
  )
  {
    BtnBoth_Click(sender, e);
  }

  private void TxtSearch_TextChanged(
      object? sender,
      EventArgs e
  )
  {
    var search =
        txtSearch.Text.ToLower();

    var filtered = projects.Where(x =>
        x.Name.ToLower().Contains(search)
    );

    RenderProjects(filtered);

    if (listProjects.Items.Count > 0)
    {
      listProjects.SelectedIndex = 0;
    }
  }

  private void TitleBar_MouseDown(
      object? sender,
      MouseEventArgs e
  )
  {
    if (e.Button == MouseButtons.Left)
    {
      NativeMethods.ReleaseCapture();

      NativeMethods.SendMessage(
          Handle,
          0xA1,
          0x2,
          0
      );
    }
  }

  private void BtnRemoveWorkspace_Click(
    object? sender,
    EventArgs e
)
  {
    var workspaces =
        WorkspaceService.LoadWorkspaces();

    if (workspaces.Count == 0)
    {
      MessageBox.Show(
          "No workspaces found."
      );

      return;
    }

    var selected =
        Microsoft.VisualBasic
            .Interaction.InputBox(
                string.Join(
                    Environment.NewLine,
                    workspaces.Select(
                        (x, i) =>
                            $"{i + 1}. {x}"
                    )
                ) +
                "\n\nEnter workspace number to remove:",
                "Remove Workspace"
            );

    if (!int.TryParse(selected, out int index))
    {
      return;
    }

    index--;

    if (index < 0 || index >= workspaces.Count)
    {
      return;
    }

    workspaces.RemoveAt(index);

    WorkspaceService.SaveWorkspaces(
        workspaces
    );

    LoadProjects();
  }

  private void BtnWorkspace_Click(
      object? sender,
      EventArgs e
  )
  {
    using var dialog =
        new FolderBrowserDialog();

    dialog.Description =
        "Select Workspace Folder";

    if (dialog.ShowDialog() != DialogResult.OK)
    {
      return;
    }

    var workspaces =
        WorkspaceService.LoadWorkspaces();

    if (!workspaces.Contains(dialog.SelectedPath))
    {
      workspaces.Add(dialog.SelectedPath);

      WorkspaceService.SaveWorkspaces(
          workspaces
      );
    }

    LoadProjects();
  }
}