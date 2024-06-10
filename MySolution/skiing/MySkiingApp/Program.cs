using Skiing;

delegate int MyDelegate();

public static class TicketExtensions
{
    // Extension method to print the site name reversed
    public static void PrintReversedSite(this Ticket ticket)
    {
        if (ticket != null)
        {
            Console.WriteLine($"Reversed Site: {ReverseString(ticket.SiteValidFor)}");
        }
        else
        {
            Console.WriteLine("Ticket is null.");
        }
    }

    private static string ReverseString(string input)
    {
        char[] charArray = input.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}

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

        // Practicing extentions methods
        Ticket extensionTicket = new Ticket("Aspen");
        extensionTicket.PrintReversedSite(); 
    }
}
