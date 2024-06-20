using System.Reflection.Metadata;
using Skiing;

delegate int MyDelegate();

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class CustomAttribute : Attribute
{
    public string Description { get; }

    public CustomAttribute(string description)
    {
        Description = description;
    }
}

[CustomAttribute("Custom attribute for Ticket class")]
public class Ticket
{
    public string SiteValidFor { get; }

    public Ticket(string siteValidFor)
    {
        SiteValidFor = siteValidFor;
    }

    public int GenerateSerialNumber()
    {
        // Generate serial number logic
        return new Random().Next(1000, 9999);
    }
}


public static class TicketExtensions
{
    // Extension method to print the site name reversed
    public static void PrintReversedSite(this Ticket ticket)
    {
        if (ticket != null)
        {
            // Accessing custom attribute
            var customAttributes = ticket.GetType().GetCustomAttributes(typeof(CustomAttribute), false);
            if (customAttributes.Length > 0)
            {
                var customAttribute = (CustomAttribute)customAttributes[0];
                Console.WriteLine($"Custom Attribute Description: {customAttribute.Description}");
            }

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
        else
        {
            String userSkierName = args[0];

            Skier mySkier = new Skier(userSkierName);
            mySkier.Greeting.SayHello();

            TicketMaster myTicketMaster = new TicketMaster("Jane");
            myTicketMaster.SellTicket(mySkier, "Breckenridge");
        }

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
