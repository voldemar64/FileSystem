using Itmo.ObjectOrientedProgramming.Lab4.Commands.CurrentPaths;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.TreeCommands;

public class TreeGotoCommand : ICommand
{
    private readonly string? _fullPath;
    private readonly ICommandStrategy _strategy;

    public TreeGotoCommand(string fullPath, ICommandStrategy strategy)
    {
        _fullPath = fullPath;
        _strategy = strategy;
    }

    public void Execute(ICurrentPath path)
    {
        path.Path = _strategy.TreeGotoCommand(_fullPath);
    }
}