using AutoMapper;
using SpaceMetaApi.Dtos;
using SpaceMetaApi.Entities;

namespace SpaceMetaApi.Configuration;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<RoleDto, Role>();
        CreateMap<Role, RoleDto>();
    }
}
