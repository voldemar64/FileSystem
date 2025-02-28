using Itmo.ObjectOrientedProgramming.Lab4.Commands.CurrentPaths;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.FileCommands;

public class FileCopyCommand : ICommand
{
    private readonly string _pathForFile;
    private readonly string _pathForDirectory;
    private readonly ICommandStrategy _strategy;

    public FileCopyCommand(string pathForFile, string pathForDirectory, ICommandStrategy strategy)
    {
        _pathForFile = pathForFile ?? throw new ArgumentNullException(nameof(pathForFile));
        _pathForDirectory = pathForDirectory ?? throw new ArgumentNullException(nameof(pathForDirectory));
        _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }

    public void Execute(ICurrentPath path)
    {
        _strategy.FileCopyCommand(_pathForFile, _pathForDirectory);
    }
}