using Ecommerce.Application.DTOs.Countries;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Mappers;

public static class CountriesMapper
{
    public static CountiesListDropdownResponseDTO MapToCountiesListDropdownResponseDTO(this Country country)
    {
        return new CountiesListDropdownResponseDTO
        {
            Id = country.Id,
            Name = country.Name
        };
    }
}
