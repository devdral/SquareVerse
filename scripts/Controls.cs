using Godot;
using System;
using SquareVerse.Utility;

namespace SquareVerse;

public partial class Controls : HBoxContainer
{
    private Button _pausePlay;
    private Button _step;

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
