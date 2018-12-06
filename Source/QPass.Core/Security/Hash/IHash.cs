namespace QPass.Core.Security.Hash
{
	/// <summary>
	/// Outer structure of hash fuction.
	/// </summary>
	public interface IHash
	{
		/// <summary>
		/// Hash algorithm name.
		/// </summary>
		/// <returns></returns>
		string AlgorithmName();

		/// <summary>
		/// Reset the hash function and 
		/// clear all used resource.
		/// </summary>
		void Reset();

		/// <summary>
		/// Return the size (in bytes) of the hash value produced 
		/// by this hash function.
		/// </summary>
		/// <returns></returns>
		int GetHashLength();

		/// <summary>
		/// Return the size (in bytes) of the internal 
		/// buffer used by this digest.
		/// </summary>
		/// <returns></returns>
		int GetByteLength();

		/// <summary>
		/// Update the hash value with a single byte.
		/// </summary>
		/// <param name="input">Input byte to be hashed.</param>
		/// <returns></returns>
		void Update(byte input);

		/// <summary>
		/// Update the hash value with a array of bytes.
		/// </summary>
		/// <param name="input">Input byte array to be hashed.</param>
		/// <returns></returns>
		void Update(byte[] input);

		/// <summary>
		/// Update the hash value with a string.
		/// </summary>
		/// <param name="input">Input string to be hashed.</param>
		/// <returns></returns>
		void Update(string input);

		/// <summary>
		/// Update the hash value with a array of bytes.
		/// </summary>
		/// <param name="input">Input array of bytes to be hashed.</param>
		/// <param name="start_index">Offset where the data starts.</param>
		/// <param name="length">Length of data.</param>
		/// <returns></returns>
		void Update(byte[] input, int start_index, int length);

		/// <summary>
		/// Copy final hash value and reset the hash function.
		/// </summary>
		/// <param name="output">Byte array the hash value is to be copied into.</param>
		/// <returns></returns>
		int DoFinal(byte[] output);

		/// <summary>
		/// Copy final hash value and reset the hash function.
		/// </summary>
		/// <param name="output">Byte array the hash value is to be copied into.</param>
		/// <param name="start_index">Offset into the output array the hash value is to start at.</param>
		/// <returns></returns>
		int DoFinal(byte[] output, int start_index);
		
		/// <summary>
		/// Computes the hash value for the specified byte array.
		/// </summary>
		/// <param name="input">The input to compute the hash value for.</param>
		/// <returns></returns>
		byte[] ComputeHash(byte[] input);

		/// <summary>
		/// Computes the hash value for the specified string.
		/// </summary>
		/// <param name="input">The input to compute the hash value for.</param>
		/// <returns></returns>
		byte[] ComputeHash(string input);

		/// <summary>
		/// Computes the hash value for the specified byte array.
		/// </summary>
		/// <param name="input">The input to compute the hash value for.</param>
		/// <param name="start_index">Offset where the data starts.</param>
		/// <param name="length">Length of data.</param>
		/// <returns></returns>
		byte[] ComputeHash(byte[] input, int start_index, int length);

		/// <summary>
		/// Create clone of current object.
		/// </summary>
		/// <typeparam name="T">Type of the object.</typeparam>
		/// <returns></returns>
		IHash Clone();
	}
}
