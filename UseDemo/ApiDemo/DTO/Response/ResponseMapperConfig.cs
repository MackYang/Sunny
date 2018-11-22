using ApiDemo.DTO.Request.Demo;
using ApiDemo.FluentValidation2;
using AutoMapper;
using RepositoryDemo.DbModel;

namespace ApiDemo.DTO.Response
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
