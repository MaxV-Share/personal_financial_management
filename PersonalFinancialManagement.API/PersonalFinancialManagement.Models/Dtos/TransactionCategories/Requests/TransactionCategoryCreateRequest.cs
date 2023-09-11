﻿using PersonalFinancialManagement.Common.Models.DTOs;

namespace PersonalFinancialManagement.Models.Dtos.TransactionCategories.Requests;

public class TransactionCategoryCreateRequest : BaseCreateRequest, IBaseTransactionCategory
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public Guid? ParentId { get; set; }
}