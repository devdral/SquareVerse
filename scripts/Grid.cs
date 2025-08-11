namespace SquareVerse.scripts;

public class Grid
{
    private Cell[] _grid;
    
    public int Width { get; }
    public int Height { get; }

    public Grid(int width, int height)
    {
        _grid = new Cell[width * height];
        Width = width;
        Height = height;
    }

    public Cell this[int x, int y]
    {
        get => _grid[x + (y * Height)];
        set => _grid[x + (y * Height)] = value;
    }
}