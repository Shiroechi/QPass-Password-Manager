namespace QPass.Core.Security.Hash
{
	/// <summary>
	/// With FIPS PUB 202 a new kind of message digest was 
	/// announced which supported extendable output, 
	/// or variable hash value sizes.
	/// 
	/// This interface provides the extra method required to 
	/// support variable output on a hash implementation.
	/// </summary>
	public interface IHashExtend : IHash
	{
		/// <summary>
		/// Output the results of the final calculation 
		/// for this digest to outLen number of bytes.
		/// </summary>
		/// <param name="output">Byte array the hash value is to be copied into.</param>
		/// <param name="start_index">Offset into the output array the hash value is to start at.</param>
		/// <param name="length">Length of bytes requested.</param>
		/// <returns>the number of bytes written.</returns>
		int DoFinal(byte[] output, int start_index, int length);
	}
}
