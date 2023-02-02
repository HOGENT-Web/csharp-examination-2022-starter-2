using ParkingLot.Client.Extensions;
using ParkingLot.Shared.Parkings;
using System.Net.Http.Json;
using System.Net;
using ParkingLot.Shared.Infrastructure;

namespace ParkingLot.Client.Parkings;

public class ParkLocationService : IParkLocationService
{
    private readonly HttpClient client;
    private const string endpoint = "api/parkLocation";
    public ParkLocationService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<ParkLocationResult.Index> GetIndexAsync(ParkLocationRequest.Index request)
    {
        var response = await client.GetFromJsonAsync<ParkLocationResult.Index>($"{endpoint}?{request.AsQueryString()}");
        return response!;
    }

    public async Task<ParkLocationResult.Create> CreateAsync(ParkLocationDto.Create request)
    {
        var response = await client.PostAsJsonAsync(endpoint, request);
        return await response.Content.ReadFromJsonAsync<ParkLocationResult.Create>();
    }
}
