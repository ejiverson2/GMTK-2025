using Godot;
using System;

[Tool]
public partial class DebugCanvas : CanvasLayer
{
    [Export(PropertyHint.None, "Tail Size - Number of points that make up the tail")]
    public bool showTSz
    {
        get => _showTSz;
        set
        {
            if (_showTSz == value) {
                return;
            }
            _showTSz = value;
            UpdateShowTSz();
        }
    }
    private bool _showTSz = true;
    private void UpdateShowTSz()
    {
        UpdateHBoxContainer(_showTSz, "TopLeftPanel/VBoxContainer/TailSizeHBox");
        bool newTopLeftPanel = _showTSz || _showTTm;
        if (newTopLeftPanel != _showTopLeftPanel) {
            _showTopLeftPanel = newTopLeftPanel;
            UpdatePanel(_showTopLeftPanel, "TopLeftPanel");
        }
    }
    private Label _TailSizeLabel;

    [Export(PropertyHint.None, "Tail Time - time in ms spent finding tail loop")]
    public bool showTTm
    {
        get => _showTTm;
        set
        {
            if (_showTTm == value) {
                return;
            }
            _showTTm = value;
            UpdateShowTTm();
        }
    }
    private bool _showTTm = true;
    private void UpdateShowTTm()
    {
        UpdateHBoxContainer(_showTTm, "TopLeftPanel/VBoxContainer/TailTimeHBox");
        bool newTopLeftPanel = _showTSz || _showTTm;
        if (newTopLeftPanel != _showTopLeftPanel) {
            _showTopLeftPanel = newTopLeftPanel;
            UpdatePanel(_showTopLeftPanel, "TopLeftPanel");
        }
    }
    private Label _TailTimeLabel;

    [Export(PropertyHint.None, "FPS - frames rendered per second")]
    public bool ShowFPS
    {
        get => _showFPS;
        set
        {
            if (_showFPS == value) {
                return;
            }
            _showFPS = value;
            UpdateShowFPS();
        }
    }
    private bool _showFPS = true;
    private void UpdateShowFPS()
    {
        UpdateHBoxContainer(_showFPS, "TopRightPanel/VBoxContainer/FPSHBox");
        bool newTopRightPanel = _showFPS || _showPrc || _showPhy || _showNav;
        if (newTopRightPanel != _showTopRightPanel) {
            _showTopRightPanel = newTopRightPanel;
            UpdatePanel(_showTopRightPanel, "TopRightPanel");
        }
    }
    private Label _FPSLabel;

    [Export(PropertyHint.None, "Process Loop time - time in ms in the process calls")]
    public bool showPrc
    {
        get => _showPrc;
        set
        {
            if (_showPrc == value) {
                return;
            }
            _showPrc = value;
            UpdateShowPrc();
        }
    }
    private bool _showPrc = true;
    private void UpdateShowPrc()
    {
        UpdateHBoxContainer(_showPrc, "TopRightPanel/VBoxContainer/ProcessHBox");
        bool newTopRightPanel = _showFPS || _showPrc || _showPhy || _showNav;
        if (newTopRightPanel != _showTopRightPanel) {
            _showTopRightPanel = newTopRightPanel;
            UpdatePanel(_showTopRightPanel, "TopRightPanel");
        }
    }
    private Label _ProcessLabel;

    [Export(PropertyHint.None, "Physics Time - time in ms in the process calls")]
    public bool showPhy
    {
        get => _showPhy;
        set
        {
            if (_showPhy == value) {
                return;
            }
            _showPhy = value;
            UpdateShowPhy();
        }
    }
    private bool _showPhy = true;
    private void UpdateShowPhy()
    {
        UpdateHBoxContainer(_showPhy, "TopRightPanel/VBoxContainer/PhysicsHBox");
        bool newTopRightPanel = _showFPS || _showPrc || _showPhy || _showNav;
        if (newTopRightPanel != _showTopRightPanel) {
            _showTopRightPanel = newTopRightPanel;
            UpdatePanel(_showTopRightPanel, "TopRightPanel");
        }
    }
    private Label _PhysicsLabel;

    [Export(PropertyHint.None, "Navigation Time - time in ms in the navigation calls")]
    public bool showNav
    {
        get => _showNav;
        set
        {
            if (_showNav == value) {
                return;
            }
            _showNav = value;
            UpdateShowNav();
        }
    }
    private bool _showNav = true;
    private void UpdateShowNav()
    {
        UpdateHBoxContainer(_showNav, "TopRightPanel/VBoxContainer/NavigationHBox");
        bool newTopRightPanel = _showFPS || _showPrc || _showPhy || _showNav;
        if (newTopRightPanel != _showTopRightPanel) {
            _showTopRightPanel = newTopRightPanel;
            UpdatePanel(_showTopRightPanel, "TopRightPanel");
        }
    }
    private Label _NavigationLabel;

    bool _showTopLeftPanel = true;
    bool _showTopRightPanel = true;

    private void UpdateHBoxContainer(bool showHBox, string nodePath)
    {
        if (!Engine.IsEditorHint()) {
            return;
        }
        HBoxContainer hBox = GetNode<HBoxContainer>(nodePath);
        if (hBox == null) {
            return;
        }
        if (showHBox) {
            hBox.Visible = true;
        } else {
            hBox.Visible = false;
        }
        hBox.QueueRedraw();
    }

    private void UpdatePanel(bool showPanel, string nodePath)
    {
        if (!Engine.IsEditorHint()) {
            return;
        }
        PanelContainer panel = GetNode<PanelContainer>(nodePath);
        if (panel == null) {
            return;
        }
        if (showPanel) {
            panel.Visible = true;
        } else {
            panel.Visible = false;
        }
        panel.QueueRedraw();
    }

    private double accDelta = 0;

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) {
            UpdateShowFPS();
            UpdateShowPrc();
            UpdateShowPhy();
            UpdateShowNav();
            UpdateShowTSz();
            UpdateShowTTm();
        }
        _FPSLabel = GetNode<Label>("TopRightPanel/VBoxContainer/FPSHBox/FPS");
        _ProcessLabel = GetNode<Label>("TopRightPanel/VBoxContainer/ProcessHBox/Process");
        _PhysicsLabel = GetNode<Label>("TopRightPanel/VBoxContainer/PhysicsHBox/Physics");
        _NavigationLabel = GetNode<Label>("TopRightPanel/VBoxContainer/NavigationHBox/Navigation");
        _TailSizeLabel = GetNode<Label>("TopLeftPanel/VBoxContainer/TailSizeHBox/TailSize");
        _TailTimeLabel = GetNode<Label>("TopLeftPanel/VBoxContainer/TailTimeHBox/TailTime");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        accDelta += delta;
        if (accDelta > 0.5) {
            _FPSLabel.Text = Performance.GetMonitor(Performance.Monitor.TimeFps).ToString("F0");
            _ProcessLabel.Text = (Performance.GetMonitor(Performance.Monitor.TimeProcess) * 1000).ToString("F2");
            _PhysicsLabel.Text = (Performance.GetMonitor(Performance.Monitor.TimePhysicsProcess) * 1000).ToString("F2");
            _NavigationLabel.Text = (Performance.GetMonitor(Performance.Monitor.TimeNavigationProcess) * 1000).ToString("F2");
            _TailSizeLabel.Text = CustomPerformance.getInstance().GetMetric("TailSize").ToString("F2");
            _TailTimeLabel.Text = CustomPerformance.getInstance().GetMetric("TailTime").ToString("F2");
            accDelta = 0;
        }
    }

}
