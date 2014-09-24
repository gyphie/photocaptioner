using System;
using System.Windows.Forms;
using SharpSetup.Base;

namespace Gui
{
	static class SetupProgram
	{
		/// <summary>
		/// The main entry point for the installer.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			SetupHelper.Initialize(args);
			SetupHelper.Install += new EventHandler<EventArgs>(SetupHelper_Install);
			//SetupHelper.SilentInstall += new EventHandler<EventArgs>(SetupHelper_SilentInstall);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			SetupHelper.StartInstallation();
		}

		/*
		static void SetupHelper_SilentInstall(object sender, EventArgs e)
		{
			MsiConnection.Instance.Open(Gui.Properties.Resources.MainMsiFile, true);
			switch (SetupHelper.InstallationModeFromCommandLine)
			{
				case InstallationMode.None:
				case InstallationMode.Install:
					MsiConnection.Instance.Install();
					break;
				case InstallationMode.Uninstall:
					MsiConnection.Instance.Uninstall();
					break;
				case InstallationMode.Reinstall:
				case InstallationMode.Upgrade:
					MsiConnection.Instance.Uninstall();
					MsiConnection.Instance.Open(Gui.Properties.Resources.MainMsiFile, true);
					MsiConnection.Instance.Install();
					break;
				default:
					throw new ArgumentException("Mode not supported: " + SetupHelper.InstallationModeFromCommandLine);
			}
		}
		*/

		static void SetupHelper_Install(object sender, EventArgs e)
		{
			Application.Run(new Gui.SetupWizard());
		}
	}
}
