using System.Collections.Generic;
using Godot;

namespace SquareVerse.Utility;

public class Kind
{
    public Color Color;
    public List<Rule> Rules { get; set; }
    public int[] ConversionCandidates { get; set; }

    public Kind(Color color)
    {
        Color = color;
        Rules = [];
        ConversionCandidates = new int[1];
    }

    public void AddRule(Rule rule)
    {
        Rules.Add(rule);
        ConversionCandidates = new int[Rules.Count+1];
    }
}