using ParkingLot.Domain.Parkings;

namespace Domain.Tests.Parkings;

public class ParkRecord_Should
{
    [Fact]
    public void ParkLocation_has_one_record_after_creating()
    {
        var location = new ParkLocation("test", 1, 1);

        ParkRecord record = location.TakeTicket("1-ABC-123");

        location.Records.Count.ShouldBe(1);
        location.Records.First().ShouldBe(record);
    }

    [Fact]
    public void Customer_cannot_park_twice_in_the_same_ParkLocation_at_the_same_time()
    {
        var location = new ParkLocation("test", 10, 10);
        var licensePlate = "1-ABC-123";
        ParkRecord record = location.TakeTicket(licensePlate);
        Action act = () =>
        {
            ParkRecord record = location.TakeTicket(licensePlate);
        };

        act.ShouldThrow<ApplicationException>();
    }

    [Fact]
    public void Customer_can_park_again_after_stopping_previous_session()
    {
        var location = new ParkLocation("test", 1, 1);
        var licensePlate1 = "1-ABC-123";
        var licensePlate2 = "1-BCD-234";

        ParkRecord record = location.TakeTicket(licensePlate1);
        record.EndParking();
        ParkRecord record2 = location.TakeTicket(licensePlate2);

        location.Records.Count.ShouldBe(2);
        location.Records.First().ShouldBe(record);
        location.Records.Last().ShouldBe(record2);
    }

    [Fact]
    public void Customer_cannot_park_in_full_parking()
    {
        var location = new ParkLocation("test", 1, 1);
        var licensePlate1 = "1-ABC-123";
        var licensePlate2 = "1-BCD-234";

        ParkRecord record = location.TakeTicket(licensePlate1);
        Action act = () =>
        {
            ParkRecord record2 = location.TakeTicket(licensePlate2);
        };

        act.ShouldThrow<ApplicationException>();
    }

    [Theory]
    [InlineData("  ")] // no spaces
    [InlineData("VANITY")] // no vanity plates
    [InlineData("ABC-123")] // no old plates
    [InlineData("0-ABC-123")] // doesn't start with 0
    [InlineData("1-123-ABC")] // not reversed
    [InlineData("1-ABC-1234")] // too many digits
    [InlineData("1-ABC-12")] // not enough digits
    [InlineData("1-ABCD-123")] // too many chars
    [InlineData("1-AB-123")] // not enough chars
    public void Have_a_valid_license_plate(string licensePlate) {
        var location = new ParkLocation("Parking Zuid", 10, 1000);
        Assert.Throws<ArgumentException>(() => location.TakeTicket(licensePlate));
    }
}
