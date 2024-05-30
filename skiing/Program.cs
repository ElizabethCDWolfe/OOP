using System;

interface IGreeting
{
    public void SayHello();
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

abstract class APerson
{
    public string name { get; }
    public IGreeting greeting { get; set; }

    protected APerson(string name)
    {
        this.name = name;
        greeting = new English();
    }

    protected APerson(string name, IGreeting greeting)
    {
        this.name = name;
        this.greeting = greeting;
    }

    public string ReturnName()
    {
        return name;
    }
}

class Skier : APerson
{
    public Pocket skierPocket { get; }

    public Skier(string name)
        : base(name, new English())
    {
        skierPocket = new Pocket();
    }

    public Skier(string name, IGreeting greeting)
        : base(name, greeting)
    {
        skierPocket = new Pocket();
    }

    public void SayHello()
    {
        greeting.SayHello();
    }
}

class TicketMaster : APerson
{
    public Inventory ticketMasterInventory { get; }

    public TicketMaster(string name)
        : base(name, new English())
    {
        ticketMasterInventory = new Inventory(10, 10, 0);
    }

    public TicketMaster(string name, IGreeting greeting)
        : base(name, greeting)
    {
        ticketMasterInventory = new Inventory(10, 10, 10);
    }

    public void SellTicket(Skier skier, string site)
    {
        Ticket ticket = ticketMasterInventory.RemoveTicket(site);
        if (ticket != null)
        {
            ticket = StampTicket(ticket, skier.ReturnName());
            skier.skierPocket.AddTicket(ticket);
        }
        else
        {
            Console.WriteLine($"No ticket found for site: {site}");
        }
    }

    private Ticket StampTicket(Ticket ticket, string name)
    {
        ticket.SetPersonValidFor(name);
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
        Ticket ticketToRemove = TicketList.FirstOrDefault(ticket => ticket.GetSite() == site);

        if (ticketToRemove != null)
        {
            TicketList.Remove(ticketToRemove);
        }

        return ticketToRemove;
    }
}

class Ticket
{
    public string siteValidFor { get; }
    public int serialNumber { get; }
    public string personValidFor { get; private set; }

    public Ticket(string site)
    {
        siteValidFor = site;
        serialNumber = GenerateSerialNumber();
        personValidFor = "";
    }

    public void SetPersonValidFor(string name)
    {
        this.personValidFor = name;
    }

    public string GetSite()
    {
        return siteValidFor;
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
        Skier mySkier = new Skier("Tom", new Spanish());
        mySkier.SayHello();

        TicketMaster myTicketMaster = new TicketMaster("Jane");
        myTicketMaster.SellTicket(mySkier, "Keystone");
    }
}
