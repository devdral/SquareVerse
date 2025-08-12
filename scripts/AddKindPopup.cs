using Godot;
using System;
using SquareVerse.Utility;

public partial class AddKindPopup : Window
{
    private ColorPickerButton colorPicker;
    
    public void OnAddKindPressed()
    {
        Show();
    }

    public void OnConfirmPressed()
    {
        Hide();
        GridManager.Instance.Kinds.Add(new Kind(
            colorPicker.Color
            ));
    }

    public void OnClose()
    {
        Hide();
        colorPicker.Color = new Color("black");
    }
}
