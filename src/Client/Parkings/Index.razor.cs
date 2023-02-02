using ParkingLot.Shared.Parkings;
using Microsoft.AspNetCore.Components;

namespace ParkingLot.Client.Parkings;

public partial class Index
{
    private IEnumerable<ParkLocationDto.Index>? parkings;

    [Inject] public IParkLocationService ParkLocationService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    [Parameter, SupplyParameterFromQuery] public string? SearchTerm { get; set; }
    [Parameter, SupplyParameterFromQuery] public decimal? MaxFee { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await FetchParkingsAsync();
    }
    private async Task FetchParkingsAsync()
    {
        ParkLocationRequest.Index request = new();

        var response = await ParkLocationService.GetIndexAsync(request);
        parkings = response.Locations;
    }

    private void ShowCreateForm()
    {
        NavigationManager.NavigateTo($"parking/create");
    }
}