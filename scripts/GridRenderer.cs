using Godot;
using System;
using System.Collections.Generic;

namespace SquareVerse;

public partial class GridRenderer : Node2D
{
    [Export] public int CellWidth = 10;
    [Export] public int CellHeight = 10;
    
    private const double REDRAW_TIME = 5/1000d; //s
    
    private double timeUntilNextRedraw = REDRAW_TIME;
    
    public override void _Draw()
    {
        var grid = GridManager.Instance.PrevGrid;
        var gridManager = GridManager.Instance;
        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                var cell = grid[x, y];
                // skip empty cells
                if (cell.Type == GridManager.EMPTY) continue;
                var kind = gridManager.Kinds[cell.Type];
                DrawRect(new Rect2(x*CellWidth,y*CellHeight,CellWidth,CellHeight), kind.Color);
            }
        }
    }

    public override void _Process(double delta)
    {
        if (timeUntilNextRedraw <= 0)
        {
            timeUntilNextRedraw = REDRAW_TIME;
            // redraw the grid after the time is up.
            QueueRedraw();
        }
        timeUntilNextRedraw -= delta;
    }
}
