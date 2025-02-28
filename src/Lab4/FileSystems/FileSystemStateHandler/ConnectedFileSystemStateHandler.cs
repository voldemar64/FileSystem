using Itmo.ObjectOrientedProgramming.Lab4.FileSystems.Results;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems.States;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystems.FileSystemStateHandler;

public class ConnectedFileSystemStateHandler : IFileSystemStateHandler
{
    public FileSystemState State => FileSystemState.Connected;

    public ConnectionStateMoveResult MoveToActive()
    {
        return new ConnectionStateMoveResult.Success(this);
    }

    public ConnectionStateMoveResult MoveToInactive()
    {
        return new ConnectionStateMoveResult.Success(new DisconnectedFileSystemStateHandler());
    }
}