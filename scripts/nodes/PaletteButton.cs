using Godot;
using System;
using SquareVerse.Utility;

namespace SquareVerse;

public partial class PaletteButton : Button
{
    public Color Color { get; private set; }
    public int Kind { get; private set; }
    
    public PaletteButton(int kindId)
    {
        Kind = kindId;
        Color = GridManager.Instance.Kinds[kindId].Color;
        Pressed += OnClick;
    }

    public void OnClick()
    {
        UIManager.Instance.SelectedKind = Kind;
    }

    public override void _Ready()
    {
        var styleBox = new StyleBoxFlat();
        styleBox.BorderColor = new Color("#a0c7f3");
        styleBox.BorderWidthBottom = 2;
        styleBox.BorderWidthLeft   = 2;
        styleBox.BorderWidthRight  = 2;
        styleBox.BorderWidthTop    = 2;
        styleBox.BgColor = Color;
        AddThemeStyleboxOverride("normal", styleBox);
        AddThemeStyleboxOverride("focus", styleBox);
        AddThemeStyleboxOverride("hover", styleBox);
        AddThemeStyleboxOverride("pressed", styleBox);
        SetVSizeFlags(SizeFlags.ShrinkCenter);
        CustomMinimumSize = new Vector2(30, 30);
    }
}
