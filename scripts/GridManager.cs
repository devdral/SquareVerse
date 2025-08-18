using Godot;
using System;
using System.Collections.Generic;
using SquareVerse.Utility;

public partial class GridManager : Node
{
    public const int EMPTY = 0;
    
    public static GridManager Instance {  get; private set; }

    public Grid Grid { get; set; }
    public Grid PrevGrid { get; set; }

    private Grid _gridA = new Grid(100, 100);
    private Grid _gridB = new Grid(100, 100);
    private bool _useGridB = false;

    public GridManager()
    {
        Grid = _gridA;
        PrevGrid = _gridB;
        Kinds = [new Kind(Color.Color8(0,0,0,0))];
        Instance = this;
    }

    public void SwapGrid()
    {
        (Grid, PrevGrid) = (PrevGrid, Grid);
    }
    
    public List<Kind> Kinds { get; set; }
}
