using System;
using System.Security.Claims;
using ParkingLot.Client.Authentication;
using Microsoft.AspNetCore.Components;

namespace ParkingLot.Client.Layout;

public partial class AccessControl
{
    [Inject] public FakeAuthenticationProvider FakeAuthenticationProvider { get; set; } = default!;

    private string? IsActive(ClaimsPrincipal principal)
    {
        return FakeAuthenticationProvider.Current.Identity?.Name == principal.Identity?.Name ? "is-active" : null;
    }

    private void ChangePrincipal(ClaimsPrincipal principal)
    {
        FakeAuthenticationProvider.ChangeAuthenticationState(principal);
    }
}

