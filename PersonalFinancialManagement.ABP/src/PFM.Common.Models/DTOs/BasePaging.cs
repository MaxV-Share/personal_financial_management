using System.Collections;
using System.Collections.Generic;
using PFM.Common.Models;
using PFM.Common.Models.DTOs;
using Volo.Abp.Application.Dtos;

namespace PFM.Common.Models.DTOs
{

    public class BasePaging<T> : PagedResultDto<T>
    {
        public Pagination? Pagination { get; set; }
    }
}
