using Itmo.ObjectOrientedProgramming.Lab4.FileSystems.FileSystemStateHandler;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystems.Results;

public record ConnectionStateMoveResult
{
    private ConnectionStateMoveResult() { }

    public sealed record Success(IFileSystemStateHandler Next) : ConnectionStateMoveResult;

    public sealed record InvalidMove : ConnectionStateMoveResult;
}