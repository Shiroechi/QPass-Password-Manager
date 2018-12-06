namespace Akashic.Utilities.Number
{
    public static class Prime
    {
        /// <summary>
        /// Check the value is prime or not.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>
        /// True, if the the value is prime.
        /// False, otherwise.
        /// </returns>
        public static bool IsPrime(short value)
        {
            for (short i = 2; i < value; i++)
            {
                if (value % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Check the value is prime or not.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>
        /// True, if the the value is prime.
        /// False, otherwise.
        /// </returns>
        public static bool IsPrime(int value)
        {
            for (int i = 2; i < value; i++)
            {
                if (value % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Check the value is prime or not.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>
        /// True, if the the value is prime.
        /// False, otherwise.
        /// </returns>
        public static bool IsPrime(uint value)
        {
            for (uint i = 2; i < value; i++) 
            {
                if (value % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Check the value is prime or not.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>
        /// True, if the the value is prime.
        /// False, otherwise.
        /// </returns>
        public static bool IsPrime(long value)
        {
            for (long i = 2; i < value; i++)
            {
                if (value % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Check the value is prime or not.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>
        /// True, if the the value is prime.
        /// False, otherwise.
        /// </returns>
        public static bool IsPrime(ulong value)
        {
            for (ulong i = 2; i < value; i++)
            {
                if (value % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}