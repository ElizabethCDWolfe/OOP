using System;
using System.Collections.Generic;
using System.Linq;

// Define smaller interfaces based on specific functionalities
public interface IGreetable
{
    void SayHello();
}

public interface IRole
{
    void SayRole();
}

public interface IPurchaser
{
    List<Ticket> BuyTickets(List<Ticket> ticketsRequested, string site);
}

// Define specific implementations of the interfaces
public class EnglishGreeting : IGreetable
{
    public void SayHello()
    {
        Console.WriteLine("Hello!");
    }
}

public class Person : IGreetable, IRole, IPurchaser
{
    private readonly IGreetable greeter;
    private readonly IRole rolePlayer;
    private readonly ITicketContainer ticketContainer;

    public Person(string name, List<Ticket> ticketsOwned, IGreetable greeter, IRole rolePlayer, ITicketContainer ticketContainer)
    {
        Name = name;
        this.greeter = greeter;
        this.rolePlayer = rolePlayer;
        this.ticketContainer = ticketContainer;

        foreach (Ticket ticketOwned in ticketsOwned)
        {
            ticketContainer.AddTicket(ticketOwned);
        }
    }

    public string Name { get; }

    public void SayHello()
    {
        greeter.SayHello();
    }

    public void SayRole()
    {
        rolePlayer.SayRole();
    }

    public void SayTicketsOwned()
    {
        Console.WriteLine($"I have {ticketContainer.GetNumberOfTickets()} tickets:");
        foreach (var ticket in ticketContainer.GetTickets())
        {
            Console.WriteLine($"- {ticket.SiteValidFor}");
        }
    }

    public List<Ticket> BuyTickets(List<Ticket> ticketsRequested, string site)
    {
        List<Ticket> ticketsReceived = new List<Ticket>();

        foreach (Ticket requestedTicket in ticketsRequested)
        {
            ticketsReceived.AddRange(ticketContainer.BuyTickets(requestedTicket, site));
        }

        return ticketsReceived;
    }
}

// Define interfaces for dependencies
public interface ITicketContainer
{
    void AddTicket(Ticket ticket);
    IEnumerable<Ticket> GetTickets();
    int GetNumberOfTickets();
    List<Ticket> BuyTickets(Ticket requestedTicket, string site);
}

public interface ITicketMaster
{
    List<Ticket> GiveTickets(List<Ticket> ticketsRequested, IGreetable requester);
}

// Implementations of dependencies
public class Pocket : ITicketContainer
{
    private readonly List<Ticket> tickets = new List<Ticket>();

    public void AddTicket(Ticket ticket)
    {
        tickets.Add(ticket);
    }

    public IEnumerable<Ticket> GetTickets()
    {
        return tickets;
    }

    public int GetNumberOfTickets()
    {
        return tickets.Count;
    }

    public List<Ticket> BuyTickets(Ticket requestedTicket, string site)
    {
        if (requestedTicket.SiteValidFor == site)
        {
            tickets.Add(requestedTicket);
            return new List<Ticket> { requestedTicket };
        }

        return new List<Ticket>();
    }
}

public class TicketMaster : ITicketMaster
{
    private readonly TicketInventory ticketInventory;

    public TicketMaster(TicketInventory ticketInventory)
    {
        this.ticketInventory = ticketInventory;
    }

    public List<Ticket> GiveTickets(List<Ticket> ticketsRequested, IGreetable requester)
    {
        List<Ticket> availableTickets = new List<Ticket>();
        foreach (Ticket requestedTicket in ticketsRequested)
        {
            if (ticketInventory.HasTicket(requestedTicket.SiteValidFor))
            {
                Ticket ticket = ticketInventory.RemoveTicket(requestedTicket.SiteValidFor);
                Ticket stampedTicket = StampTicket(ticket, requester);
                availableTickets.Add(stampedTicket);
            }
            else
            {
                Console.WriteLine($"No more {requestedTicket.SiteValidFor} tickets left");
            }
        }
        return availableTickets;
    }

    public static Ticket StampTicket(Ticket ticket, IGreetable requester)
    {
        ticket.PersonValidFor = requester.ToString();
        // Stamp ticket with a new serial number
        ticket.SerialNumber = Guid.NewGuid().ToString();
        return ticket;
    }
}

public class TicketInventory : ITicketContainer
{
    private readonly Dictionary<string, List<Ticket>> tickets = new Dictionary<string, List<Ticket>>();

    public TicketInventory()
    {
        // Initialize tickets for different sites
        InitializeTickets("Aspen", 50);
        InitializeTickets("Vail", 50);
        InitializeTickets("Keystone", 50);
    }

    // Initialize tickets for a specific site with a given count
    private void InitializeTickets(string site, int count)
    {
        tickets[site] = Enumerable.Range(0, count).Select(_ => new Ticket(site)).ToList();
    }

    // Add a ticket to the inventory
    public void AddTicket(Ticket ticket)
    {
        if (!tickets.ContainsKey(ticket.SiteValidFor))
        {
            tickets[ticket.SiteValidFor] = new List<Ticket>();
        }
        tickets[ticket.SiteValidFor].Add(ticket);
    }

    // Get all tickets from the inventory
    public IEnumerable<Ticket> GetTickets()
    {
        return tickets.Values.SelectMany(t => t);
    }

    // Get the total number of tickets in the inventory
    public int GetNumberOfTickets()
    {
        return tickets.Values.Sum(t => t.Count);
    }

    // Check if there are tickets available for a specific site
    public bool HasTicket(string site)
    {
        return tickets.TryGetValue(site, out var siteTickets) && siteTickets.Count > 0;
    }

    // Remove a ticket for a specific site from the inventory
    public Ticket RemoveTicket(string site)
    {
        if (tickets.TryGetValue(site, out var siteTickets) && siteTickets.Count > 0)
        {
            Ticket ticket = siteTickets[0];
            siteTickets.RemoveAt(0);
            return ticket;
        }
        return null;
    }

    public List<Ticket> BuyTickets(Ticket requestedTicket, string site)
    {
        if (requestedTicket.SiteValidFor == site && HasTicket(site))
        {
            Ticket ticket = RemoveTicket(site);
            return new List<Ticket> { ticket };
        }

        return new List<Ticket>();
    }
}

// Define roles
public class SkierRole : IRole
{
    public void SayRole()
    {
        Console.WriteLine("I am a skier.");
    }

    public void ShowNextTicket()
    {
        // Implement show next ticket logic for skier
    }
}

public class TicketMasterRole : IRole
{
    public void SayRole()
    {
        Console.WriteLine("I am the ticket master.");
    }
}

// Define Ticket class
public class Ticket
{
    public string SiteValidFor { get; }
    public string PersonValidFor { get; set; }
    public string SerialNumber { get; set; }

    public Ticket(string siteValidFor, string personValidFor = "")
    {
        SiteValidFor = siteValidFor;
        PersonValidFor = personValidFor;
    }

    public Ticket(Ticket otherTicket)
    {
        SiteValidFor = otherTicket.SiteValidFor;
        PersonValidFor = otherTicket.PersonValidFor;
        SerialNumber = otherTicket.SerialNumber;
    }
}

// Main Program class
class Program
{
    static void Main(string[] args)
    {
        // Ticket list initialization
        List<Ticket> ticketList = new List<Ticket>
        {
            new Ticket("Aspen", "Mary"),
            new Ticket("Keystone", "Bob"),
            new Ticket("Vail")
        };

        // Ticket inventory creation
        var ticketInventory = new TicketInventory();

        // Greeting initialization
        var greeter = new EnglishGreeting();

        // Create instances of Skier and TicketMaster
        var mySkier = new Person("Bob", ticketList, greeter, new SkierRole(), new Pocket());
        var myTicketMaster = new Person("Mary", ticketList, greeter, new TicketMasterRole(), ticketInventory);

        // Display tickets owned by skier and ticket master
        mySkier.SayTicketsOwned();

        // Skier buys tickets from TicketMaster
        mySkier.BuyTickets(new List<Ticket> { new Ticket("Vail") }, "Vail");

        // Display tickets owned by skier and ticket master after purchase
        mySkier.SayTicketsOwned();

        // Test ticket copy constructor
        Ticket myTicket = new Ticket("Aspen", "Jane");
        Ticket copiedTicket = new Ticket(myTicket);
        Console.WriteLine($"{copiedTicket.PersonValidFor}");

        // Display tickets owned by skier after test ticket copy
        mySkier.SayTicketsOwned();
    }
}
