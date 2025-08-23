using Godot;

namespace SquareVerse.Utility;

public struct Cell
{
    public int Type;

    public Cell(int type)
    {
        Type = type;
    }
}

public struct QualifiedCell
{
    public Cell Cell;
    public Vector2I Coordinate;

    public QualifiedCell(Cell cell, Vector2I coordinate)
    {
        Cell = cell;
        Coordinate = coordinate;
    }
}