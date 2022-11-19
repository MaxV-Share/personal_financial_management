using AutoMapper;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType.Requests;
using PersonalFinancialManagement.Models.Entities;

namespace App.Models.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<TransactionCategoryType, TransactionCategoryTypeCreateRequest>().ReverseMap();
            CreateMap<TransactionCategoryType, TransactionCategoryTypeUpdateRequest>().ReverseMap();
            CreateMap<TransactionCategoryType, TransactionCategoryTypeViewModel>().ReverseMap();

        }

    }
}
