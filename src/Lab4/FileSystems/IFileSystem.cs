using Itmo.ObjectOrientedProgramming.Lab4.FileSystems.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

public interface IFileSystem
{
    ConnectionResult Connect();

    ConnectionResult Disconnect();

    void Run();
}