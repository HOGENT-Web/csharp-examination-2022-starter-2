using FluentValidation;

namespace ParkingLot.Shared.Parkings;

public class ParkLocationDto
{
    public class Index
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Fee { get; set; }
        public int Capacity { get; set; }
        public int Available { get; set; }
    }

    public class Detail
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Fee { get; set; }
        public int Capacity { get; set; }
        public float MaxHeight { get; set; }
        public bool LpgAllowed { get; set; }
        public bool HasToilets { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class Create
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Fee { get; set; }
        public int Capacity { get; set; }
        public float MaxHeight { get; set; }
        public bool LpgAllowed { get; set; }
        public bool HasToilets { get; set; }
        public string? Description { get; set; }

        public class Validator : AbstractValidator<Create>
        {
            public Validator()
            {
            }
        }
    }

}
