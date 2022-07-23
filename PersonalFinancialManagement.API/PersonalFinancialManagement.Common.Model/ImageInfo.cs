using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManagement.Common.Models
{
    public class ImageInfo : BaseEntity<Guid>
    {
        public string? Name { get; set; }
        public string? ClientName { get; set; }
        public string? Url { get; set; }
        public int? Size { get; set; }
    }
}
