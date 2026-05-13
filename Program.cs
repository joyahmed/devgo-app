namespace DevGo;

internal static class Program
{
    private const string AppFolderName = "DevGo";
    private const string LogFolderName = "logs";
    private const string StartupLogFileName = "startup.log";

    [STAThread]
    static void Main()
    {
        WriteStartupHeartbeat("Main() entered");

        AppDomain.CurrentDomain.UnhandledException += (_, args) =>
        {
            if (args.ExceptionObject is Exception ex)
            {
                LogStartupError(ex);
            }
        };

        Application.ThreadException += (_, args) =>
        {
            LogStartupError(args.Exception);
        };

        try
        {
            ApplicationConfiguration.Initialize();
            WriteStartupHeartbeat("ApplicationConfiguration initialized");
            Application.Run(new MainForm());
        }
        catch (Exception ex)
        {
            LogStartupError(ex);
            throw;
        }
    }

    private static void LogStartupError(Exception ex)
    {
        try
        {
            var logDir = GetLogDir();

            Directory.CreateDirectory(logDir);

            var logPath = Path.Combine(logDir, StartupLogFileName);
            var message = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {ex}\n\n";
            File.AppendAllText(logPath, message);
        }
        catch
        {
            // Avoid crashing while writing crash logs.
        }
    }

    private static void WriteStartupHeartbeat(string message)
    {
        try
        {
            var logDir = GetLogDir();
            Directory.CreateDirectory(logDir);

            var heartbeatPath = Path.Combine(logDir, "startup-heartbeat.log");
            var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}\n";
            File.AppendAllText(heartbeatPath, line);
        }
        catch
        {
            // Ignore diagnostic write failures.
        }
    }

    private static string GetLogDir()
    {
        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            AppFolderName,
            LogFolderName
        );
    }
}
