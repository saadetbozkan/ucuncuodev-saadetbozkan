using AutoMapper;
using Data.DataModel;
using Entity;
using System;

namespace Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Vehicle, VehicleEntity>();
            CreateMap<VehicleEntity, Vehicle>();
            CreateMap<ContainerEntity, Container>();
            CreateMap<Container, ContainerEntity>();
        }

    }
}
