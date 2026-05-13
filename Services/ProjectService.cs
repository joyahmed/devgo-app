namespace DevGo;

public static class ProjectService
{
    public static List<Project> LoadProjects(
        string workspace
    )
    {
        var projects = new List<Project>();

        if (!Directory.Exists(workspace))
        {
            return projects;
        }

        var directories =
            Directory.GetDirectories(workspace);

        foreach (var dir in directories)
        {
            var info = new DirectoryInfo(dir);

            projects.Add(new Project
            {
                Name = info.Name,

                FullPath = info.FullName
            });
        }

        return projects
            .OrderBy(x => x.Name)
            .ToList();
    }
}