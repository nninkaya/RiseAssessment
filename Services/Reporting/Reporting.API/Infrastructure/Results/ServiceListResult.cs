namespace Reporting.API.Infrastructure.Results
{
    public class ServiceListResult<T> : ServiceResult
    {
        List<T> _items;

        public List<T> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<T>();
                }

                return _items;
            }

            set { _items = value; }
        }
    }
}
