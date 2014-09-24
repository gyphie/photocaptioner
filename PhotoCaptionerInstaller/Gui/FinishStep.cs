using System;
using System.Diagnostics;
using SharpSetup.Base;
using SharpSetup.Prerequisites.Base;
using SharpSetup.UI.Forms.Modern;

namespace Gui
{
	[System.ComponentModel.ToolboxItem(false)]
	public partial class FinishStep : ModernInfoStep
	{
		public FinishStep()
		{
			InitializeComponent();
		}

		private void FinishStep_Entered(object sender, EventArgs e)
		{
			Wizard.BackButton.Enabled = false;
		}

		private void FinishStep_Finish(object sender, ChangeStepEventArgs e)
		{
			if (cbRunNow.Checked)
				Process.Start(string.Format(Gui.Properties.Resources.FinishStepCommand, MsiConnection.Instance.GetPath("INSTALLLOCATION")));
			if (cbRestartNow.Checked)
				SetupHelper.Restart(this, RestartOptions.Schedule | RestartOptions.NoAsk);
		}

		private void FinishStep_Entering(object sender, ChangeStepEventArgs e)
		{
			cbRunNow.Visible = cbRunNow.Checked = Globals.GetVariable<bool>("AllowRunOnFinish") && !MsiConnection.Instance.RebootRequired;
			cbRestartNow.Visible = cbRestartNow.Checked = (MsiConnection.Instance.RebootRequired || PrerequisiteManager.Instance.GetProperty(StandardProperties.RebootRequired, false)) && !SetupHelper.GetCommandLineOption("sssp.norestart", false);
		}
	}
}
