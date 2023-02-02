using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using ParkingLot.Shared.Parkings;

namespace ParkingLot.Server.Controllers.Parkings;

[ApiController]
[Route("api/[controller]")]
public class ParkLocationController : ControllerBase
{
    private readonly IParkLocationService parkLocationService;

    public ParkLocationController(IParkLocationService parkLocationService)
    {
        this.parkLocationService = parkLocationService;
    }

    [HttpGet, AllowAnonymous]
    public async Task<ParkLocationResult.Index> GetIndex([FromQuery] ParkLocationRequest.Index request)
    {
        return await parkLocationService.GetIndexAsync(request);
    }




}
