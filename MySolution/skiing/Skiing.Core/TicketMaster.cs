namespace Skiing;

public class TicketMaster : Skier
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
        try
        {
            // Attempt to remove ticket for the specified site
            Ticket? ticket = TicketMasterInventory.RemoveTicket(site);

            if (ticket is not null)
            {
                // Found a ticket, proceed with your logic
                Ticket stampedTicket = StampTicket(ticket, skier.Name);
                skier.SkierPocket.AddTicket(stampedTicket);
            }
            else
            {
                // No ticket found for the specified site
                Console.WriteLine($"No tickets found for {site}");
            }
        }
        catch (Exception e)
        {
            // Handle exceptions if needed
            Console.WriteLine($"An error occurred: {e.Message}");
            Console.WriteLine($"Stack Trace: {e.StackTrace}");
        }
    }

    private Ticket StampTicket(Ticket ticket, string name)
    {
        ticket.PersonValidFor = name;
        return ticket;
    }
}
