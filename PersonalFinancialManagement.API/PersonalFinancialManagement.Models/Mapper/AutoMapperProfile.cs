using AutoMapper;
using PersonalFinancialManagement.Models.Dtos.Currencies;
using PersonalFinancialManagement.Models.Dtos.Currencies.Requests;
using PersonalFinancialManagement.Models.Dtos.PaymentAccounts;
using PersonalFinancialManagement.Models.Dtos.PaymentAccounts.Requests;
using PersonalFinancialManagement.Models.Dtos.TransactionCategories;
using PersonalFinancialManagement.Models.Dtos.TransactionCategories.Requests;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryTypes;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryTypes.Requests;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType.Requests;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType;

namespace App.Models.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<TransactionCategory, TransactionCategoryCreateRequest>().ReverseMap();
            CreateMap<TransactionCategory, TransactionCategoryUpdateRequest>().ReverseMap();
            CreateMap<TransactionCategory, TransactionCategoryViewModel>().ReverseMap();

            CreateMap<TransactionCategoryType, TransactionCategoryTypeCreateRequest>().ReverseMap();
            CreateMap<TransactionCategoryType, TransactionCategoryTypeUpdateRequest>().ReverseMap();
            CreateMap<TransactionCategoryType, TransactionCategoryTypeViewModel>().ReverseMap();

            CreateMap<Currency, CurrencyCreateRequest>().ReverseMap();
            CreateMap<Currency, CurrencyUpdateRequest>().ReverseMap();
            CreateMap<Currency, CurrencyViewModel>().ReverseMap();

            CreateMap<PaymentAccountType, PaymentAccountTypeCreateRequest>().ReverseMap();
            CreateMap<PaymentAccountType, PaymentAccountTypeUpdateRequest>().ReverseMap();
            CreateMap<PaymentAccountType, PaymentAccountTypeViewModel>().ReverseMap();
            
            
            CreateMap<PaymentAccount, PaymentAccountCreateRequest>().ReverseMap();
            CreateMap<PaymentAccount, PaymentAccountUpdateRequest>().ReverseMap();
            CreateMap<PaymentAccount, PaymentAccountViewModel>().ReverseMap();

        }
    }
}
