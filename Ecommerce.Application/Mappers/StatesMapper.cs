using Ecommerce.Application.DTOs.States;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Mappers;

public static class StatesMapper
{
    public static StateListDropdownResponseDTO MapToStateListDropdownResponseDTO(this State state)
    {
        return new StateListDropdownResponseDTO
        {
            Id = state.Id,
            Name = state.Name
        };
    }
}
