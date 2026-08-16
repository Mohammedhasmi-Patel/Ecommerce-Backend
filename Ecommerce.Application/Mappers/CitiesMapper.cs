using Ecommerce.Application.DTOs.Cities;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Mappers;

public static class CitiesMapper
{
    public static CityListDropdownResponseDTO MapToCityListDropdownResponseDTO(this City city)
    {
        return new CityListDropdownResponseDTO
        {
            Id = city.Id,
            Name = city.Name
        };
    }
}
