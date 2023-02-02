namespace ParkingLot.Domain.Parkings;

public class ParkRecord : Entity
{
    public DateTime StartTime { get; }
    public DateTime? EndTime { get; set; }

    private string? licensePlate;
    public string? LicensePlate
    {
        get => licensePlate;
        private set => licensePlate = value;
    }
    public ParkLocation ParkLocation { get; } = default!;

    public bool Occupied => EndTime == null;

    // fees can change, but these records should be permanent
    public decimal Fee { get; }

    /// <summary>
    /// Database Constructor
    /// </summary>
    private ParkRecord() { }
    internal ParkRecord(string licensePlate, ParkLocation location)
    {
        LicensePlate = licensePlate;
        StartTime = DateTime.UtcNow;
        Fee = location.Fee;
        ParkLocation = location;
    }

    public void EndParking()
    {
        EndTime = DateTime.UtcNow;
    }

    public decimal CalculateCost()
    {
        if (EndTime == null)
            throw new ApplicationException("Parking is not ended yet, could not calculate the cost.");

        var duration = Math.Ceiling((EndTime.Value - StartTime).TotalHours);
        return Fee * (decimal)duration;
    }
}
