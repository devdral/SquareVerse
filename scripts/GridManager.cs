using Godot;
using System;
using System.Collections.Generic;
using SquareVerse.Utility;

public partial class GridManager : Node
{
    public const int EMPTY = 0;
    
    public static GridManager Instance {  get; private set; }
    
    public Grid Grid { get; set; }
    
    public List<Kind> Kinds { get; set; }

    public override void _Ready()
    {
        Grid = new Grid(100,100);
        Kinds = [new Kind(Color.Color8(0,0,0,0))];
        Instance = this;
    }
}
