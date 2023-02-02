using Microsoft.AspNetCore.Components;
using ParkingLot.Shared.Parkings;

namespace ParkingLot.Client.Parkings;

public partial class Create
{
    private ParkLocationDto.Create parkLocation = new();
    [Inject] public IParkLocationService ParkLocationService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    private async Task CreateParkLocationAsync()
    {
        ParkLocationResult.Create result = await ParkLocationService.CreateAsync(parkLocation);

        NavigationManager.NavigateTo($"parking");
    }
}