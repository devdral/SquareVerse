using Godot;
using System;
using SquareVerse.Utility;

namespace SquareVerse;

public partial class SaveAsPopup : Window
{
    private FileDialog _fileDialog;
    private LineEdit _pathLine;

    public override void _Ready()
    {
        _pathLine = GetNode<LineEdit>("FormItems/PathPick/LineEdit");
    }

    public void OnOpen()
    {
        Show();
    }

    public void OnClose()
    {
        Hide();
    }
    
    public void OnFilePathChanged(string newPath)
    {
        UIManager.Instance.SaveConfig.FilePath = newPath;
    }

    public void OnIncludeRulesToggled(bool newState)
    {
        UIManager.Instance.SaveConfig.IncludeRules = newState;
    }
    
    public void OnIncludeBoardToggled(bool newState)
    {
        UIManager.Instance.SaveConfig.IncludeBoard = newState;
    }

    public void OnSave()
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
            msgBox.SetInitialPosition(WindowInitialPosition.CenterMainWindowScreen);
            GetTree().Root.AddChild(msgBox);
            msgBox.Show();
        }
        Hide();
    }
    
    public void OnBrowseClicked()
    {
        _fileDialog = new FileDialog();
        _fileDialog.FileMode = FileDialog.FileModeEnum.SaveFile;
        _fileDialog.ModeOverridesTitle = false;
        _fileDialog.Title = "Select Location";
        _fileDialog.Access = FileDialog.AccessEnum.Filesystem;
        _fileDialog.InitialPosition = WindowInitialPosition.CenterMainWindowScreen;
        _fileDialog.SetFlag(Flags.AlwaysOnTop, true);
        _fileDialog.GetOkButton().Pressed += FinishBrowsing;
        GetTree().Root.AddChild(_fileDialog);
        _fileDialog.Show();
    }

    private void FinishBrowsing()
    {
        UIManager.Instance.SaveConfig.FilePath = _fileDialog.CurrentPath;
        _pathLine.Text = _fileDialog.CurrentPath;
        _fileDialog.QueueFree();
    }
}
