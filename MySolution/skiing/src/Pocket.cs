namespace Skiing;

class Pocket : TicketContainer
{
    public void AddTicket(Ticket ticket)
    {
        TicketList.Add(ticket);
    }
}
