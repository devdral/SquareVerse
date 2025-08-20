using Godot;
using System;

public partial class ExportPopup : Window
{
    private string _path;
    private int _format;
    
    private LineEdit _pathLine;
    private FileDialog _fileDialog;

    public override void _Ready()
    {
        _pathLine = GetNode<LineEdit>("FormItems/PathPick/LineEdit");
    }

    public void OnFilePathChanged(string newPath)
    {
        _path = newPath;
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
        _path = _fileDialog.CurrentPath;
        _pathLine.Text = _fileDialog.CurrentPath;
        _fileDialog.QueueFree();
    }

    public void OnFormatChanged(int newFormat)
    {
        _format = newFormat;
    }

    public void OnExport()
    {
        var grid = GridManager.Instance.Grid;
        var kinds = GridManager.Instance.Kinds;
        Image img = Image.CreateEmpty(100, 100, false, Image.Format.Rgba8);
        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                var cell = grid[x, y];
                var kind = kinds[cell.Type];
                img.SetPixel(x,y, kind.Color);
            }
        }

        switch (_format)
        {
            case 0:
                img.SavePng(_path);
                break;
            case 1:
                img.SaveJpg(_path);
                break;
            case 2:
                img.SaveWebp(_path);
                break;
        }
        Hide();
    }

    public void OnOpen()
    {
        Show();
    }

    public void OnClose()
    {
        Hide();
    }
}
