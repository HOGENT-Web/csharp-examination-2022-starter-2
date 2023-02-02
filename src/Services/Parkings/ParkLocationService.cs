using ParkingLot.Domain.Parkings;
using ParkingLot.Persistence;
using ParkingLot.Shared.Parkings;
using Microsoft.EntityFrameworkCore;
using ParkingLot.Domain.Exceptions;
namespace ParkingLot.Services.Parkings;

public class ParkLocationService : IParkLocationService
{
    private readonly ParkingDbContext dbContext;

    public ParkLocationService(ParkingDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ParkLocationResult.Create> CreateAsync(ParkLocationDto.Create model)
    {
        if (await dbContext.ParkLocations.AnyAsync(x => x.Name == model.Name))
            throw new EntityAlreadyExistsException(nameof(ParkLocation), nameof(ParkLocation.Name), model.Name);

        ParkLocation parkLocation = new(model.Name!, model.Fee!, model.Capacity){
            Description = model.Description,
            MaxHeight = model.MaxHeight,
            LpgAllowed = model.LpgAllowed,
            HasToilets = model.HasToilets,
        };

        dbContext.ParkLocations.Add(parkLocation);
        await dbContext.SaveChangesAsync();

        ParkLocationResult.Create result = new()
        {
            ParkLocationId = parkLocation.Id,
        };

        return result;
    }

    public async Task<ParkLocationResult.Index> GetIndexAsync(ParkLocationRequest.Index request)
    {
        var query = dbContext.ParkLocations.AsQueryable();

        var items = await query
           .Select(x => new ParkLocationDto.Index
           {
               Id = x.Id,
               Name = x.Name,
               Fee = x.Fee,
               Capacity = x.Capacity,
               Available = x.Capacity, // TODO
           }).ToListAsync();

        var result = new ParkLocationResult.Index
        {
            Locations = items,
        };
        return result;
    }
}

