using System;

interface IGreeting 
{
    public void SayHello();
}

class English : IGreeting
{
    public void SayHello()
    {
        Console.WriteLine("Hello!");
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
    protected string _Name;
    protected IGreeting greeting;
    
    protected APerson(string name)
    {
        _Name = name;
        greeting = new English(); 
    }

    protected APerson(string name, IGreeting greeting)
    {
        _Name = name;
        this.greeting = greeting;
    }
}

class Skier : APerson 
{
    public Pocket skierPocket;

    public Skier(string name) : base(name, new English()) 
    {   
        skierPocket = new Pocket();
    }

    public Skier(string name, IGreeting greeting) : base(name, greeting)
    {
        skierPocket = new Pocket();
    }

    public string ReturnName()
    {
        return this._Name;
    }
}

class TicketMaster : APerson
{   
    Inventory ticketMasterInventory;

    public TicketMaster(string name) : base(name, new English()) // Default greeting for ticket master is English
    {
        ticketMasterInventory = new Inventory();
    }

    public TicketMaster(string name, IGreeting greeting) : base(name, greeting)
    {
        ticketMasterInventory = new Inventory();
    }

    public void SellTickets(Skier skier, string site)
    {
        Ticket ticket = TakeTicketFromInventory(site);
        ticket = StampTicket(ticket, skier.ReturnName());
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
