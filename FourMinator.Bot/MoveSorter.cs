namespace FourMinator.BotLogic
{
    public class MoveSorter
    {
        private int size;
        private readonly (ulong move, int score)[] entries = new (ulong move, int score)[7];


        public MoveSorter()
        {
            size = 0;
        }

        public void Add(ulong move, int score)
        {
            int pos = size++;
            for (; pos > 0 && entries[pos - 1].score > score; --pos)
            {
                entries[pos] = entries[pos - 1];
            }
            entries[pos].move = move;
            entries[pos].score = score;
        }

        public ulong GetNext()
        {
            return size > 0 ? entries[--size].move : 0;
        }

        public void Reset()
        {
            size = 0;
        }
    }
}