using AutoMapper;

namespace PawChina.UI.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        protected override void Configure()
        {
            //NoteInfo==>NoteBackModel
            //CreateMap<NoteInfo, NoteBackModel>().ForMember(back => back.SeoInfo, n => n.Ignore());

            //Mapper Config验证
            Mapper.AssertConfigurationIsValid();
        }
    }
}