namespace MapAPIs.Models;

public class MonkeyModel
{
    private string name;

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    public string Location { get; set; }
    public string Details { get; set; }
    public string Image { get; set; }
    public int Population { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
