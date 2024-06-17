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

        if (args.Length < 1)
        {
            Console.WriteLine("Run program with the name of the skier that you would like to create");
        }

        String userSkierName = args[0];

        Skier mySkier = new Skier(userSkierName);
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
