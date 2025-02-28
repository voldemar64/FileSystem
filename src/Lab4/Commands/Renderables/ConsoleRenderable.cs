namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.Renderables;

public class ConsoleRenderable : IRenderable
{
    public void Render(string renderable)
    {
        Console.WriteLine(renderable);
    }
}