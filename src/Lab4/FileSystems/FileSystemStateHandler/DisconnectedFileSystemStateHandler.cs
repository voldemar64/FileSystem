using Itmo.ObjectOrientedProgramming.Lab4.FileSystems.Results;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems.States;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystems.FileSystemStateHandler;

public class DisconnectedFileSystemStateHandler : IFileSystemStateHandler
{
    public FileSystemState State => FileSystemState.Disconnected;

    public ConnectionStateMoveResult MoveToActive()
    {
        return new ConnectionStateMoveResult.Success(new ConnectedFileSystemStateHandler());
    }

    public ConnectionStateMoveResult MoveToInactive()
    {
        return new ConnectionStateMoveResult.Success(this);
    }
}