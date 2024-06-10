using Skiing;

delegate int MyDelegate();

class Program
{
    static void Main(string[] args)
    {
        Skier mySkier = new Skier("Tom");
        mySkier.Greeting.SayHello();

        TicketMaster myTicketMaster = new TicketMaster("Jane");
        myTicketMaster.SellTicket(mySkier, "Breckenridge");

        Ticket myTicket = new Ticket("Aspen");

        // Practicing Delegates
        MyDelegate generateSerialNumberDelegate = myTicket.GenerateSerialNumber;
        int numberGenerated = generateSerialNumberDelegate();
        Console.WriteLine(numberGenerated);
    }
}
