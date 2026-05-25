using NSqlTools.Lib;
using NSqlTools.UI.Pages;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using NSqlTools.Types.Properties;

namespace NSqlTools.UI
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// Log sistemini başlat ve test et
			try
			{
				LogHelper.Info("Application starting...");
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				MessageBox.Show(String.Format(CommonResource.LogInitializationError0, ex.Message), CommonResource.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(UIHelper.GetCultureFromRegistry());
			
			try
			{
				Application.Run(new frmMain());
			}
			catch (Exception ex)
			{
				LogHelper.Error("Application crashed", ex);
				MessageBox.Show(String.Format(CommonResource.ApplicationError0, ex.Message), CommonResource.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				LogHelper.Info("Application shutting down");
			}
		}
	}
}

