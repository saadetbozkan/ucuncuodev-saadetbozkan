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
            CreateMap<Vehicle, VehiclePutEntity>();
            CreateMap<VehiclePutEntity, Vehicle>();
            CreateMap<ContainerPutEntity, Container>();
            CreateMap<Container, ContainerPutEntity>();
            CreateMap<ContainerPostEntity, Container>();
            CreateMap<Container, ContainerPostEntity>();
        }

    }
}
