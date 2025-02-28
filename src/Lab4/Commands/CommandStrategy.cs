using Itmo.ObjectOrientedProgramming.Lab4.Commands.Renderables;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class CommandStrategy : ICommandStrategy
{
    private string? _currentPath;

    public string? ConnectCommand(string? fullPath)
    {
        if (fullPath == null || !Directory.Exists(fullPath))
        {
            throw new ArgumentException($"Invalid path: {fullPath}");
        }

        _currentPath = fullPath;
        return _currentPath;
    }

    public string? DisconnectCommand()
    {
        _currentPath = null;
        return _currentPath;
    }

    public void FileRenderCommand(string pathForFile)
    {
        string fullFilePath = GetFullPath(pathForFile);

        if (!File.Exists(fullFilePath))
        {
            throw new FileNotFoundException($"File not found: {fullFilePath}");
        }

        try
        {
            string content = File.ReadAllText(fullFilePath);
            Console.WriteLine(content);
        }
        catch (Exception exception)
        {
            throw new IOException($"Error reading file: {fullFilePath}", exception);
        }
    }

    public void FileCopyCommand(string pathForFile, string pathForDirectory)
    {
        string sourceFilePath = GetFullPath(pathForFile);
        string destinationDirectory = GetFullPath(pathForDirectory);

        if (!File.Exists(sourceFilePath))
        {
            throw new FileNotFoundException($"Source file not found: {sourceFilePath}");
        }

        if (!Directory.Exists(destinationDirectory))
        {
            throw new DirectoryNotFoundException($"Destination directory not found: {destinationDirectory}");
        }

        try
        {
            string uniqueDestinationPath = GenerateUniqueFileName(destinationDirectory, Path.GetFileName(pathForFile));
            File.Copy(sourceFilePath, uniqueDestinationPath);
        }
        catch (Exception exception)
        {
            throw new IOException($"Error copying file: {sourceFilePath}", exception);
        }
    }

    public void FileDeleteCommand(string pathForFile)
    {
        string fullPath = GetFullPath(pathForFile);

        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException($"File not found: {fullPath}");
        }

        try
        {
            File.Delete(fullPath);
        }
        catch (Exception exception)
        {
            throw new IOException($"Error deleting file: {fullPath}", exception);
        }
    }

    public void FileMoveCommand(string pathForFile, string pathForDirectory)
    {
        string sourceFilePath = GetFullPath(pathForFile);
        string destinationDirectory = GetFullPath(pathForDirectory);

        if (!File.Exists(sourceFilePath))
        {
            throw new FileNotFoundException($"Source file not found: {sourceFilePath}");
        }

        if (!Directory.Exists(destinationDirectory))
        {
            throw new DirectoryNotFoundException($"Destination directory not found: {destinationDirectory}");
        }

        try
        {
            string destinationFilePath = Path.Combine(destinationDirectory, Path.GetFileName(pathForFile));
            File.Move(sourceFilePath, destinationFilePath, true);
        }
        catch (Exception exception)
        {
            throw new IOException($"Error moving file: {sourceFilePath}", exception);
        }
    }

    public void FileRenameCommand(string pathForFile, string newName)
    {
        string fullPath = GetFullPath(pathForFile);

        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException($"File not found: {fullPath}");
        }

        try
        {
            string directory = Path.GetDirectoryName(fullPath) ?? string.Empty;
            string extension = Path.GetExtension(fullPath);
            string newFileNameWithExtension = newName + extension;
            string newFilePath = Path.Combine(directory, newFileNameWithExtension);
            File.Move(fullPath, newFilePath, true);
        }
        catch (Exception exception)
        {
            throw new IOException($"Error renaming file: {fullPath}", exception);
        }
    }

    public string? TreeGotoCommand(string? fullPath)
    {
        if (fullPath == null)
        {
            return _currentPath;
        }

        if (Path.IsPathRooted(fullPath))
        {
            if (!Directory.Exists(fullPath))
            {
                throw new ArgumentException($"Invalid path: {fullPath}");
            }

            _currentPath = fullPath;
        }
        else if (_currentPath != null)
        {
            string combinedPath = Path.Combine(_currentPath, fullPath);
            if (!Directory.Exists(combinedPath))
            {
                throw new ArgumentException($"Invalid path: {combinedPath}");
            }

            _currentPath = combinedPath;
        }
        else
        {
            throw new InvalidOperationException("Cannot use relative path without a current directory.");
        }

        return _currentPath;
    }

    public void TreeListCommand(int depth, IRenderable renderable)
    {
        if (string.IsNullOrEmpty(_currentPath))
        {
            throw new InvalidOperationException("Not connected to a file system.");
        }

        if (!Directory.Exists(_currentPath))
        {
            throw new DirectoryNotFoundException($"Directory not found: {_currentPath}");
        }

        RenderTreeList(_currentPath, depth, renderable, " ");
    }

    private void RenderTreeList(string path, int depth, IRenderable renderable, string indent)
    {
        if (depth < 0) return;

        RenderFiles(path, renderable, indent);
        RenderDirectories(path, depth, renderable, indent);
    }

    private void RenderFiles(string path, IRenderable renderable, string indent)
    {
        string[] files;
        try
        {
            files = Directory.GetFiles(path);
        }
        catch (UnauthorizedAccessException exception)
        {
            throw new UnauthorizedAccessException($"Access denied to directory: {path}", exception);
        }

        foreach (string file in files)
        {
            renderable.Render($"{indent}- {Path.GetFileName(file)}");
        }
    }

    private void RenderDirectories(string path, int depth, IRenderable renderable, string indent)
    {
        string[] subDirs;
        try
        {
            subDirs = Directory.GetDirectories(path);
        }
        catch (UnauthorizedAccessException exception)
        {
            throw new UnauthorizedAccessException($"Access denied to directory: {path}", exception);
        }

        foreach (string dir in subDirs)
        {
            renderable.Render($"{indent}+ {Path.GetFileName(dir)}");
            RenderTreeList(dir, depth - 1, renderable, indent + "   ");
        }
    }

    private string GenerateUniqueFileName(string directory, string fileNameWithExtension)
    {
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileNameWithExtension);
        string extension = Path.GetExtension(fileNameWithExtension);

        string newFileName = fileNameWithExtension;
        int counter = 1;

        while (File.Exists(Path.Combine(directory, newFileName)))
        {
            newFileName = $"{fileNameWithoutExtension}({counter++}){extension}";
        }

        return Path.Combine(directory, newFileName);
    }

    private string GetFullPath(string relativeOrAbsolutePath)
    {
        if (string.IsNullOrEmpty(_currentPath))
        {
            return relativeOrAbsolutePath;
        }

        return Path.IsPathRooted(relativeOrAbsolutePath)
            ? relativeOrAbsolutePath
            : Path.GetFullPath(Path.Combine(_currentPath, relativeOrAbsolutePath));
    }
}