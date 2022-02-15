using AutoMapper;
using DomainModels = StudentAdminPortalAPI.DomainModels;
using DataModels = StudentAdminPortalAPI.DataModels;

namespace StudentAdminPortalAPI.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Map DataModels to DomainModels
            CreateMap<DataModels.Student, DomainModels.Student>()
                .ReverseMap();

            CreateMap<DataModels.Gender, DomainModels.Gender>()
                .ReverseMap();

            CreateMap<DataModels.Address, DomainModels.Address>()
                .ReverseMap();
        }
    }
}
