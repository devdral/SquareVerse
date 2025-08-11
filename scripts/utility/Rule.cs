namespace SquareVerse.Utility;

public record struct Neighborhood(int TL, int Top, int TR, int Left, int Right, int BL, int Bottom, int BR);

public struct Rule
{
    public Neighborhood Neighborhood;
    
    public int NewCenter;

    public Rule(Neighborhood neighborhood, int newCenter)
    {
        Neighborhood = neighborhood;
        NewCenter = newCenter;
    }
}