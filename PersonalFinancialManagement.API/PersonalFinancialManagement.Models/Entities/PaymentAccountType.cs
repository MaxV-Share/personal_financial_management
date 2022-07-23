using PersonalFinancialManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManagement.Models.Entities
{
    /// <summary>
    /// Loại tài khoản thanh toán
    /// </summary>
    public class PaymentAccountType : BaseEntity<Guid>
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
