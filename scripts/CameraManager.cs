using Godot;
using System;

namespace SquareVerse;

public partial class CameraManager : Camera2D
{
    [Export] public float PanSpeed = 25f;
    [Export] public float ZoomSpeed = 5f;
    
    private bool _panning = false;

    public override void _Ready()
    {
        var grid = GridManager.Instance.Grid;
        var gridCenter = new Vector2(grid.Width*10 / 2f, grid.Height*10 / 2f);
        Position = gridCenter;
    }
    
    public override void _Input(InputEvent ev)
    {
        if (UIManager.Instance.Mode == UIManager.EDIT) return;
        if (ev is InputEventMouseMotion mouseMotion)
        {
            if (_panning)
            {
                var vel = mouseMotion.Relative;
                vel *= PanSpeed;
                vel /= Zoom.X;
                vel = -vel;
                Position += vel;
            }
        }
        else if (ev is InputEventMouseButton mouseButton)
        {
            if (mouseButton.ButtonIndex == MouseButton.WheelUp)
            {
                var zoomAmount = mouseButton.Factor * ZoomSpeed;
                Zoom += new Vector2(zoomAmount, zoomAmount);
            }
            else if (mouseButton.ButtonIndex == MouseButton.WheelDown)
            {
                var zoomAmount = -(mouseButton.Factor * ZoomSpeed);
                Zoom += new Vector2(zoomAmount, zoomAmount);
            }
            else if (mouseButton.ButtonIndex == MouseButton.Left && mouseButton.Pressed)
            {
                _panning = true;
            }
            else if (mouseButton.ButtonIndex == MouseButton.Left && !mouseButton.Pressed)
            {
                _panning = false;
            }
        }
    }
}
