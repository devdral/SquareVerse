using Godot;
using System;
using SquareVerse.Utility;

namespace SquareVerse;

public partial class EditCursor : Node2D
{
    public override void _Draw()
    {
        DrawRect(new Rect2(0,0,10,10), new Color("#b3c497"));
    }

    public override void _Process(double delta)
    {
        Visible = false;
        if (UIManager.Instance.Mode == UIManager.EDIT)
        {
            var mousePos = GetGlobalMousePosition();
            int x = (int)Mathf.Floor(mousePos.X / 10);
            int y = (int)Mathf.Floor(mousePos.Y / 10);
            var grid = GridManager.Instance.Grid;
            if (x >= 0 && x < grid.Width && y >= 0 && y < grid.Height)
            {
                Position = new Vector2(x * 10, y * 10);
                if (Input.IsMouseButtonPressed(MouseButton.Left))
                {
                    grid[x, y] = new Cell(UIManager.Instance.SelectedKind);                    
                } else if (Input.IsMouseButtonPressed(MouseButton.Right))
                {
                    grid[x, y] = new Cell(0);
                }
                Visible = true;
            }
        }
    }
}
