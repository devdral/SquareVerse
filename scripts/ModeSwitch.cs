using Godot;
using System;

public partial class ModeSwitch : VBoxContainer
{
	private Button EnterEdit;
	private Button EnterView;

	public override void _Ready()
	{
		EnterEdit = (Button)GetNode("EnterEdit");
		EnterView = (Button)GetNode("EnterView");
	}

	public override void _Process(double delta)
	{
		EnterEdit.Disabled = UIManager.Instance.Mode;
		EnterView.Disabled = !UIManager.Instance.Mode;
		if (UIManager.Instance.Mode == UIManager.VIEW)
		{
			GetTree().CallGroup("EditControl", "hide");
		}
		else
		{
			GetTree().CallGroup("EditControl", "show");
		}
	}

	public void OnEnterEdit()
	{
		UIManager.Instance.Mode = UIManager.EDIT;
		EnterEdit.Disabled = true;
		EnterView.Disabled = false;
	}
	
	public void OnEnterView()
	{
		UIManager.Instance.Mode = UIManager.VIEW;
		EnterView.Disabled = true;
		EnterEdit.Disabled = false;
	}
}
