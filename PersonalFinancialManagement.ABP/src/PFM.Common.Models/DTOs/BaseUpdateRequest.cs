using PFM.Common.Models.DTOs;
using System;

namespace PFM.DTOs
{
    public abstract class BaseUpdateRequest<TKey> : BaseDTO
    {
        public TKey Id { get; set; }
    }
}
