namespace Ecommerce.Domain.Entities;

/*
Table States {
  Id Guid [Primary key]
  CountryId Guid [NOT NULL]
  Name string 
  Code string 
  IsActive boolean

  CreatedAt timestamp
  UpdatedAt timestamp
  DeletedAt timestamp

}

*/
public class State : BaseEntity
{
    public Guid CountryId { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public bool IsActive { get; set; } = true;

    public virtual Country? Country { get; set; }
    public virtual ICollection<City>? Cities { get; set; }
}

