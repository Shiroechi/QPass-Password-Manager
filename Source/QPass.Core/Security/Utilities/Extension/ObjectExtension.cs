using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Akashic.Security.Hash;
using Akashic.Utilities.Convert;

namespace Akashic.Utilities.Extension
{
	/// <summary>
	/// Object Extension.
	/// </summary>
	public static class ObjectExtension
	{
		/// <summary>
		/// Get object HashCode.
		/// Hash with SHA-1 Algortihm.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string HashCode(this object obj)
		{
			byte[] data = Objects.Serialize(obj);
			SHA1 sha = new SHA1();
			return sha.ComputeHash(data).EncodeBase16();
		}

		/// <summary>
		/// Get object HashCode.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string HashCode(this object obj, IHash hash)
		{
			byte[] data = Objects.Serialize(obj);
			hash.Update(data, 0, data.Length);
			data = new byte[hash.GetHashLength()];
			hash.DoFinal(data, 0);
			return data.EncodeBase16();
		}

		/// <summary>
		/// Get object address in RAM.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static long GetObjectAddress(this object obj)
		{
			System.Runtime.InteropServices.GCHandle handle = System.Runtime.InteropServices.GCHandle.Alloc(obj, System.Runtime.InteropServices.GCHandleType.WeakTrackResurrection);
			long address = System.Runtime.InteropServices.GCHandle.ToIntPtr(handle).ToInt64();
			handle.Free();
			return address;
		}

		/// <summary>
		/// Get object address in RAM.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string GetObjectAddressString(this object obj)
		{
			System.Runtime.InteropServices.GCHandle handle = System.Runtime.InteropServices.GCHandle.Alloc(obj, System.Runtime.InteropServices.GCHandleType.WeakTrackResurrection);
			string address = System.Runtime.InteropServices.GCHandle.ToIntPtr(handle).ToString();
			handle.Free();
			return address;
		}

		/// <summary>
		/// Perform a deep Copy of the object.
		/// </summary>
		/// <typeparam name="T">The type of object being copied.</typeparam>
		/// <param name="source">The object instance to copy.</param>
		/// <returns>The copied object.</returns>
		public static T Clone<T>(this T source)
		{
			if (!typeof(T).IsSerializable)
			{
				throw new ArgumentException("The type must be serializable.", "source");
			}

			// Don't serialize a null object, simply return the default for that object
			if (Object.ReferenceEquals(source, null))
			{
				return default(T);
			}
			
			IFormatter formatter = new BinaryFormatter();
			using (Stream stream = new MemoryStream())
			{
				formatter.Serialize(stream, source);
				stream.Seek(0, SeekOrigin.Begin);
				return (T)formatter.Deserialize(stream);
			}
		}

		public static void Copy<T>(this T source, T other)
		{
			source = other.Clone();
		}

		/// <summary>
		/// Check is the object is serializable.
		/// </summary>
		/// <param name="obj">Object to check.</param>
		/// <returns></returns>
		public static bool IsSerializable(this object obj)
		{
			if (obj == null)
			{
				return false;
			}

			Type type = obj.GetType();
			return type.IsSerializable;
        	}
	}
}
