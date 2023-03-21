namespace PersonalFinancialManagement.Common.Models.DTOs
{

    public class BasePaging<T> : IBasePaging<T>
    {
        public BasePaging()
        {
            Pagination = new Pagination();
        }
        public Pagination Pagination { get; set; }
        public IEnumerable<T>? Data { get; set; }
    }
    public interface IBasePaging<T>
    {
        public Pagination Pagination { get; set; }
        public IEnumerable<T>? Data { get; set; }
    }

}
