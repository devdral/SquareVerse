using Godot;
using System;

namespace SquareVerse;

public partial class PaintableButton : Button
{
    public int Kind;
    public Color Color => GridManager.Instance.Kinds[Kind].Color;
    private TextureRect _wildcardX;

    public PaintableButton(int kind = 0)
    {
        Kind = kind;
        _wildcardX = new TextureRect();
        _wildcardX.Texture = ResourceLoader.Load<Texture2D>("res://assets/icons/x.svg");
        _wildcardX.AnchorBottom = 1f;
        _wildcardX.AnchorRight = 1f;
        _wildcardX.Hide();
        AddChild(_wildcardX);
        Pressed += OnPaint;
        SetStyles();
        ButtonMask = MouseButtonMask.Left | MouseButtonMask.Middle | MouseButtonMask.Right;
    }

    private void SetStyles()
    {
        // Wildcard
        if (Kind == -1)
        {
            _wildcardX.Show();
            var styleBoxWc = new StyleBoxFlat();
            styleBoxWc.BorderColor = Colors.White;
            styleBoxWc.BorderWidthBottom = 2;
            styleBoxWc.BorderWidthLeft   = 2;
            styleBoxWc.BorderWidthRight  = 2;
            styleBoxWc.BorderWidthTop    = 2;
            styleBoxWc.BgColor = GridManager.Instance.Kinds[GridManager.EMPTY].Color;
            AddThemeStyleboxOverride("pressed", styleBoxWc);
            AddThemeStyleboxOverride("hover", styleBoxWc);
            AddThemeStyleboxOverride("focus", styleBoxWc);
            AddThemeStyleboxOverride("normal", styleBoxWc);
            CustomMinimumSize = new Vector2(70, 70);
            ActionMode = ActionModeEnum.Press;
            return;
        }
        _wildcardX.Hide();
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
        ActionMode = ActionModeEnum.Press;
    }

    public void OnPaint()
    {
        if (Input.IsMouseButtonPressed(MouseButton.Left))
        {
            Kind = UIManager.Instance.SelectedKind;   
        } else if (Input.IsMouseButtonPressed(MouseButton.Right))
        {
            Kind = 0;
        } else if (Input.IsMouseButtonPressed(MouseButton.Middle))
        {
            Kind = -1;
        }
        SetStyles();
    }
}
