using AutoMapper;
using Template.Domain.DTOs;
using Template.Domain.Entities;

namespace Template.Core.ObjectMapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
    }
}