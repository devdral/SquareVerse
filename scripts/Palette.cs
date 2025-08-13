using Godot;
using System;
using System.Collections.Generic;

namespace SquareVerse;

public partial class Palette : HBoxContainer
{

    public void AddKind(int kindId)
    {
        GD.Print("AddKind");
        var button = new PaletteButton(kindId);
        
        AddChild(button);
    }
}
