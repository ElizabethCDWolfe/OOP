namespace Skiing;

abstract class TicketContainer
{
    protected List<Ticket> TicketList;

    protected TicketContainer()
    {
        TicketList = new List<Ticket>();
    }
}
