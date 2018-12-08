using QPass.Core.Security.Hash;

namespace QPass.Core.Security.MAC
{
	/// <summary>
	/// The base interface for implementations of 
	/// message authentication codes (MACs).
	/// </summary>
	public interface IMAC
    {
        /// <summary>
        /// Initialise the MAC.
        /// </summary>
        /// <param name="key">key required by the MAC</param>
        void Initialize(byte[] key);

		/// <summary>
		/// Return the name of the algorithm the MAC implements.
		/// </summary>
		string AlgorithmName();

		/// <summary>
		/// Reset the MAC. At the end of resetting the MAC should be in the
		/// in the same state it was after the last init (if there was one).
		/// </summary>
		void Reset();

		/// <summary>
		/// Get used hash funtion in the MAC.
		/// </summary>
		/// <returns></returns>
		IHash GetHashFunction();

		/// <summary>
		/// Return the size (in bytes) of the hash value produced 
		/// by this hash function.
		/// </summary>
		/// <returns></returns>
		int GetHashLength();

		/// <summary>
		/// Return the block size for this MAC (in bytes).
		/// </summary>
		/// <returns></returns>
		//int GetMacSize();

		/// <summary>
		/// Add a single byte to the mac for processing.
		/// </summary>
		/// <param name="input">Byte to be processed.</param>
		//void Update(byte input);

		/// <summary>
		/// Update the HMAC value with a array of bytes.
		/// </summary>
		/// <param name="input">Input byte array to be hashed.</param>
		/// <returns></returns>
		void Update(byte[] input);

		/// <summary>
		/// Update the HMAC value with a string.
		/// </summary>
		/// <param name="input">Input string to be hashed.</param>
		/// <returns></returns>
		void Update(string input);

		/// <summary>
		/// Update the HMAC value with a array of bytes.
		/// </summary>
		/// <param name="input">Input array of bytes to be hashed.</param>
		/// <param name="start_index">Offset where the data starts.</param>
		/// <param name="length">Length of data.</param>
		/// <returns></returns>
		void Update(byte[] input, int start_index, int length);

		/// <summary>
		/// Copy final HMAC value and reset the hash function.
		/// </summary>
		/// <param name="output">Byte array the hash value is to be copied into.</param>
		/// <returns></returns>
		int DoFinal(byte[] output);

		/// <summary>
		/// Copy final HMAC value and reset the hash function.
		/// </summary>
		/// <param name="output">Byte array the hash value is to be copied into.</param>
		/// <param name="start_index">Offset into the output array the hash value is to start at.</param>
		/// <returns></returns>
		int DoFinal(byte[] output, int start_index);

		/// <summary>
		/// Add byte[] to the MAC for processin.
		/// </summary>
		/// <param name="input">byte[] containing the input.</param>
		/// <param name="inOff">index in the array the data begins at.</param>
		/// <param name="len">length of the input starting at inOff.</param>
		//void BlockUpdate(byte[] input, int inOff, int len);
		
		/// <summary>
		/// Computes the HMAC value for the specified byte array.
		/// </summary>
		/// <param name="keys">Secret keys.</param>
		/// <param name="data">Array bytes to be authenticated.</param>
		/// <returns></returns>
		byte[] ComputeHash(byte[] keys, byte[] data);

		/// <summary>
		/// Computes the HMAC value for the specified byte array.
		/// </summary>
		/// <param name="keys">Secret keys.</param>
		/// <param name="data">Array bytes to be authenticated.</param>
		/// <returns></returns>
		byte[] ComputeHash(string keys, string data);

	}
}