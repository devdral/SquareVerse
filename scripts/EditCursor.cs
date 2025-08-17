using Godot;
using System;
using SquareVerse.Utility;
using Vector2 = Godot.Vector2;

namespace SquareVerse;

public partial class EditCursor : Node2D
{
    private Vector2I? _lastPos;
    
    public override void _Draw()
    {
        DrawRect(new Rect2(0,0,10,10), new Color("#b3c497"));
    }

    public override void _Input(InputEvent @event)
    {
        var mousePos = GetGlobalMousePosition();
        int x = (int)Mathf.Floor(mousePos.X / 10);
        int y = (int)Mathf.Floor(mousePos.Y / 10);
        var grid = GridManager.Instance.Grid;
        if (@event is InputEventMouseMotion)
        {
            Visible = false;
            if (UIManager.Instance.Mode == UIManager.EDIT)
            {
                if (x >= 0 && x < grid.Width && y >= 0 && y < grid.Height)
                {
                    Position = new Vector2(x * 10, y * 10);
                    if (Input.IsMouseButtonPressed(MouseButton.Left))
                    {
                        if (_lastPos is not null)
                        {
                            var line = Geometry2D.BresenhamLine((Vector2I)_lastPos, new Vector2I(x, y));
                            foreach (var point in line)
                            {
                                grid[point] = new Cell(UIManager.Instance.SelectedKind);
                            }
                        }
                        else
                        {
                            grid[x, y] = new Cell(UIManager.Instance.SelectedKind);
                        }

                        _lastPos = new Vector2I(x, y);
                    }
                    else if (Input.IsMouseButtonPressed(MouseButton.Right))
                    {
                        if (_lastPos is not null)
                        {
                            var line = Geometry2D.BresenhamLine((Vector2I)_lastPos, new Vector2I(x, y));
                            foreach (var point in line)
                            {
                                grid[point] = new Cell(0);
                            }
                        }
                        else
                        {
                            grid[x, y] = new Cell(0);
                        }

                        _lastPos = new Vector2I(x, y);
                    }
                    else
                    {
                        _lastPos = null;
                    }

                    Visible = true;
                }
            }
        }
        else if (@event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.Pressed && UIManager.Instance.Mode == UIManager.EDIT)
            {
                if (!(x >= 0 && x < grid.Width && y >= 0 && y < grid.Height))
                    return;
                if (mouseButton.ButtonIndex == MouseButton.Left)
                {
                    grid[x, y] = new Cell(UIManager.Instance.SelectedKind);
                }
                else if (mouseButton.ButtonIndex == MouseButton.Right)
                {
                    grid[x, y] = new Cell(0);
                }
            }
        }
    }
}
