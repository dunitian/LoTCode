using AutoMapper;
using PawChina.Model;
using PawChina.UI.Areas.PawRoot.Models;

namespace PawChina.UI.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        protected override void Configure()
        {
            //NoteInfoView==>NoteInfo
            //CreateMap<NoteViewModel, NoteInfo>().ForMember(back => back.SeoInfo, n => n.Ignore());
            //CreateMap<ChineseViewModel, ChineseInfo>();//ChineseViewModel==>ChineseInfo

            //Mapper Config验证
            //Mapper.AssertConfigurationIsValid();
        }
    }
}