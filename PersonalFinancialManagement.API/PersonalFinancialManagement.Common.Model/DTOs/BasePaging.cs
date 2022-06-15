using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Common.Models;

namespace PersonalFinancialManagement.Common.Models.DTOs
{

    public class BasePaging<T> : IBasePaging<T>
    {
        public Pagination Pagination { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
    public interface IBasePaging<T>
    {
        public Pagination Pagination { get; set; }
        public IEnumerable<T> Data { get; set; }
    }

}
