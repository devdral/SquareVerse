using Godot;
using System;
using SquareVerse.Utility;

public partial class SelectSizePopup : Window
{
    public void OnOpen()
    {
        Show();
    }

    public void OnClose()
    {
        Hide();
    }
    
    public void OnWidthChanged(float value)
    {
        UIManager.Instance.GridSize = UIManager.Instance.GridSize with { X = (int)value };
    }
    
    public void OnHeightChanged(float value)
    {
        UIManager.Instance.GridSize = UIManager.Instance.GridSize with { Y = (int)value };
    }

    public void OnApply()
    {
        GridManager.Instance.Grid = new Grid(
            UIManager.Instance.GridSize.X,
            UIManager.Instance.GridSize.Y
            );
        GridManager.Instance.PrevGrid = new Grid(
            UIManager.Instance.GridSize.X,
            UIManager.Instance.GridSize.Y
        );
        Hide();
    }
}
