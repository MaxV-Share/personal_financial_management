namespace PersonalFinancialManagement.Common.Models
{
    public class GlobalVariable<T>
    {
        private static T? _object;
        public GlobalVariable(T? Object)
        {
            _object = Object;
        }

        public static T? Object => _object;
    }
}
