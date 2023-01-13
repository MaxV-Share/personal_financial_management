using System;
using PFM.Common.Models.DTOs;
using Volo.Abp.Application.Dtos;

namespace PFM.Common.Models.DTOs
{
    public abstract class BaseViewModel<TKey> : IEntityDto<TKey>
    {
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? CreateBy { get; set; }
        public string? UpdateBy { get; set; }
        public TKey Id { get ; set ; }
    }
}
