namespace Itmo.ObjectOrientedProgramming.Lab4.Parsers.CommandHandlers;

public interface ICommandHandler<TRequest> where TRequest : CommandRequest
{
    ICommandHandler<TRequest> AddNext(ICommandHandler<TRequest> handler);

    TRequest? Handle(CommandRequest request);
}