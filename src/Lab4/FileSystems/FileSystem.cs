using Itmo.ObjectOrientedProgramming.Lab4.Commands.CurrentPaths;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.Renderables;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems.FileSystemStateHandler;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems.Results;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems.States;
using Itmo.ObjectOrientedProgramming.Lab4.Parsers;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

public class FileSystem : IFileSystem
{
    private readonly IRenderable _renderable;
    private readonly IParser _parser;
    private readonly CurrentPath _currentPath;
    private IFileSystemStateHandler _stateHandler;

    public FileSystemState State => _stateHandler.State;

    public FileSystem(IRenderable renderable, IParser parser, CurrentPath currentPath, IFileSystemStateHandler stateHandler)
    {
        _renderable = renderable;
        _parser = parser;
        _currentPath = currentPath;
        _stateHandler = stateHandler;
    }

    public ConnectionResult Connect()
    {
        ConnectionStateMoveResult result = _stateHandler.MoveToActive();

        if (result is not ConnectionStateMoveResult.Success success)
        {
            return new ConnectionResult.InvalidState(_stateHandler.State);
        }

        _stateHandler = success.Next;
        return new ConnectionResult.Success();
    }

    public ConnectionResult Disconnect()
    {
        ConnectionStateMoveResult result = _stateHandler.MoveToInactive();

        if (result is not ConnectionStateMoveResult.Success success)
        {
            return new ConnectionResult.InvalidState(_stateHandler.State);
        }

        _stateHandler = success.Next;
        return new ConnectionResult.Success();
    }

    public void Run()
    {
        while (State is FileSystemState.Connected)
        {
            _renderable.Render("Enter what you have to say (or 'exit' to quit):");
            string command = Console.ReadLine() ?? string.Empty;
            if (command == "exit")
            {
                Disconnect();
                break;
            }

            _parser.ParseAndRun(command);

            _renderable.Render($"Current Path: {_currentPath.Path}");
        }
    }
}