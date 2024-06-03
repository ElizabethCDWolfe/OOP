namespace Skiing;

class Inventory : TicketContainer
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

    public Ticket? RemoveTicket(string site)
    {
        Ticket? ticketToRemove = null;

        try
        {
            // Using LINQ library FirstOrDefault to get ticket, then using a lambda expression
            // to match the ticket site to the site we are searching for
            ticketToRemove = TicketList.FirstOrDefault(ticket => ticket.SiteValidFor == site);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }

        return ticketToRemove;
    }
}
