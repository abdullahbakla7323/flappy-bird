# Flappy Bird Console

A small Flappy Bird clone in C# that runs in the terminal. The bird stays in one column; pipes scroll from the right. Press space to jump and try to get through the gaps without hitting the ground or the pipes.

## Requirements

- .NET SDK 6 or newer

## Run

From the project folder:

```bash
cd FlappyBirdConsole
dotnet run
```

You can also double-click a shell script on your desktop if you set that up earlier.

## Controls

- **Space** — jump  
- **ESC** — quit

Score goes up each time you pass a pipe. Hit a pipe or the ground and the game ends; your final score is shown.
