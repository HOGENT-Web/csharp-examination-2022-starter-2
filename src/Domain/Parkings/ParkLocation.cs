namespace ParkingLot.Domain.Parkings;

public class ParkLocation : Entity
{
    private string? name;
    public string? Name
    {
        get => name;
        private set => name = value;
    }

    private decimal fee;
    public decimal Fee
    {
        get => fee;
        set => fee = value;
    }

    private int capacity;
    public int Capacity
    {
        get => capacity;
        set => capacity = value;
    }
    
    public float MaxHeight { get; set; }

    public bool LpgAllowed { get; set; }

    public bool HasToilets { get; set; }
    
    public string? Description { get; set; }

    private readonly List<ParkRecord> records = new();
    public IReadOnlyCollection<ParkRecord> Records => records.AsReadOnly();

    /// <summary>
    /// Database Constructor
    /// </summary>
    private ParkLocation() { }
    
    public ParkLocation(string name, decimal fee, int capacity)
    {
        Name = name;
        Fee = fee;
        Capacity = capacity;
    }

    public ParkRecord TakeTicket(string licensePlate) {
        var result = new ParkRecord(licensePlate, this);
        AddRecord(result);
        return result;
    }

    private void AddRecord(ParkRecord record)
    {

    }
}
