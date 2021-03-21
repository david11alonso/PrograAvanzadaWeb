using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using data = Solution.DO.Objects;

namespace Solution.API.Mapping
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<data.AspNetUsers, DataModels.AspNetUsers>().ReverseMap();
            CreateMap<data.AspNetRoles, DataModels.AspNetRoles>().ReverseMap();
            CreateMap<data.AspNetUserRoles, DataModels.AspNetUserRoles>().ReverseMap();
            CreateMap<data.Departamento, DataModels.Departamento>().ReverseMap();
            CreateMap<data.Foro, DataModels.Foro>().ReverseMap();


        }
    }
}
