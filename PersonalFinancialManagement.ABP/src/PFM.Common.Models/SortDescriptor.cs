using PFM;
using PFM.Common.Models.Enums;

namespace PFM.Common.Models
{
    public class SortDescriptor
    {
        public string? Field { get; set; }
        public SortDirection Direction { get; set; }
    }
}
