using Godot;
using System;
using SquareVerse.Utility;

public partial class AddKindPopup : Window
{
	[Signal]
	public delegate void KindCreatedEventHandler(int kindId);
	
	private ColorPickerButton colorPicker;

	public override void _Ready()
	{
		colorPicker = GetNode<ColorPickerButton>("FormItems/ColorPick/ColorPickerButton");
	}
	
	public void OnAddKindPressed()
	{
		Show();
	}

	public void OnConfirmPressed()
	{
		Hide();
		var index = GridManager.Instance.Kinds.Count; 
		GridManager.Instance.Kinds.Add(new Kind(
			colorPicker.Color
			));
		EmitSignalKindCreated(index);
	}

	public void OnClose()
	{
		Hide();
		colorPicker.Color = new Color("black");
	}
}
