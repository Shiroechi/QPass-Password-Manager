namespace QPass.Core.Security.RNG
{
    /// <summary>
    /// Interface structure for Pseduo Random Number Generator (PRNG).
    /// </summary>
    public interface IRNG
    {
		/// <summary>
		/// The name of the algorithm this generator implements.
		/// </summary>
		/// <returns></returns>
		string AlgorithmName();

        /// <summary>
        /// Reset the generator.
        /// Autoseed the seed or generator state.
        /// </summary>
        void Reset();

		/// <summary>
		/// Generate Boolean value from generator.
		/// </summary>
		/// <returns></returns>
		bool NextBoolean();

        /// <summary>
        /// Generate Integer value from generator.
        /// </summary>
        /// <returns></returns>
        uint NextInt();

		/// <summary>
		/// Generate Integer value between 
		/// lower and upper limit from generator.
		/// </summary>
		/// <param name="lower">Lower limit.</param>
		/// <param name="upper">Upper limit.</param>
		/// <returns></returns>
		uint NextInt(uint lower, uint upper);

		/// <summary>
		/// Generate Long value from generator. 
		/// </summary>
		/// <returns></returns>
		ulong NextLong();

		/// <summary>
		/// Generate Long value between 
		/// lower and upper limit from generator.
		/// </summary>
		/// <param name="lower">Lower limit.</param>
		/// <param name="upper">Upper limit.</param>
		/// <returns></returns>
		ulong NextLong(ulong lower, ulong upper);

		/// <summary>
		/// Generate Double value from generator.
		/// </summary>
		/// <returns></returns>
		double NextDouble();

		/// <summary>
		/// Generate Double value between 
		/// lower and upper limit from generator.
		/// </summary>
		/// <param name="lower">Lower limit.</param>
		/// <param name="upper">Upper limit.</param>
		/// <returns></returns>
		double NextDouble(double lower, double upper);
		
		/// <summary>
		/// Generate random byte[] value from generator.
		/// </summary>
		/// <param name="bytes"></param>
		void NextBytes(byte[] bytes);

		/// <summary>
		/// Generate random byte[] value from generator.
		/// </summary>
		/// <param name="length">Output length.</param>
		/// <returns></returns>
		byte[] GetBytes(int length);
    }
}