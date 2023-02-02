using ParkingLot.Fakers.Parkings;

namespace ParkingLot.Persistence;

public class FakeSeeder
{
    private readonly ParkingDbContext dbContext;

    public FakeSeeder(ParkingDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Seed()
    {
        // Not a good idea in production.
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        SeedParkings();
    }

    private void SeedParkings()
    {
        var parkings = new ParkLocationFaker().AsTransient().UseSeed(1337).Generate(15);
        dbContext.ParkLocations.AddRange(parkings);
        var pl = parkings[0];
        pl.TakeTicket("1-ABC-123");
        pl.TakeTicket("1-ABC-234");
        pl.TakeTicket("1-ABC-345");
        pl.TakeTicket("1-ABC-456");

        dbContext.SaveChanges();
    }
}

