namespace QPass.Core.Security.KDF
{
	/// <summary>
	/// Key derivation Function
	/// </summary>
	interface IKDF
	{
		/// <summary>
		/// Return the name of the algorithm the KDF implements.
		/// </summary>
		/// <returns></returns>
		string AlgorithmName();

		/// <summary>
		/// Reset the KDF.
		/// </summary>
		void Reset();
		
		/// <summary>
		/// Computes the derived key for specified byte array. 
		/// </summary>
		/// <param name="data">Data to derive.</param>
		/// <param name="salt">Additional byte array.</param>
		/// <param name="length">Output length.</param>
		/// <returns></returns>
		byte[] Derive(byte[] data, byte[] salt, int length);
	}
}
