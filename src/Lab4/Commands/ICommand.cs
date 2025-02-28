using Itmo.ObjectOrientedProgramming.Lab4.Commands.CurrentPaths;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public interface ICommand
{
    void Execute(ICurrentPath path);
}