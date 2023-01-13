using System.Collections.Generic;
using PFM.Common.Models.DTOs;

namespace PFM.Common.Models.DTOs
{
    public interface IBaseTreeViewModel<T>
    {
        public T Data { get; set; }
        public List<IBaseTreeViewModel<T>> Children { get; set; }
    }
}
