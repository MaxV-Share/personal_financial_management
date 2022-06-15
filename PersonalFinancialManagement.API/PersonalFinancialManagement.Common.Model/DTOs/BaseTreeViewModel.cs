using System.Collections.Generic;
using PersonalFinancialManagement.Common.Models.DTOs;

namespace PersonalFinancialManagement.Common.Models.DTOs
{
    public interface IBaseTreeViewModel<T>
    {
        public T Data { get; set; }
        public List<IBaseTreeViewModel<T>> Children { get; set; }
    }
}
