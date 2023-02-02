using ParkingLot.Domain.Parkings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ParkingLot.Persistence.Configurations.Parkings;

internal class ParkLocationConfiguration : IEntityTypeConfiguration<ParkLocation>
{
    public void Configure(EntityTypeBuilder<ParkLocation> builder)
    {
    }
}