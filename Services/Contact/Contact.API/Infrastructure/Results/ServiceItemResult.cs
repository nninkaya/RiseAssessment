namespace Contact.API.Infrastructure.Results
{
    public class ServiceItemResult<T> : ServiceResult where T : new()
    {
        T _item;

        public T Item
        {
            get
            {
                if (_item == null)
                {
                    _item = new T();
                }

                return _item;
            }
            set { _item = value; }
        }
    }
}
