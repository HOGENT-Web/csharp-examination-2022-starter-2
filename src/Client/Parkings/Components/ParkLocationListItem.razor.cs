using ParkingLot.Shared.Parkings;
using Microsoft.AspNetCore.Components;

namespace ParkingLot.Client.Parkings.Components;

public partial class ParkLocationListItem
{
    [Parameter, EditorRequired]
    public ParkLocationDto.Index ParkLocation { get; set; } = null!;
}