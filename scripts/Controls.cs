using Godot;
using System;
using SquareVerse.Utility;

namespace SquareVerse;

public partial class Controls : HBoxContainer
{
    [Signal]
    public delegate void SaveAsRequestedEventHandler();
    
    private Button _pausePlay;
    private Button _step;
    private FileDialog _fileDialog;

    public override void _Ready()
    {
        _pausePlay = GetNode<Button>("PausePlay");
        _step = GetNode<Button>("Step");
    }

    public void OnReset()
    {
        var grid = GridManager.Instance.Grid;
        GridManager.Instance.Grid = new Grid(grid.Width, grid.Height);
    }

    public void OnTogglePause()
    {
        UIManager.Instance.Paused = !UIManager.Instance.Paused;
        _pausePlay.Text = UIManager.Instance.Paused ? "Play" : "Pause";
    }

    public void OnOpen()
    {
        _fileDialog = new FileDialog();
        _fileDialog.ModeOverridesTitle = false;
        _fileDialog.Title = "Open Project";
        _fileDialog.Access = FileDialog.AccessEnum.Filesystem;
        _fileDialog.FileMode = FileDialog.FileModeEnum.OpenFile;
        _fileDialog.InitialPosition = Window.WindowInitialPosition.CenterMainWindowScreen;
        _fileDialog.SetFlag(Window.Flags.AlwaysOnTop, true);
        _fileDialog.FileSelected += FinishBrowsing;
        GetTree().Root.AddChild(_fileDialog);
        _fileDialog.Show();
    }

    private void FinishBrowsing(string _)
    {
        UIManager.Instance.SelectedKind = GridManager.EMPTY;
        UIManager.Instance.SaveConfig = new SaveConfig();
        UIManager.Instance.SaveConfig.FilePath = _fileDialog.CurrentPath;
        ProjectFile projectFile = ResourceLoader.Load<ProjectFile>(_fileDialog.CurrentPath);
        var palette = GetNode<Palette>("Palette");
        foreach (var child in palette.GetChildren())
        {
            child.QueueFree();
        }

        GridManager.Instance.Kinds = [];
        for (var index = 0; index < projectFile.Kinds.Length; index++)
        {
            var color = projectFile.Kinds[index];
            GridManager.Instance.Kinds.Add(new Kind(color));
            if (index > 0)
                palette.AddChild(new PaletteButton(GridManager.Instance.Kinds.Count - 1));
        }

        if (projectFile.Grid.Length <= 0)
        {
            UIManager.Instance.SaveConfig.IncludeBoard = false;
            var oldGrid = GridManager.Instance.Grid;
            GridManager.Instance.Grid = new Grid(oldGrid.Width, oldGrid.Height);
        }   
        else
        {
            UIManager.Instance.SaveConfig.IncludeBoard = true;
            var grid = GridManager.Instance.Grid;
            int i = 0;
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    grid[x, y] = new Cell(projectFile.Grid[i]);
                    i++;
                }
            }
        }

        var ruleEditor = GetNode<VBoxContainer>("../../../RulesEditorPopup/FormItems");
        var ruleEditors = ruleEditor.GetNode<VBoxContainer>("ScrollContainer/Items");
        foreach (var child in ruleEditors.GetChildren())
        {
            child.QueueFree();
        }

        foreach (var kind in GridManager.Instance.Kinds)
        {
            kind.Rules = [];
        }
        if (projectFile.Rules.Count <= 0)
        {
            ruleEditor.GetNode<Label>("NoRules").Show();
            UIManager.Instance.SaveConfig.IncludeRules = false;
        }
        else
        {
            ruleEditor.GetNode<Label>("NoRules").Hide();            
            UIManager.Instance.SaveConfig.IncludeBoard = true;
            for (var i = 0; i < projectFile.Rules.Count; i++)
            {
                var rule = projectFile.Rules[i];
                var ruleStruct = new Rule(
                    new Neighborhood(
                        rule.TL, rule.Top, rule.TR,
                        rule.Left, rule.Right,
                        rule.BL, rule.Bottom, rule.BR
                    ), rule.NewCenter
                );
                GridManager.Instance.Kinds[rule.OldCenter].Rules.Add(ruleStruct);
                ruleEditors.AddChild(new RuleEditor(rule.OldCenter, ruleStruct, i));
            }
        }
    }

    public void OnSaveClicked()
    {
        if (UIManager.Instance.SaveConfig.FilePath != "")
        {
            var saveConfig = UIManager.Instance.SaveConfig;
            if (saveConfig is null ||
                saveConfig.FilePath == "")
                return;
            var fileResource = new ProjectFile();
            fileResource.StorePalette();
            if (saveConfig.IncludeBoard)
                fileResource.StoreGrid();
            if (saveConfig.IncludeRules)
                fileResource.StoreRules();
            var error = ResourceSaver.Save(fileResource, UIManager.Instance.SaveConfig.FilePath);
            if (error != Error.Ok)
            {
                var msgBox = new AcceptDialog();
                msgBox.DialogText = "ERROR: " + error;
                msgBox.SetInitialPosition(Window.WindowInitialPosition.CenterMainWindowScreen);
                GetTree().Root.AddChild(msgBox);
                msgBox.Show();
            }
        }
        else
        {
            EmitSignalSaveAsRequested();
        }
    }

    public override void _Process(double delta)
    {
        if (!UIManager.Instance.Paused)
        {
            _step.Hide();
        }
        else
        {
            _step.Show();
        }
    }
}
