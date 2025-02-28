namespace Itmo.ObjectOrientedProgramming.Lab4.Parsers.CommandHandlers;

public abstract class CommandHandlerBase<TRequest> : ICommandHandler<TRequest> where TRequest : CommandRequest
{
    protected ICommandHandler<TRequest>? Next { get; private set; }

    public ICommandHandler<TRequest> AddNext(ICommandHandler<TRequest> handler)
    {
        if (Next is null)
        {
            Next = handler;
        }
        else
        {
            Next.AddNext(handler);
        }

        return this;
    }

    public abstract TRequest? Handle(CommandRequest request);
}