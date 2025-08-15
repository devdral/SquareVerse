using Godot;
using System;

namespace SquareVerse;

public partial class PaintableButton : Button
{
    public int Kind;
    public Color Color => GridManager.Instance.Kinds[Kind].Color;

    public PaintableButton(int kind = 0)
    {
        Kind = kind;
        SetStyles();
        Pressed += OnPaint;
    }

    private void SetStyles()
    {
        var styleBox = new StyleBoxFlat();
        styleBox.BorderColor = Colors.White;
        styleBox.BorderWidthBottom = 2;
        styleBox.BorderWidthLeft   = 2;
        styleBox.BorderWidthRight  = 2;
        styleBox.BorderWidthTop    = 2;
        styleBox.BgColor = GridManager.Instance.Kinds[Kind].Color;
        AddThemeStyleboxOverride("pressed", styleBox);
        AddThemeStyleboxOverride("hover", styleBox);
        AddThemeStyleboxOverride("focus", styleBox);
        AddThemeStyleboxOverride("normal", styleBox);
        CustomMinimumSize = new Vector2(70, 70);
    }

    public void OnPaint()
    {
        Kind = UIManager.Instance.SelectedKind;
        SetStyles();
    }
}
