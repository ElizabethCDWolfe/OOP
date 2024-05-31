using Skiing;

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
