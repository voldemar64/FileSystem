using Itmo.ObjectOrientedProgramming.Lab4.Commands.Renderables;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public interface ICommandStrategy
{
    string? ConnectCommand(string? fullPath);

    string? DisconnectCommand();

    void FileRenderCommand(string pathForFile);

    void FileCopyCommand(string pathForFile, string pathForDirectory);

    void FileDeleteCommand(string pathForFile);

    void FileMoveCommand(string pathForFile, string pathForDirectory);

    void FileRenameCommand(string pathForFile, string newName);

    string? TreeGotoCommand(string? fullPath);

    void TreeListCommand(int depth, IRenderable renderable);
}