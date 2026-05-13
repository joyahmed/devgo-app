namespace DevGo;

using System.Drawing;
using System.Drawing.Drawing2D;

public partial class MainForm
{
    private Label lblEmptyState = null!;

    private void ConfigureForm()
    {
        Text = "DevGo";

        Width = 900;

        Height = 720;

        StartPosition =
            FormStartPosition.CenterScreen;


        BackColor = Color.FromArgb(8, 12, 18);

        ForeColor = Color.White;

        Font = new Font("Segoe UI", 10);

        Icon = AppAssets.LoadAppIcon()
            ?? SystemIcons.Application;

        FormBorderStyle =
            FormBorderStyle.None;

        DoubleBuffered = true;

        Padding = new Padding(2);

        Resize += (_, _) => CenterTitle();
    }

    private void CreateTitleBar()
    {
        titleBar = new Panel
        {
            Dock = DockStyle.Top,

            Height = 100,

            BackColor =
             Color.FromArgb(12, 18, 28)
        };

        Controls.Add(titleBar);

        var headerBorder = new Panel
        {
            Dock = DockStyle.Top,

            Height = 1,

            BackColor =
                Color.FromArgb(40, 52, 78)
        };

        Controls.Add(headerBorder);

        headerBorder.BringToFront();

        titleBar.BringToFront();

        titleBar.MouseDown +=
            TitleBar_MouseDown;

        lblTitle = new Label
        {
            Text = "✦ DevGo",

            Font = new Font(
                "Segoe UI",
                20,
                FontStyle.Bold
            ),

            ForeColor = Color.White,

            AutoSize = true
        };

        titleBar.Controls.Add(lblTitle);

        lblSubTitle = new Label
        {
            Text =
                "Developer Workspace Launcher",

            Font = new Font(
                "Segoe UI",
                10
            ),

            ForeColor =
                Color.FromArgb(
                    160,
                    160,
                    160
                ),

            AutoSize = true
        };

        titleBar.Controls.Add(lblSubTitle);

        // ------------------------------------------------
        // WINDOW BUTTONS
        // ------------------------------------------------

        var btnClose = CreateWindowButton("✕");

        btnClose.Location =
            new Point(Width - 50, 10);

        btnClose.Click += (_, _) => Close();

        titleBar.Controls.Add(btnClose);

        var btnMinimize = CreateWindowButton("—");

        btnMinimize.Location =
            new Point(Width - 100, 10);

        btnMinimize.Click += (_, _) =>
        {
            WindowState =
        FormWindowState.Minimized;
        };

        titleBar.Controls.Add(btnMinimize);

        CenterTitle();

        lblSubTitle.Left =
            (titleBar.Width - lblSubTitle.Width)
            / 2;

        lblSubTitle.Top = 58;
    }

    private static Button CreateWindowButton(
      string text
  )
    {
        var button = new Button
        {
            Text = text,

            Width = 40,

            Height = 30,

            FlatStyle = FlatStyle.Flat,

            BackColor =
                Color.FromArgb(22, 22, 22),

            ForeColor = Color.White,

            Font = new Font(
                "Segoe UI",
                10,
                FontStyle.Bold
            ),

            Cursor = Cursors.Hand
        };

        button.FlatAppearance.BorderSize = 0;

        button.FlatAppearance.MouseOverBackColor =
            Color.FromArgb(45, 45, 45);

        button.FlatAppearance.MouseDownBackColor =
            Color.FromArgb(60, 60, 60);

        return button;
    }

    private void CreateSearchBox()
    {
        var lblSearch = new Label
        {
            Text = "Search Projects",

            Location = new Point(20, 110),

            AutoSize = true,

            Font = new Font(
                "Segoe UI",
                10,
                FontStyle.Bold
            ),

            ForeColor =
                Color.FromArgb(180, 180, 180)
        };

        Controls.Add(lblSearch);

        var searchPanel = CreateRoundedPanel(
            new Point(20, 135),
            new Size(840, 50),
            Color.FromArgb(18, 25, 38),
            20
        );

        Controls.Add(searchPanel);

        txtSearch = new TextBox
        {
            BorderStyle = BorderStyle.None,

            BackColor =
                Color.FromArgb(18, 25, 38),

            ForeColor = Color.White,

            Font = new Font(
                "Segoe UI",
                12
            ),

            Location = new Point(15, 12),

            Width = 800
        };

        txtSearch.TextChanged +=
            TxtSearch_TextChanged;

        searchPanel.Controls.Add(txtSearch);
    }

    private void CreateProjectList()
    {
        var lblProjects = new Label
        {
            Text = "Projects",

            Location = new Point(20, 200),

            AutoSize = true,

            Font = new Font(
                "Segoe UI",
                10,
                FontStyle.Bold
            ),

            ForeColor =
                Color.FromArgb(180, 180, 180)
        };

        Controls.Add(lblProjects);

        var listPanel = CreateRoundedPanel(
            new Point(20, 225),
            new Size(840, 400),
            Color.FromArgb(14, 20, 32),
            20
        );

        Controls.Add(listPanel);

        lblEmptyState = new Label
        {
            Text =
            "No projects found.\n\nAdd a workspace to begin.",

            AutoSize = false,

            Width = 400,

            Height = 80,

            TextAlign = ContentAlignment.MiddleCenter,

            Font = new Font(
            "Segoe UI",
            12,
            FontStyle.Regular
        ),

            ForeColor =
            Color.FromArgb(120, 140, 170),

            BackColor = Color.Transparent,

            Location = new Point(220, 140)
        };

        listPanel.Controls.Add(lblEmptyState);

        listProjects = new ListBox
        {
            BorderStyle = BorderStyle.None,

            Font = new Font(
                "Consolas",
                12
            ),

            BackColor =
                Color.FromArgb(14, 20, 32),

            ForeColor = Color.White,

            ItemHeight = 28,

            Location = new Point(15, 15),

            Width = 800,

            Height = 360
        };

        listProjects.DoubleClick +=
            ListProjects_DoubleClick;

        listPanel.Controls.Add(listProjects);
    }
    private void CreateButtons()
    {
        int buttonWidth = 150;

        int gap = 20;

        int totalWidth =
            (buttonWidth * 5) + (gap * 4);

        int startX =
            (ClientSize.Width - totalWidth) / 2;

        int y = 650;

        // ------------------------------------------------
        // REMOVE WORKSPACE
        // ------------------------------------------------

        var btnRemoveWorkspace = CreateButton(
            "🗑 Remove",
            new Point(startX, y)
        );

        btnRemoveWorkspace.Click +=
            BtnRemoveWorkspace_Click;

        Controls.Add(btnRemoveWorkspace);

        // ------------------------------------------------
        // ADD WORKSPACE
        // ------------------------------------------------

        var btnWorkspace = CreateButton(
            "📁 Add",
            new Point(
                startX + (buttonWidth + gap),
                y
            )
        );

        btnWorkspace.Click +=
            BtnWorkspace_Click;

        Controls.Add(btnWorkspace);

        // ------------------------------------------------
        // VS CODE
        // ------------------------------------------------

        btnCode = CreateButton(
            "💻 VS Code",
            new Point(
                startX + (buttonWidth + gap) * 2,
                y
            )
        );

        btnCode.Click += BtnCode_Click;

        Controls.Add(btnCode);

        // ------------------------------------------------
        // TMUX
        // ------------------------------------------------

        btnTmux = CreateButton(
            "🖥️ tmux",
            new Point(
                startX + (buttonWidth + gap) * 3,
                y
            )
        );

        btnTmux.Click += BtnTmux_Click;

        Controls.Add(btnTmux);

        // ------------------------------------------------
        // OPEN BOTH
        // ------------------------------------------------

        btnBoth = CreateButton(
            "🚀 Open Both",
            new Point(
                startX + (buttonWidth + gap) * 4,
                y
            )
        );

        btnBoth.Click += BtnBoth_Click;

        Controls.Add(btnBoth);
    }
    private void CenterTitle()
    {
        lblTitle.Left =
            (titleBar.Width - lblTitle.Width) / 2;

        lblTitle.Top = 10;

        lblSubTitle.Left =
            (titleBar.Width - lblSubTitle.Width) / 2;

        lblSubTitle.Top = 58;
    }

    private static Button CreateButton(
        string text,
        Point location
    )
    {
        var button = new Button
        {
            Text = text,

            Width = 150,

            Height = 50,

            Location = location,

            Cursor = Cursors.Hand,

            FlatStyle = FlatStyle.Flat,

            BackColor =
                Color.FromArgb(20, 28, 42),

            ForeColor = Color.White,

            Font = new Font(
                "Segoe UI",
                10,
                FontStyle.Bold
            )
        };

        button.FlatAppearance.BorderColor =
            Color.FromArgb(60, 60, 60);

        button.FlatAppearance
            .MouseOverBackColor =
            Color.FromArgb(45, 45, 45);

        button.FlatAppearance
            .MouseDownBackColor =
            Color.FromArgb(45, 45, 45);

        var path = new GraphicsPath();

        int radius = 20;

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
            button.Width - radius,
            0,
            radius,
            radius,
            270,
            90
        );

        path.AddArc(
            button.Width - radius,
            button.Height - radius,
            radius,
            radius,
            0,
            90
        );

        path.AddArc(
            0,
            button.Height - radius,
            radius,
            radius,
            90,
            90
        );

        path.CloseFigure();

        button.Region = new Region(path);

        return button;
    }

    private Panel CreateRoundedPanel(
      Point location,
      Size size,
      Color backColor,
      int radius
  )
    {
        var panel = new Panel
        {
            Location = location,

            Size = size,

            BackColor = backColor
        };

        var path = new GraphicsPath();

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
            size.Width - radius,
            0,
            radius,
            radius,
            270,
            90
        );

        path.AddArc(
            size.Width - radius,
            size.Height - radius,
            radius,
            radius,
            0,
            90
        );

        path.AddArc(
            0,
            size.Height - radius,
            radius,
            radius,
            90,
            90
        );

        path.CloseFigure();

        panel.Region = new Region(path);

        return panel;
    }
}
