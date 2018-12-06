using System;
using System.Text;
using System.Windows.Forms;

using QPass.Core.Utilities.Extension;

namespace QPass
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new QPass.Forms.QPassMainForm());
		}
	}
}
