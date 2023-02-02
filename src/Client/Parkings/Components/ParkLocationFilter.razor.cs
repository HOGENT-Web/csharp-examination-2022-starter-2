using Microsoft.AspNetCore.Components;

namespace ParkingLot.Client.Parkings.Components;

public partial class ParkLocationFilter
{
    private string? searchTerm = string.Empty;
    private decimal? maxFee = null;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Parameter, EditorRequired] public string? SearchTerm { get; set; } = default!;
    [Parameter, EditorRequired] public decimal? MaxFee { get; set; }

    private void SearchTermChanged(ChangeEventArgs args)
    {
        searchTerm = args.Value?.ToString();
        FilterParkLocations();
    }
    
    void MaxFeeChanged(ChangeEventArgs args)
    {
        maxFee = string.IsNullOrWhiteSpace(args.Value?.ToString()) ? null : Convert.ToDecimal(args.Value!.ToString());
        FilterParkLocations();
    }

    void FilterParkLocations()
    {
        Dictionary<string, object?> parameters = new();

        parameters.Add(nameof(searchTerm), searchTerm);
        parameters.Add(nameof(maxFee), maxFee);

        var uri = NavigationManager.GetUriWithQueryParameters(parameters);

        NavigationManager.NavigateTo(uri);
    }
}