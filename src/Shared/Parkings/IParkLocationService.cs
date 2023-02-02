namespace ParkingLot.Shared.Parkings;

public interface IParkLocationService
{
    Task<ParkLocationResult.Index> GetIndexAsync(ParkLocationRequest.Index request);
    // Task<ParkLocationDto.Detail> GetDetailAsync(int parkLocationId);
    Task<ParkLocationResult.Create> CreateAsync(ParkLocationDto.Create model);
    // Task EditAsync(int parkLocationId, ParkLocationDto.Mutate model);
}
