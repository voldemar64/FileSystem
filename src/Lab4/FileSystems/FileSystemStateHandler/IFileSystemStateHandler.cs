using Itmo.ObjectOrientedProgramming.Lab4.FileSystems.Results;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems.States;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystems.FileSystemStateHandler;

public interface IFileSystemStateHandler
{
    FileSystemState State { get; }

    ConnectionStateMoveResult MoveToActive();

    ConnectionStateMoveResult MoveToInactive();
}