using System;
using PFM;
using Volo.Abp.Domain.Entities.Auditing;

namespace PFM.Common.Models
{
    public class ImageInfo : AuditedAggregateRoot<Guid>
    {
        public string? Name { get; set; }
        public string? ClientName { get; set; }
        public string? Url { get; set; }
        public int? Size { get; set; }
    }
}
