namespace ParkingLot.Shared.Parkings;

public abstract class ParkLocationRequest
{
    public class Index
    {
        public string? Searchterm { get; set; }
        public decimal? MaxFee { get; set; }
    }
}

