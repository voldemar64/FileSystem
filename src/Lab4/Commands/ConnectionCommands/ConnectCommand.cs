using Itmo.ObjectOrientedProgramming.Lab4.Commands.CurrentPaths;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.ConnectionCommands;

public class ConnectCommand : ICommand
{
    private readonly ICommandStrategy _strategy;
    private readonly string? _path;

    public ConnectCommand(string path, ICommandStrategy strategy)
    {
        _path = path ?? throw new ArgumentNullException(nameof(path));
        _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }

    public void Execute(ICurrentPath path)
    {
        path.Path = _strategy.ConnectCommand(_path);
    }
}