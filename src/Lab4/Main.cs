using Itmo.ObjectOrientedProgramming.Lab4.Commands.CurrentPaths;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.Renderables;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems.FileSystemStateHandler;
using Itmo.ObjectOrientedProgramming.Lab4.Parsers;
using Itmo.ObjectOrientedProgramming.Lab4.Parsers.CommandHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.Parsers.CommandHandlers.ConnectionHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.Parsers.CommandHandlers.FileOperationHandlers;

var currentPath = new CurrentPath();
var consoleRender = new ConsoleRenderable();
var connectionModeHandler = new ConnectModeHandler();

connectionModeHandler
    .AddNext(new ConnectHandler(currentPath, consoleRender))
    .AddNext(new FileRenderHandler(currentPath, consoleRender))
    .AddNext(new NullCommandHandler());

var parser = new Parser(connectionModeHandler, consoleRender);
var stateHandler = new DisconnectedFileSystemStateHandler();

var fileSystem = new FileSystem(consoleRender, parser, currentPath, stateHandler);

fileSystem.Connect();
fileSystem.Run();