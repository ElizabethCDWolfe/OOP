namespace Skiing;

public class Skier : Person
{
    public Pocket SkierPocket { get; }

    public Skier(string name)
        : base(name, new English())
    {
        SkierPocket = new Pocket();
    }

    public Skier(string name, IGreeting greeting)
        : base(name, greeting)
    {
        SkierPocket = new Pocket();
    }
}
