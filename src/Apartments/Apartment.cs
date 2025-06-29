using Bookify.Domain.Abstractions;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Apartments;

public sealed class Apartment : Entity
{
    public Name Name { get; private set; }
    
    public Description Description { get; private set; }
    
    public Address Address { get; private set; }
    
    public Money Price { get; private set; }
    
    public Money CleaningFee { get; private set; }
    
    public DateTime? LastBookedOnUtc { get; private set; }

    public List<Amenity> Amenities { get; private set; } = [];

    public Apartment(Guid id) : base(id)
    {
    }
}