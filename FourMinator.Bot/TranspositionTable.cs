namespace FourMinator.BotLogic
{
    public class TranspositionTable<TPartialKey, TValue> : ITableGetter<ulong, TValue> where TPartialKey : IConvertible
    {
        private readonly int _size;
        private readonly TPartialKey[] _keys;
        private readonly TValue[] _values;

        public TranspositionTable(int logSize)
        {
            _size = (int)UtilFunctions.NextPrime(1UL << logSize);
            _keys = new TPartialKey[_size];
            _values = new TValue[_size];
            Reset();
        }

        public void Reset()
        {
            Array.Clear(_keys, 0, _keys.Length);
            Array.Clear(_values, 0, _values.Length);
        }

        private int Index(ulong key)
        {
            return (int)(key % (ulong)_size);
        }

        private static TPartialKey ConvertKey(ulong key)
        {
            Type targetType = typeof(TPartialKey);

            if (targetType == typeof(byte))
                return (TPartialKey)(object)(byte)key;
            if (targetType == typeof(ushort))
                return (TPartialKey)(object)(ushort)key;
            if (targetType == typeof(uint))
                return (TPartialKey)(object)(uint)key;
            if (targetType == typeof(ulong))
                return (TPartialKey)(object)key;

            throw new InvalidCastException($"Unsupported key type: {targetType.Name}");
        }

        public void Put(ulong key, TValue value)
        {
            var pos = Index(key);
            _keys[pos] = ConvertKey(key);
            _values[pos] = value;
        }


        public TValue Get(ulong key)
        {
            int pos = Index(key);
            if (_keys[pos].Equals(ConvertKey(key)))
            {
                return _values[pos];
            }
            else
            {
                return default(TValue);
            }
        }

        public TValue GetBook(ulong key)
        {
            int pos = Index(key);
            if (pos == (int)key)
            {
                return _values[pos];
            }
            else
            {
                return default(TValue);
            }
        }

        public ulong[] GetKeys()
        {
            ulong[] ulongKeys = new ulong[_keys.Length];
            for (int i = 0; i < _keys.Length; i++)
            {
                ulongKeys[i] = Convert.ToUInt64(_keys[i]);
            }
            return ulongKeys;
        }

        public TValue[] GetValues()
        {
            return _values;
        }

        public int GetSize()
        {
            return _size;
        }

        public int GetKeySize()
        {
            return System.Runtime.InteropServices.Marshal.SizeOf(typeof(TPartialKey));
        }

        public int GetValueSize()
        {
            return System.Runtime.InteropServices.Marshal.SizeOf(typeof(TValue));
        }
        TValue ITableGetter<ulong, TValue>.Get(ulong key)
        {
            return Get(key);
        }
    }
}