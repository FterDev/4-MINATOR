namespace FourMinator.BotLogic
{
    internal class UtilFunctions
    {
        // util functions to compute next prime at compile time
        public static ulong Med(ulong min, ulong max)
        {
            return (min + max) / 2;
        }

        // Determines if an integer n has a divisor between min (inclusive) and max (exclusive)
        public static bool HasFactor(ulong n, ulong min, ulong max)
        {
            if (min * min > n) return false; // do not search for factor above sqrt(n)
            if (min + 1 >= max) return n % min == 0;
            return HasFactor(n, min, Med(min, max)) || HasFactor(n, Med(min, max), max);
        }

        // Returns the next prime number greater or equal to n. n must be >= 2
        public static ulong NextPrime(ulong n)
        {
            return HasFactor(n, 2, n) ? NextPrime(n + 1) : n;
        }

        // Calculates log2 of n
        public static uint Log2(uint n)
        {
            return n <= 1 ? 0 : Log2(n / 2) + 1;
        }
    }
}
