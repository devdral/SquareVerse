using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace SquareVerse.Utility;

public partial class ProjectFile : Resource
{
    
    [Export] public Array<RuleResource> Rules { get; set; }
    [Export] public Color[] Kinds { get; set; }
    [Export] public int[] Grid { get; set; }

    public ProjectFile()
    {
        Rules = [];
        Kinds = [];
        Grid = [];
    }

    public void StorePalette()
    {
        Kinds = new Color[GridManager.Instance.Kinds.Count];
        for (int i = 0; i < GridManager.Instance.Kinds.Count; i++)
        {
            var kind = GridManager.Instance.Kinds[i];
            Kinds[i] = kind.Color;
        }
    }

    public void StoreGrid()
    {
        Grid = GridManager.Instance.Grid.GetPackedGrid();
    }

    public void StoreRules()
    {
        for (int i = 0; i < GridManager.Instance.Kinds.Count; i++)
        {
            var kind = GridManager.Instance.Kinds[i];
            foreach (var rule in kind.Rules)
            {
                var ruleResource = new RuleResource(
                    rule.Neighborhood, i, rule.NewCenter
                );
                Rules.Add(ruleResource);
            }
        }
    }
}