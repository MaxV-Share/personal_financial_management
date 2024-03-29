﻿using PersonalFinancialManagement.Common.Models;

namespace PersonalFinancialManagement.Models.Entities
{
    public class TransactionCategory : BaseEntity<Guid>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public Guid? ParentId { get; set; }
        public virtual TransactionCategory? Parent { get; set; }
        public virtual ICollection<TransactionCategory>? Childrens { get; set; }
        public Guid? TypeId { get; set; }
        public virtual TransactionCategoryType? Type { get; set; }
    }
}
