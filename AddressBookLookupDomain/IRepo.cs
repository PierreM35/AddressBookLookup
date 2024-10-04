namespace AddressBookLookupDomain
{
    public interface IRepo<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Func<T, bool> predicate);
        void Add(T data);
        void Remove(Func<T, bool> predicate);
    }
}
