using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Ookii.Dialogs;
using PhotoCaptioner.Properties;

namespace PhotoCaptioner
{
	public partial class frmMain : Form
	{
		private VistaFolderBrowserDialog dlgVistaFolder;

		public frmMain()
		{
			InitializeComponent();

			this.dlgVistaFolder = new VistaFolderBrowserDialog();

			this.lblWorking.Text = "";
			this.LoadSettings();
			
		}

		public void LoadSettings()
		{
			this.txtPhotoPath.Text = Settings.Default.PhotoPath;
			this.txtSaveToPath.Text = Settings.Default.OutputPath;
			this.fontChooser.Font = Settings.Default.Font;
			this.lblFontName.Text = Settings.Default.Font.Name;
			this.btnFontColor.BackColor = Settings.Default.FontColor;
			this.btnBackgroundColor.BackColor = Settings.Default.BackgroundColor;
			this.comboSize.SelectedItem = Settings.Default.Size;
			this.comboPosition.SelectedItem = Settings.Default.Position;
			this.comboWrapping.SelectedItem = Settings.Default.Wrapping;
		}

		public void SaveSettings()
		{
			Settings.Default.Save();
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			this.CleanlyExitApp();
		}


		private void btnPhotoFolderChooser_Click(object sender, EventArgs e)
		{
			string newPath = this.ImageFolderChooser(this.txtPhotoPath, Settings.Default.PhotoPath);
			if (!string.IsNullOrEmpty(newPath))
			{
				Settings.Default.PhotoPath = newPath;
			}
		}
		private void btnSaveToFolderChooser_Click(object sender, EventArgs e)
		{
			string newPath = this.ImageFolderChooser(this.txtSaveToPath, Settings.Default.OutputPath);
			if (!string.IsNullOrEmpty(newPath))
			{
				Settings.Default.OutputPath = newPath;
			}
		}


		private string ImageFolderChooser(TextBox targetTextBox, string defaultPath)
		{
			this.dlgVistaFolder.Description = "Photos";
			this.dlgVistaFolder.ShowNewFolderButton = true;
			this.dlgVistaFolder.RootFolder = Environment.SpecialFolder.MyPictures;
			string imageFolder = targetTextBox.Text.Trim();

			if (!string.IsNullOrEmpty(imageFolder) && Directory.Exists(imageFolder))
			{
				this.dlgVistaFolder.SelectedPath = imageFolder;
			}
			else if (!string.IsNullOrEmpty(Settings.Default.PhotoPath) && Directory.Exists(Settings.Default.PhotoPath))
			{
				this.dlgVistaFolder.SelectedPath = defaultPath;
			}
			else
			{
				this.dlgVistaFolder.SelectedPath = Environment.GetFolderPath(this.dlgVistaFolder.RootFolder);
			}

			this.dlgVistaFolder.SelectedPath = this.dlgVistaFolder.SelectedPath.TrimEnd('\\') + "\\";

			if (this.dlgVistaFolder.ShowDialog() == DialogResult.OK)
			{
				return (targetTextBox.Text = this.dlgVistaFolder.SelectedPath);
			}

			return string.Empty;
		}

		
		public void ProcessPhotos()
		{
			if (!this.bwProcessor.IsBusy)
			{
				this.btnBegin.Enabled = false; 
				this.bwProcessor.RunWorkerAsync();
				this.lblWorking.Text = "Starting";
			}
		}

		private void ProcessProgress(decimal percentComplete, PhotoProcessResult result)
		{
			this.bwProcessor.ReportProgress(Convert.ToInt32(percentComplete), result);
		}

		private void bwWallPaper_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				PhotoProcessor.OnProgress += ProcessProgress;
				PhotoProcessor.ProcessPhotos(
					Settings.Default.PhotoPath,
					Settings.Default.OutputPath,
					new System.Windows.Media.FontFamily(Settings.Default.Font.FontFamily.Name),
					System.Windows.Media.Color.FromRgb(Settings.Default.FontColor.R, Settings.Default.FontColor.G, Settings.Default.FontColor.B),
					System.Windows.Media.Color.FromRgb(Settings.Default.BackgroundColor.R, Settings.Default.BackgroundColor.G, Settings.Default.BackgroundColor.B),
					Settings.Default.Size,
					Settings.Default.Position,
					Settings.Default.Wrapping
				);
			}
			catch (Exception ex)
			{
				PhotoProcessor.OnProgress(0, new PhotoProcessResult() { FileName = "Error", Message = ex.Message });
				NonBlockingConsole.WriteLine(ex.Message);
			}
			finally
			{
				PhotoProcessor.OnProgress -= ProcessProgress;
			}
		}


		private void bwProcessor_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			PhotoProcessResult result = e.UserState as PhotoProcessResult;
			string message = "";
			if (result != null)
			{
				message = result.Message;
				if (result.Success)
				{
					this.lbSuccess.Items.Add(result.ToString());
				}
				else
				{
					this.lbError.Items.Add(result.ToString());
				}
			}


			this.lblWorking.Text = string.Format("Working...{0}%", e.ProgressPercentage.ToString());
			Program.DebugMessage("Progress Update: {0}% {1}", e.ProgressPercentage.ToString(), message);
		}


		private void bwWallPaper_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.btnBegin.Enabled = true;
			this.lblWorking.Text = "Done";
		}

		private void btnBegin_Click(object sender, EventArgs e)
		{
			this.lbSuccess.Items.Clear();
			this.lbError.Items.Clear();
			
			this.ProcessPhotos();
			Program.DebugMessage("Started processing.");
		}
		
		private void CleanlyExitApp()
		{
			this.SaveSettings();
			Application.Exit();
		}

		private void btnFontChooser_Click(object sender, EventArgs e)
		{
			if (this.fontChooser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Settings.Default.Font = this.fontChooser.Font;
				this.lblFontName.Text = Settings.Default.Font.Name;
			}
		}

		private void btnFontColor_Click(object sender, EventArgs e)
		{
			if (this.colorChooser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.btnFontColor.BackColor = this.colorChooser.Color;
				Settings.Default.FontColor = this.colorChooser.Color;
			}
		}

		private void btnBackgroundColor_Click(object sender, EventArgs e)
		{
			if (this.colorChooser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.btnBackgroundColor.BackColor = this.colorChooser.Color;
				Settings.Default.BackgroundColor = this.colorChooser.Color;
			}
		}

		private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.CleanlyExitApp();
		}

		private void listBox_DoubleClick(object sender, EventArgs e)
		{
			ListBox lb = sender as ListBox;
			MessageBox.Show(lb.SelectedItem.ToString(), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void comboPosition_SelectedIndexChanged(object sender, EventArgs e)
		{
			Settings.Default.Position = comboPosition.SelectedItem as string;
		}

		private void comboSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			Settings.Default.Size = comboSize.SelectedItem as string;
		}

		private void comboWrapping_SelectedIndexChanged(object sender, EventArgs e)
		{
			Settings.Default.Wrapping = comboWrapping.SelectedItem as string;
		}
	}
}
