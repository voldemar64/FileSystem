using Itmo.ObjectOrientedProgramming.Lab4.FileSystems.States;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystems.Results;

public abstract record ConnectionResult
{
    private ConnectionResult() { }

    public sealed record Success : ConnectionResult;

    public sealed record InvalidState(FileSystemState CurrentState) : ConnectionResult;
}