namespace ParkingLot.Shared.Parkings;

public abstract class ParkLocationResult
{
    public class Index
    {
        public IEnumerable<ParkLocationDto.Index>? Locations { get; set; }
        public int TotalAmount { get; set; }
    }

    public class Create
    {
        public int ParkLocationId { get; set; }
    }
}
