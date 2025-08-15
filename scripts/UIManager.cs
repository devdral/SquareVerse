using Godot;
using System;

public partial class UIManager : Node
{
    public const bool EDIT = true;
    public const bool VIEW = false;
    
    public static UIManager Instance { get; private set; }
    
    public bool Mode { get; set; }

    public int SelectedKind { get; set; } = 0;

    public bool Paused { get; set; } = false;

    public override void _Ready()
    {
        Instance = this;
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("pause"))
        {
            Paused = !Paused;
        } else if (Input.IsActionJustReleased("switch_mode"))
        {
            Mode = !Mode;
        }
    }
}
