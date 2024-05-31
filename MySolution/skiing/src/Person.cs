using EnglishNS;

namespace Skiing;

abstract class Person
{
    public string Name { get; }
    public IGreeting Greeting { get; }

    protected Person(string name)
    {
        Name = name;
        Greeting = new English();
    }

    protected Person(string name, IGreeting greeting)
    {
        Name = name;
        Greeting = greeting;
    }
}
