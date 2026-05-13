namespace DevGo;

using System.Text.Json;

public static class WorkspaceService
{
  private const string AppFolderName = "DevGo";
  private const string DataFolderName = "data";
  private const string WorkspaceFileName = "workspaces.json";

  private static readonly string DataDir =
      Path.Combine(
          Environment.GetFolderPath(
              Environment.SpecialFolder.LocalApplicationData
          ),
          AppFolderName,
          DataFolderName
      );

  private static readonly string WorkspaceFile =
      Path.Combine(
          DataDir,
          WorkspaceFileName
      );

  public static List<string> LoadWorkspaces()
  {
    EnsureDataDirectory();

    // ------------------------------------------------
    // CREATE FILE IF MISSING
    // ------------------------------------------------

    // MessageBox.Show(WorkspaceFile);

    if (!File.Exists(WorkspaceFile))
    {
      File.WriteAllText(
          WorkspaceFile,
          "[]"
      );
    }

    var json =
        File.ReadAllText(WorkspaceFile);

    return JsonSerializer.Deserialize<List<string>>(json)
        ?? new List<string>();
  }

  public static void SaveWorkspaces(
      List<string> workspaces
  )
  {
    EnsureDataDirectory();

    var json =
        JsonSerializer.Serialize(
            workspaces,
            new JsonSerializerOptions
            {
              WriteIndented = true
            }
        );

    File.WriteAllText(
        WorkspaceFile,
        json
    );
  }

  private static void EnsureDataDirectory()
  {
    if (!Directory.Exists(DataDir))
    {
      Directory.CreateDirectory(DataDir);
    }

    MigrateLegacyWorkspaceFileIfNeeded();
  }

  private static void MigrateLegacyWorkspaceFileIfNeeded()
  {
    if (File.Exists(WorkspaceFile))
    {
      return;
    }

    var legacyFile = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        DataFolderName,
        WorkspaceFileName
    );

    if (!File.Exists(legacyFile))
    {
      return;
    }

    File.Copy(legacyFile, WorkspaceFile, overwrite: false);
  }
}
