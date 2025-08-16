using System.Collections.Generic;
using Godot;

namespace SquareVerse.Utility;

public partial class RuleResource : Resource
{
    [Export] public int TL { get; set; }
    [Export] public int Top { get; set; } 
    [Export] public int TR { get; set; }
    [Export] public int Left { get; set; }
    [Export] public int Right { get; set; }
    [Export] public int BL { get; set; }
    [Export] public int Bottom { get; set; }
    [Export] public int BR { get; set; }
    
    [Export] public int OldCenter { get; set; }
    [Export] public int NewCenter { get; set; }

    public RuleResource(Neighborhood neighborhood, int oldCenter, int newCenter)
    {
        TL = neighborhood.TL;
        Top = neighborhood.Top;
        TR = neighborhood.TR;
        Left = neighborhood.Left;
        Right = neighborhood.Right;
        BL = neighborhood.BL;
        Bottom = neighborhood.Bottom;
        BR = neighborhood.BR;
        
        OldCenter = oldCenter;
        NewCenter = newCenter;
    }

    public RuleResource() : this(new Neighborhood(), 0, 0) {}
}