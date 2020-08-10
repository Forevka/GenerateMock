using AutoMapper;
using GenerateMock.Dal.Models.DB;
using GenerateMock.Dal.Models.View.Out;

namespace GenerateMock.Dal
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RepositoryDatabaseDb, RepositoryDatabaseOutViewModel>()
                .ForMember(x => x.DatabaseApiUrl,
                    o => o.MapFrom(x => x.GetApiUrl()));

            CreateMap<RepositoryDb, RepositoryOutViewModel>();
        }
    }
}