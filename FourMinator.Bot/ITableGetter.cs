namespace FourMinator.BotLogic
{
    public interface ITableGetter<TKey, TValue>
    {
        TKey[] GetKeys();
        TValue[] GetValues();
        int GetSize();
        int GetKeySize();
        int GetValueSize();
        TValue Get(TKey key);
        TValue GetBook(TKey key);
    }
}
