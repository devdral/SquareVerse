namespace SquareVerse.Utility;

public record SaveConfig
{
    public string FilePath { get; set; }
    public bool IncludeRules { get; set; } 
    public bool IncludeBoard { get; set; }
}