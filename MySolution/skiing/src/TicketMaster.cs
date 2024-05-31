namespace Skiing;

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
