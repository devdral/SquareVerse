using Godot;
using System;
using SquareVerse;
using SquareVerse.Utility;

public partial class RuleEditor : VBoxContainer
{
    private PaintableButton _tlColor     = new();
    private PaintableButton _topColor    = new();
    private PaintableButton _trColor     = new();
    private PaintableButton _leftColor   = new();
    private PaintableButton _centerColor = new();
    private PaintableButton _rightColor  = new();
    private PaintableButton _blColor     = new();
    private PaintableButton _bottomColor = new();
    private PaintableButton _brColor     = new();
    private PaintableButton _newCenter   = new();
    
    private Button _deleteButton = new();

    public RuleEditor(int index)
    {
        Label name = new Label();
        name.Text = $"Rule #{index+1}";
        _deleteButton.Text = "Remove";
        _deleteButton.Pressed += QueueFree;
        _deleteButton.AnchorLeft = 1;
        _deleteButton.AnchorRight = 1;
        name.AddChild(_deleteButton);
        AddChild(name);
        AddThemeConstantOverride("separation", 5);
        
        var ruleGrid = new GridContainer();
        ruleGrid.Columns = 3;
        
        ruleGrid.AddChild(_tlColor);
        ruleGrid.AddChild(_topColor);
        ruleGrid.AddChild(_trColor);
        
        ruleGrid.AddChild(_leftColor);
        ruleGrid.AddChild(_centerColor);
        ruleGrid.AddChild(_rightColor);
        
        ruleGrid.AddChild(_blColor);
        ruleGrid.AddChild(_bottomColor);
        ruleGrid.AddChild(_brColor);
        
        var rule = new HBoxContainer();
        rule.AddChild(ruleGrid);
        var arrow = new TextureRect();
        arrow.Texture = ResourceLoader.Load<Texture2D>("res://assets/icons/arrow_r.svg");
        arrow.AnchorTop = 0.5f;
        arrow.AnchorBottom = 0.5f;
        arrow.SizeFlagsVertical = SizeFlags.ShrinkCenter;
        rule.AddChild(arrow);
        _newCenter.SizeFlagsVertical = SizeFlags.ShrinkCenter;
        rule.AddChild(_newCenter);
        AddChild(rule);
    }

    public RuleEditor(int oldCenter, Rule rule, int index)
    {
        _tlColor.Kind = rule.Neighborhood.TL;
        _tlColor.SetStyles();
        _topColor.Kind = rule.Neighborhood.Top;
        _topColor.SetStyles();
        _trColor.Kind = rule.Neighborhood.TR;
        _trColor.SetStyles();
        _leftColor.Kind = rule.Neighborhood.Left;
        _leftColor.SetStyles();
        _centerColor.Kind = oldCenter;
        _centerColor.SetStyles();
        _rightColor.Kind = rule.Neighborhood.Right;
        _rightColor.SetStyles();
        _blColor.Kind = rule.Neighborhood.BL;
        _blColor.SetStyles();
        _bottomColor.Kind = rule.Neighborhood.BR;
        _bottomColor.SetStyles();
        _brColor.Kind = rule.Neighborhood.BR;
        _brColor.SetStyles();
        _newCenter.Kind = rule.NewCenter;
        _newCenter.SetStyles();
        Label name = new Label();
        name.Text = $"Rule #{index+1}";
        _deleteButton.Text = "Remove";
        _deleteButton.Pressed += QueueFree;
        _deleteButton.AnchorLeft = 1;
        _deleteButton.AnchorRight = 1;
        name.AddChild(_deleteButton);
        AddChild(name);
        AddThemeConstantOverride("separation", 5);
        
        var ruleGrid = new GridContainer();
        ruleGrid.Columns = 3;
        
        ruleGrid.AddChild(_tlColor);
        ruleGrid.AddChild(_topColor);
        ruleGrid.AddChild(_trColor);
        
        ruleGrid.AddChild(_leftColor);
        ruleGrid.AddChild(_centerColor);
        ruleGrid.AddChild(_rightColor);
        
        ruleGrid.AddChild(_blColor);
        ruleGrid.AddChild(_bottomColor);
        ruleGrid.AddChild(_brColor);
        
        var contents = new HBoxContainer();
        contents.AddChild(ruleGrid);
        var arrow = new TextureRect();
        arrow.Texture = ResourceLoader.Load<Texture2D>("res://assets/icons/arrow_r.svg");
        arrow.AnchorTop = 0.5f;
        arrow.AnchorBottom = 0.5f;
        arrow.SizeFlagsVertical = SizeFlags.ShrinkCenter;
        contents.AddChild(arrow);
        _newCenter.SizeFlagsVertical = SizeFlags.ShrinkCenter;
        contents.AddChild(_newCenter);
        AddChild(contents);
    }

    public void ApplyRule()
    {
        var neighborhood = new Neighborhood(
            _tlColor.Kind,   _topColor.Kind,    _trColor.Kind,
            _leftColor.Kind,                    _rightColor.Kind,
            _blColor.Kind,   _bottomColor.Kind, _brColor.Kind
            );
        GridManager.Instance.Kinds[_centerColor.Kind].Rules.Add( new Rule(neighborhood, _newCenter.Kind));
    }
}
