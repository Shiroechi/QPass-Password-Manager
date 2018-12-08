using System;

namespace QPass.Entities
{
	/// <summary>
	/// Grouping account.
	/// </summary>
	public class Groups
	{
		#region Member

		private ulong _ID;
		private string _Name;
		private string _Description;

		#endregion Member

		#region Constructor & Destructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public Groups()
		{
			this._ID = 0;
			this._Name = "General";
			this._Description = "Default groups";
		}

		/// <summary>
		/// Destructor.
		/// </summary>
		~Groups()
		{
			this._ID = 0;
			this._Name = string.Empty;
			this._Description = string.Empty;
		}

		#endregion Constructor & Destructor

		#region Public

		/// <summary>
		/// Set group id.
		/// </summary>
		/// <param name="id">Group id.</param>
		public void SetID(ulong id)
		{
			if (id <= 0)
			{
				throw new Exception("ID can't lower or equal then zero (0).");
			}

			this._ID = id;
		}

		/// <summary>
		/// Get group id.
		/// </summary>
		/// <returns></returns>
		public ulong GetID()
		{
			return this._ID;
		}

		/// <summary>
		/// Set group name.
		/// </summary>
		/// <param name="name">Group name.</param>
		public void SetName(string name)
		{
			if (name == null || name.Trim() == "")
			{
				name = "";
			}

			this._Name = name;
		}

		/// <summary>
		/// Get group name.
		/// </summary>
		/// <returns></returns>
		public string GetName()
		{
			return this._Name;
		}

		/// <summary>
		/// Set group description.
		/// </summary>
		/// <param name="desc">Group description.</param>
		public void SetDescription(string desc)
		{
			if (desc == null || desc.Trim() == "")
			{
				desc = "";
			}

			this._Description = desc;
		}

		/// <summary>
		/// Get group description.
		/// </summary>
		/// <returns></returns>
		public string GetDescription()
		{
			return this._Description;
		}

		#endregion Public
	}
}
