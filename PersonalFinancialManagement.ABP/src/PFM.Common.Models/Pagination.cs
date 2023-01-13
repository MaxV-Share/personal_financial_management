using PFM;
using Volo.Abp.Application.Dtos;

namespace PFM.Common.Models
{
    public class Pagination : PagedResultRequestDto
    {
        public Pagination()
        {
            //PageIndex = 1;
            //PageSize = 10;
            //TotalRow = 0;
            //PageCount = 0;
        }
        //public int TotalRow { get; set; }
        //public int PageCount { get; set; } = MaxResultCount;
        //public int PageIndex { get; set; }
        //public int PageSize { get; set; }
    }
}
