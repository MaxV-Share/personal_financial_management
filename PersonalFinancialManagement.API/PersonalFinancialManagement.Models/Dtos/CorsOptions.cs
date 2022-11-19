using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManagement.Models.Dtos
{
    public class CorsOptions
    {
        public string PolicyName { get; set; }
        public string[] AllowedOrigins { get; set; }
        public string[] AllowedMethods { get; set; }
        public string[] AllowedHeaders { get; set; }
        public string[] ExposedHeaders { get; set; }
        public string PreflightMaxAgeInMinutes { get; set; }
    }
}
