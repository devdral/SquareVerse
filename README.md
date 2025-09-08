# SquareVerse

A tool for experimenting with cellular automata. Can be used to make cool images.

## How it works

SquareVerse uses a grid of "squares" (cells if you're familiar with CA). Each square
has behavior, in the form of rules. These rules tell it what to do. For instance, a rule
could tell a blue square that can see a red square above it to turn red. In this example,
a vertical line of blue squares will turn red when a single red square is placed above it.

Importantly, empty squares can also have such rules, and so a similar behavior could be programmed
where a line of red appears in the empty space.

## A tour of the UI

1. Modes: There are two modes in SquareVerse: Editing and Viewing. Viewing lets you control the camera. Editing lets you edit the board.
2. Palette: This is the color palette of the current project. Use the "+" button to add a new color.
3. Reset: Resets the board state to all empty.
4. Resize: Opens the resize dialog. Use it to change the board size. The max the application can handle is around 500 by 500.
5. Pause: Pauses/plays the simulation.
   + Shortcut: space.
6. Step: Steps the simulation one "tick" when paused. Only visible when paused.
    + Hold 'q' to advance the simulation.
7. Rules Editor: Opens the rules editor.
8. Save: Saves the current project file. Valid file extensions are `.res` and `.tres` (will take longer to load).
9. New: Opens up a new project. WARNING: This will overwrite any unsaved changes.
10. Save As: Like save but always lets you choose a new location and save parameters.
11. Open: Opens a project file.
12. Export: Saves the current board state as an image in a format of your choosing.

### Painting on the board

* LMB: paint currently selected color
* RMB: turn current square into empty

### The rules editor

The rules editor lets you modify the rules that the squares use to behave. Each rule is structured as a desired state leading
to a result. A grid of three by three shows what you will see when this rule applies. To the right of the arrow is what
the center cell will transform into. 

Paint these cells using the same controls as the board, plus one. Rules may use a "wildcard" as one of the squares around the
center square. Paint this special case using MMB (middle-click).

## Installation

Download the latest .zip from the "Releases" section. Extract ALL the contents and run
`SquareVerse.exe`.

## Contributing

This project is not currently accepting contributions.

## License

This project is licensed under the MIT license.