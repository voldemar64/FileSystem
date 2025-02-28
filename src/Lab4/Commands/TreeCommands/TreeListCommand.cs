using Itmo.ObjectOrientedProgramming.Lab4.Commands.CurrentPaths;

using Itmo.ObjectOrientedProgramming.Lab4.Commands.Renderables;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.TreeCommands;

public class TreeListCommand : ICommand
{
    private readonly ICommandStrategy _strategy;
    private readonly IRenderable _printer;
    private int _depth = 1;

    public TreeListCommand(ICommandStrategy strategy, IRenderable printer)
    {
        _strategy = strategy;
        _printer = printer;
    }

    public void SetDepth(int depth)
    {
        _depth = depth;
    }

    public void Execute(ICurrentPath path)
    {
        _strategy.TreeListCommand(_depth, _printer);
    }
}