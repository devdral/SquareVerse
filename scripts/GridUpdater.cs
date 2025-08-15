using Godot;
using System;
using System.Runtime.CompilerServices;
using SquareVerse.Utility;

namespace SquareVerse;

public partial class GridUpdater : Node
{
    // Physics process is a fixed update method. Thus, it is appropriate for simulations, especially
    // ones that take a variable amount of time to execute.
    public override void _Process(double delta)
    {
        if (!UIManager.Instance.Paused)
            Step();
        
        if (Input.IsActionPressed("step"))
        {
            Update();
        }
    }

    public void Step()
    {
        for (int i = 0; i < 8; i++)
            Update();
    }

    public void Update()
    {
        var grid = GridManager.Instance.Grid;
        var kinds = GridManager.Instance.Kinds;
        var newGrid = new Grid(grid);
        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                var cell = grid[x, y];
                var kind = kinds[cell.Type];
                foreach (var rule in kind.Rules)
                {
                    if (CheckNeighborhood(
                            grid,
                            x, y,
                            rule.Neighborhood
                        ))
                    {
                        newGrid[x, y] = new Cell(rule.NewCenter);
                        break;
                    }
                }
            }
        }
        GridManager.Instance.Grid = newGrid;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool CheckNeighborhood(Grid grid, int atX, int atY, Neighborhood neighborhood)
    {
        if (!CompareCells(grid[atX - 1, atY - 1].Type, neighborhood.TL))
            return false;
        if (!CompareCells(grid[atX, atY - 1].Type, neighborhood.Top))
            return false;
        if (!CompareCells(grid[atX + 1, atY - 1].Type, neighborhood.TR))
            return false;
        if (!CompareCells(grid[atX - 1, atY].Type, neighborhood.Left))
            return false;
        // Center cell already known
        if (!CompareCells(grid[atX + 1, atY].Type, neighborhood.Right))
            return false;
        if (!CompareCells(grid[atX - 1, atY + 1].Type, neighborhood.BL))
            return false;
        if (!CompareCells(grid[atX, atY + 1].Type, neighborhood.Bottom))
            return false;
        if (!CompareCells(grid[atX + 1, atY + 1].Type, neighborhood.BR))
            return false;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool CompareCells(int kind, int pattern)
    {
        // empty in rules is a wildcard
        return kind == pattern || pattern == -1;
    }
}