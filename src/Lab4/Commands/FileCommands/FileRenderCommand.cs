using Itmo.ObjectOrientedProgramming.Lab4.Commands.CurrentPaths;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.FileCommands;

public class FileRenderCommand : ICommand
{
    private readonly string _pathForFile;
    private readonly ICommandStrategy _strategy;

    public FileRenderCommand(string pathForFile, ICommandStrategy strategy)
    {
        _pathForFile = pathForFile ?? throw new ArgumentNullException(nameof(pathForFile));
        _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }

    public void Execute(ICurrentPath path)
    {
        _strategy.FileRenderCommand(_pathForFile);
    }
}