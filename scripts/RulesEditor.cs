using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;
using SquareVerse.Utility;

namespace SquareVerse;

public partial class RulesEditor : Window
{
    private Label _noRules;
    private VBoxContainer _editors;
    private Array<Node> _children;

    public override void _Ready()
    {
        _noRules = GetNode<Label>("FormItems/NoRules");
        _editors = GetNode<VBoxContainer>("FormItems/ScrollContainer/Items");
    }

    public void OnCreateNew()
    {
        _noRules.Hide();
        var editor = new RuleEditor(_editors.GetChildren().Count);
        _editors.AddChild(editor);
    }
    
    private void ApplyRules()
    {
        // Delete preexisting rules.
        var kinds = GridManager.Instance.Kinds;
        for (int i = 0; i < kinds.Count; i++)
        {
            kinds[i].Rules = [];
        }
        
        foreach (RuleEditor rule in _editors.GetChildren())
        {
            rule.ApplyRule();            
        }
    }

    public void OnApplyClicked()
    {
        var oldGrid = GridManager.Instance.Grid;
        GridManager.Instance.Grid = new Grid(oldGrid.Width, oldGrid.Height);
        ApplyRules();
    }

    public void OnDoneClicked()
    {
        var oldGrid = GridManager.Instance.Grid;
        GridManager.Instance.Grid = new Grid(oldGrid.Width, oldGrid.Height);
        ApplyRules();
        Hide();
    }
    
    public void OnClearAllClicked()
    {
        var kinds = GridManager.Instance.Kinds;
        for (int i = 0; i < kinds.Count; i++)
        {
            kinds[i].Rules = [];
        }

        foreach (var editor in _editors.GetChildren())
        {
            editor.QueueFree();
        }
    }

    public void OnOpenRulesEditor()
    {
        Show();
    }

    public void OnCloseRulesEditor()
    {
        Hide();
    }
}
