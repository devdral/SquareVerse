using System.Collections.Generic;
using Godot;

namespace SquareVerse.Utility;

public class Kind
{
    public Color Color;
    public List<Rule> Rules { get; set; }

    public Kind(Color color)
    {
        Color = color;
        Rules = [];
    }

    public void AddRule(Rule rule)
    {
        Rules.Add(rule);
    }
}