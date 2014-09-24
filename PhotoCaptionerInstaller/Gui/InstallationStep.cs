using System;
using System.IO;
using System.Windows.Forms;
using SharpSetup.Base;
using SharpSetup.Prerequisites.Base;
using SharpSetup.UI.Forms.Modern;
using Gui.Properties;

namespace Gui
{
	[System.ComponentModel.ToolboxItem(false)]
	public partial class InstallationStep : ModernActionStep
	{
		InstallationMode mode;
		public InstallationStep(InstallationMode mode)
		{
			InitializeComponent();
			this.mode = mode;
		}

		private void InstallationStep_Entered(object sender, EventArgs e)
		{
			ipProgress.StartListening();
			try
			{
				if (mode == InstallationMode.Uninstall)
				{
					MsiConnection.Instance.Uninstall();
					/*
					try
					{
						MsiConnection.Instance.Open(new Guid("{582b72de-335b-4186-92ae-ca6e07373c7f}"), false);
						MsiConnection.Instance.Uninstall();
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "Uninstall");
					}
					*/
					if (File.Exists(Resources.MainMsiFile))
						MsiConnection.Instance.Open(Resources.MainMsiFile, true);
				}
				else if (mode == InstallationMode.Install)
				{
					PrerequisiteManager.Instance.Install();
					/*
					MsiConnection.Instance.SaveAs("MainInstall");
					MsiConnection.Instance.EnableSettingsChanged = false;
					MsiConnection.Instance.Open("other.msi", false);
					MsiConnection.Instance.Install("");
					MsiConnection.Instance.OpenSaved("MainInstall");
					*/
					MsiConnection.Instance.Install();
				}
				else
					MessageBox.Show("Unknown mode");
			}
			catch (MsiException mex)
			{
				if (mex.ErrorCode != (uint)InstallError.UserExit)
					MessageBox.Show("Installation failed: " + mex.Message);
				Wizard.Finish();
			}
			ipProgress.StopListening();
			Wizard.NextStep();
		}

		public override bool CanClose()
		{
			return false;
		}
	}
}
