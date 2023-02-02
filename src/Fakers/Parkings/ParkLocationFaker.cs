using ParkingLot.Domain.Parkings;

namespace ParkingLot.Fakers.Parkings;

public class ParkLocationFaker : EntityFaker<ParkLocation>
{
    public ParkLocationFaker(string locale = "en") : base(locale)
    {
        var heights = new[] { 1.8f, 1.9f, 2.0f, 2.05f, 2.2f };
        var bools = new[] { true, false };
        CustomInstantiator(f => new ParkLocation(f.Address.StreetName(), f.Random.Decimal(0, 5), f.Random.Number(10, 100)))
                .RuleFor(o => o.MaxHeight, f => f.PickRandom(heights))
                .RuleFor(o => o.LpgAllowed, f => f.PickRandom(bools))
                .RuleFor(o => o.Description, f => f.Lorem.Sentence(15));
    }
}