using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab4.Parsers.CommandHandlers;

public class CommandRequest
{
    private readonly List<string> _arguments = new List<string>();

    public string CommandName { get; }

    public ReadOnlyCollection<string> Arguments { get; }

    public ICommandStrategy? Strategy { get; private set; } = new CommandStrategy();

    public string? Mode { get; private set; }

    public int? Depth { get; private set; }

    public CommandRequest(string commandLine)
    {
        string[] parts = commandLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 0)
        {
            throw new ArgumentException("Empty command line.");
        }

        int commandNameEndIndex = 1;
        while (commandNameEndIndex < parts.Length &&
               !parts[commandNameEndIndex].StartsWith('-') &&
               !parts[commandNameEndIndex].StartsWith('/'))
        {
            commandNameEndIndex++;
        }

        CommandName = string.Join(" ", parts.Take(commandNameEndIndex - 1));
        _arguments.AddRange(parts.Skip(commandNameEndIndex - 1));
        Arguments = new ReadOnlyCollection<string>(_arguments);

        ParseArguments();
    }

    public CommandRequest WithStrategy(ICommandStrategy? strategy)
    {
        Strategy = strategy;
        return this;
    }

    public CommandRequest WithMode(string? mode)
    {
        Mode = mode;
        return this;
    }

    private void ParseArguments()
    {
        for (int i = 0; i < _arguments.Count; i++)
        {
            if (_arguments[i].StartsWith("-m"))
            {
                if (i + 1 < _arguments.Count)
                {
                    Mode = _arguments[i + 1];
                    _arguments.RemoveRange(i, 2);
                    i--;
                }
                else
                {
                    throw new ArgumentException("Invalid mode argument.");
                }
            }
            else if (_arguments[i].StartsWith("-d"))
            {
                if (i + 1 < _arguments.Count && int.TryParse(_arguments[i + 1], out int depth))
                {
                    Depth = depth;
                    _arguments.RemoveRange(i, 2);
                    i--;
                }
                else
                {
                    throw new ArgumentException("Invalid depth argument.");
                }
            }
        }
    }
}