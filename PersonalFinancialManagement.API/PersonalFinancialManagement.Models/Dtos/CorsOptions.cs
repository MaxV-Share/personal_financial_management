namespace PersonalFinancialManagement.Models.Dtos
{
    public class CorsOptions
    {
        public CorsOptions()
        {
            PolicyName = "";
            PreflightMaxAgeInMinutes = "";
            AllowedOrigins = Array.Empty<string>();
            AllowedMethods = Array.Empty<string>();
            AllowedHeaders = Array.Empty<string>();
            ExposedHeaders = Array.Empty<string>();
        }
        public string PolicyName { get; set; }
        public string[] AllowedOrigins { get; set; }
        public string[] AllowedMethods { get; set; }
        public string[] AllowedHeaders { get; set; }
        public string[] ExposedHeaders { get; set; }
        public string PreflightMaxAgeInMinutes { get; set; }
    }
}
