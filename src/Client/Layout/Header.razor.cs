using Microsoft.AspNetCore.Components;

namespace ParkingLot.Client.Layout;

public partial class Header : IDisposable
{
    private bool isOpen;
    private string? isOpenClass => isOpen ? "is-active" : null;

    protected override void OnInitialized()
    {
    }

    public void Dispose()
    {
    }

    private void ToggleMenuDisplay()
    {
        isOpen = !isOpen;
    }

    
}

