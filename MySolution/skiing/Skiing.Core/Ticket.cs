namespace Skiing;

public class Ticket
{
    private string _personValidFor;
    public string SiteValidFor { get; }
    public int SerialNumber { get; }
    public string PersonValidFor
    {
        set
        {
            if (_personValidFor != "")
            {
                throw new Exception("Person valid for is already set ");
            }
            _personValidFor = value;
        }
        get { return _personValidFor; }
    }

    public Ticket(string site)
    {
        SiteValidFor = site;
        SerialNumber = GenerateSerialNumber();
        _personValidFor = "";
    }

    public int GenerateSerialNumber()
    {
        Random dice = new Random();
        return dice.Next(0, 1000);
    }
}
