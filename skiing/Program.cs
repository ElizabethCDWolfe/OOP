using System;

interface IGreeting 
{
    public void SayHello();
}

class English : IGreeting
{
    void SayHello()
    {
        Console.WriteLine("Hello!");
    }
}

class Spanish : IGreeting
{
    void SayHello()
    {
        Console.WriteLine("Hola");
    }
}

abstract class APerson 
{
    protected string _Name;
    protected IGreeting greeting;
    
    protected APerson()
    {
        greeting = new English(); 
    }

    protected APerson(IGreeting greeting)
    {
        Greetings = greeting;
    }
}

class Skier : APerson 
{
    Pocket skierPocket;

    public Skier() : base(new English()) // Default greeting for skier is English
    {
        skierPocket = new Pocket();
    }

    public Skier(IGreeting greeting) : base(greeting)
    {
        skierPocket = new Pocket();
    }
}

class TicketMaster : APerson
{   
    Inventory ticketMasterInventory;

    public TicketMaster() : base(new English()) // Default greeting for ticket master is English
    {
        ticketMasterInventory = new Inventory();
    }

    public TicketMaster(IGreeting greeting) : base(greeting)
    {
        ticketMasterInventory = new Inventory();
    }

    public void SellTickets(Skier skier, string site)
    {
        Ticket ticket = TakeTicketFromInventory(site);
        ticket = StampTicket(ticket, skier._Name);
        skier.skierPocket.AddTicket(ticket);
    }

    private Ticket TakeTicketFromInventory(string site)
    {
        // logic to check Inventory for site and then take ticket, do error checking
        if (ticketMasterInventory.HasSiteTicket(site)) 
        {
            return ticketMasterInventory.RemoveTicket(site);
        }
        else 
        {
            return null;
        }
    }

    private HasSiteTicket(string site)
    {
        // 
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
    // Constructor to initialize Inventory with elements
    Inventory(int numberOfAspen, int numberOfVail, int numberOfKeystone)
    {
        // add int number of tickets to inventory 
        TicketList.Add(new List<Ticket> {Ticket})
    }

    public Ticket RemoveTicket(Ticket ticket)
    {
        TicketList.Remove(ticket);
        return ticket;
    }
}

class Ticket 
{
    readonly string siteValidFor;
    readonly int serialNumber;
    private string personValidFor;

    public Ticket(string site)
    {
        this.siteValidFor = site;
        this.serialNumber = GenerateSerialNumber();
        this.personValidFor = "";
    }

    public void SetPersonValidFor(string name) 
    {
        this.personValidFor = name;
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
        Console.WriteLine("hi");
    }
}
