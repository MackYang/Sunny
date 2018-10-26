using AutoMapper;
using Sunny.Api.DTO.Request.Demo;
using Sunny.Api.FluentValidation2;
using Sunny.Repository.DbModel.Model;

namespace Sunny.Api.DTO.Response
{
    public class ResponseMapperConfig : Profile
    {
        public ResponseMapperConfig()
        {

            CreateMap<IdTest, Customer>()
                .ForMember(cus => cus.LocalType, opt => opt.MapFrom(id => id.requestType)).ReverseMap();
            //手动指定字段映射关系
            //.ForMember(cus => cus.LocalType, opt => opt.MapFrom(id => id.requestType))
            //.ReverseMap()映射反向转换

            CreateMap<Buyer, Seller>().ReverseMap();
            CreateMap<BuyerSub, SellerSub>().ReverseMap();
            CreateMap<BuyerComment, SellerComment>().ReverseMap();
        }


    }
}
