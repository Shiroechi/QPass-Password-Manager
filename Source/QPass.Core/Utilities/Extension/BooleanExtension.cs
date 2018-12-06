namespace QPass.Core.Utilities.Extension
{
	/// <summary>
	/// Boolean Extension.
	/// </summary>
	public static class BooleanExtension
	{
		/// <summary>
		/// Check if a specific value is a number.
		/// </summary>
		/// <typeparam name="Template">The Type of value.</typeparam>
		/// <param name="value">Value to check.</param>
		/// <returns>
		/// <c>true</c> if the value is a number; 
		/// otherwise, <c>false</c>.
		/// </returns>
		public static bool IsNumeric<Template>(this Template value)
		{
			if (value is sbyte)
			{
				return true;
			}
			if (value is byte)
			{
				return true;
			}
			if (value is short)
			{
				return true;
			}
			if (value is ushort)
			{
				return true;
			}
			if (value is int)
			{
				return true;
			}
			if (value is uint)
			{
				return true;
			}
			if (value is long)
			{
				return true;
			}
			if (value is ulong)
			{
				return true;
			}
			if (value is float)
			{
				return true;
			}
			if (value is double)
			{
				return true;
			}
			if (value is decimal)
			{
				return true;
			}

			sbyte sbytes;
			if (sbyte.TryParse(value.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out sbytes))
			{
				return true;
			}

			byte bytes;
			if (byte.TryParse(value.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out bytes))
			{
				return true;
			}

			short shorts;
			if (short.TryParse(value.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out shorts))
			{
				return true;
			}

			ushort ushorts;
			if (ushort.TryParse(value.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out ushorts))
			{
				return true;
			}

			int ints;
			if (int.TryParse(value.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out ints))
			{
				return true;
			}

			uint uints;
			if (uint.TryParse(value.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out uints))
			{
				return true;
			}

			long longs;
			if (long.TryParse(value.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out longs))
			{
				return true;
			}

			ulong ulongs;
			if (ulong.TryParse(value.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out ulongs))
			{
				return true;
			}

			float floats;
			if (float.TryParse(value.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out floats))
			{
				return true;
			}

			double doubles;
			if (double.TryParse(value.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out doubles))
			{
				return true;
			}

			decimal decimals;
			if (decimal.TryParse(value.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out decimals))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Check if a specific value is a alphabeth.
		/// </summary>
		/// <typeparam name="Template">The Type of value.</typeparam>
		/// <param name="value">Value to check.</param>
		/// <returns>
		/// <c>true</c> if the value is a number; 
		/// otherwise, <c>false</c>.
		/// </returns>
		public static bool IsAlphabetic<Template>(this Template value)
		{
			string line = value.ToString();
			for (var i = 0; i < line.Length; i++)
			{
				if (char.IsLetter(line[i]) == false)
				{
					return false;
				}
			}
			return true;
		}
	}
}
