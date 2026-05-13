namespace DevGo;

using System.Diagnostics;

public static class DevLauncher
{
    public static void OpenVSCode(Project project)
    {
        var linuxPath = ConvertToWslPath(project.FullPath);

        var uri = $"vscode-remote://wsl+Ubuntu{linuxPath}";

        // MessageBox.Show(uri);

        Process.Start(new ProcessStartInfo
        {
            FileName = "code",

            Arguments = $"--folder-uri \"{uri}\"",

            UseShellExecute = true
        });
    }
public static void OpenTmux(Project project)
{
    var session = project.Name;

    var linuxPath = ConvertToWslPath(project.FullPath);

    var script = $"""
#!/usr/bin/env bash

if ! tmux has-session -t "{session}" 2>/dev/null; then

    tmux new-session -d \
        -s "{session}" \
        -n code \
        -c "{linuxPath}"

    tmux new-window \
        -t "{session}:" \
        -n agents \
        -c "{linuxPath}"

    tmux new-window \
        -t "{session}:" \
        -n git \
        -c "{linuxPath}"

fi

tmux attach -t "{session}"
""";

    var tempFile = Path.Combine(
        Path.GetTempPath(),
        $"devgo-{session}.sh"
    );

    File.WriteAllText(
    tempFile,
    script.Replace("\r\n", "\n")
);

    var wslTempFile =
        $"/mnt/c/Users/{Environment.UserName}/AppData/Local/Temp/devgo-{session}.sh";

    Process.Start(new ProcessStartInfo
    {
        FileName = "wt",

        Arguments = $"wsl bash '{wslTempFile}'",

        UseShellExecute = true
    });
}

public static void OpenBoth(Project project)
{
    OpenVSCode(project);

    Thread.Sleep(1000);

    OpenTmux(project);
}

    private static string ConvertToWslPath(string path)
    {
        path = path.Replace("\\", "/");

        // WSL UNC PATH
        if (path.StartsWith("//wsl.localhost/Ubuntu"))
        {
            path = path.Replace("//wsl.localhost/Ubuntu", "");

            return path;
        }

        // WINDOWS C DRIVE
        if (path.StartsWith("C:/"))
        {
            return path.Replace("C:/", "/mnt/c/");
        }

        // WINDOWS D DRIVE
        if (path.StartsWith("D:/"))
        {
            return path.Replace("D:/", "/mnt/d/");
        }

        return path;
    }
}