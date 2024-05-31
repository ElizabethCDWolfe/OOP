using System;
using System.Collections.Generic;
using System.Linq;

interface IGreeting
{
    void SayHello();
}

class English : IGreeting
{
    public void SayHello()
    {
        Console.WriteLine("Hello");
    }
}

class Spanish : IGreeting
{
    public void SayHello()
    {
        Console.WriteLine("Hola");
    }
}

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

class Skier : Person
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

class TicketMaster : Skier
{
    private Inventory TicketMasterInventory { get; }

    public TicketMaster(string name)
        : base(name, new English())
    {
        TicketMasterInventory = new Inventory(10, 10, 10);
    }

    public TicketMaster(string name, IGreeting greeting)
        : base(name, greeting)
    {
        TicketMasterInventory = new Inventory(10, 10, 10);
    }

    public void SellTicket(Skier skier, string site)
    {
        Ticket ticket = TicketMasterInventory.RemoveTicket(site);
        ticket = StampTicket(ticket, skier.Name);
        skier.SkierPocket.AddTicket(ticket);
    }

    private Ticket StampTicket(Ticket ticket, string name)
    {
        ticket.PersonValidFor = name;
        return ticket;
    }
}

abstract class ATicketContainer
{
    protected List<Ticket> TicketList;

    protected ATicketContainer()
    {
        TicketList = new List<Ticket>();
    }
}

class Pocket : ATicketContainer
{
    public void AddTicket(Ticket ticket)
    {
        TicketList.Add(ticket);
    }
}

class Inventory : ATicketContainer
{
    public Inventory(int numberOfAspen, int numberOfVail, int numberOfKeystone)
    {
        TicketList = new List<Ticket>();

        FillList(numberOfAspen, "Aspen");
        FillList(numberOfVail, "Vail");
        FillList(numberOfKeystone, "Keystone");
    }

    private void FillList(int number, string site)
    {
        for (int i = 0; i < number; i++)
        {
            Ticket ticket = new Ticket(site);
            TicketList.Add(ticket);
        }
    }

    public Ticket RemoveTicket(string site)
    {
        // Using LINQ library FirstOrDefault to get ticket, then using a lambda expression
        // to match the ticket site to the site we are searching for
        Ticket? ticketToRemove = TicketList.FirstOrDefault(ticket => ticket.SiteValidFor == site);

        if (ticketToRemove != null)
        {
            TicketList.Remove(ticketToRemove);
        }
        else
        {
            throw new NullReferenceException($"No ticket found for site: {site}");
        }

        return ticketToRemove;
    }
}

class Ticket
{
    private string _personValidFor;
    public string SiteValidFor { get; }
    public int SerialNumber { get; }
    public string PersonValidFor
    {
        set
        {
            if (_personValidFor != "")
            {
                throw new Exception("Person valid for is already set ");
            }
            _personValidFor = value;
        }
        get { return _personValidFor; }
    }

    public Ticket(string site)
    {
        SiteValidFor = site;
        SerialNumber = GenerateSerialNumber();
        _personValidFor = "";
    }

    private int GenerateSerialNumber()
    {
        Random dice = new Random();
        return dice.Next(0, 1000);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Skier mySkier = new Skier("Tom");
        mySkier.Greeting.SayHello();

        TicketMaster myTicketMaster = new TicketMaster("Jane");
        myTicketMaster.SellTicket(myTicketMaster, "Keystone");
    }
}
