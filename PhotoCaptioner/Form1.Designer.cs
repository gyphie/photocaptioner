namespace PhotoCaptioner
{
	partial class frmMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.btnBegin = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.txtPhotoPath = new System.Windows.Forms.TextBox();
			this.btnPhotoFolderChooser = new System.Windows.Forms.Button();
			this.bwProcessor = new System.ComponentModel.BackgroundWorker();
			this.lblWorking = new System.Windows.Forms.Label();
			this.lblPhotoFolder = new System.Windows.Forms.Label();
			this.labSaveToPath = new System.Windows.Forms.Label();
			this.txtSaveToPath = new System.Windows.Forms.TextBox();
			this.btnSaveToFolderChooser = new System.Windows.Forms.Button();
			this.lbSuccess = new System.Windows.Forms.ListBox();
			this.lbError = new System.Windows.Forms.ListBox();
			this.labFontColor = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnFontColor = new System.Windows.Forms.Button();
			this.btnBackgroundColor = new System.Windows.Forms.Button();
			this.labComplete = new System.Windows.Forms.Label();
			this.labError = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnFontChooser = new System.Windows.Forms.Button();
			this.fontChooser = new System.Windows.Forms.FontDialog();
			this.colorChooser = new System.Windows.Forms.ColorDialog();
			this.lblFontName = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnBegin
			// 
			this.btnBegin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnBegin.Location = new System.Drawing.Point(15, 337);
			this.btnBegin.Name = "btnBegin";
			this.btnBegin.Size = new System.Drawing.Size(75, 23);
			this.btnBegin.TabIndex = 7;
			this.btnBegin.Text = "&Begin";
			this.btnBegin.UseVisualStyleBackColor = true;
			this.btnBegin.Click += new System.EventHandler(this.btnBegin_Click);
			// 
			// btnExit
			// 
			this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnExit.Location = new System.Drawing.Point(548, 337);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(75, 23);
			this.btnExit.TabIndex = 9;
			this.btnExit.Text = "E&xit";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// txtPhotoPath
			// 
			this.txtPhotoPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPhotoPath.Location = new System.Drawing.Point(88, 105);
			this.txtPhotoPath.Name = "txtPhotoPath";
			this.txtPhotoPath.ReadOnly = true;
			this.txtPhotoPath.Size = new System.Drawing.Size(491, 20);
			this.txtPhotoPath.TabIndex = 1;
			// 
			// btnPhotoFolderChooser
			// 
			this.btnPhotoFolderChooser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPhotoFolderChooser.Location = new System.Drawing.Point(585, 104);
			this.btnPhotoFolderChooser.Name = "btnPhotoFolderChooser";
			this.btnPhotoFolderChooser.Size = new System.Drawing.Size(38, 20);
			this.btnPhotoFolderChooser.TabIndex = 2;
			this.btnPhotoFolderChooser.Text = "...";
			this.btnPhotoFolderChooser.UseVisualStyleBackColor = true;
			this.btnPhotoFolderChooser.Click += new System.EventHandler(this.btnPhotoFolderChooser_Click);
			// 
			// bwProcessor
			// 
			this.bwProcessor.WorkerReportsProgress = true;
			this.bwProcessor.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwWallPaper_DoWork);
			this.bwProcessor.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwProcessor_ProgressChanged);
			this.bwProcessor.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwWallPaper_RunWorkerCompleted);
			// 
			// lblWorking
			// 
			this.lblWorking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblWorking.AutoSize = true;
			this.lblWorking.Location = new System.Drawing.Point(96, 342);
			this.lblWorking.Name = "lblWorking";
			this.lblWorking.Size = new System.Drawing.Size(56, 13);
			this.lblWorking.TabIndex = 10;
			this.lblWorking.Text = "Working...";
			// 
			// lblPhotoFolder
			// 
			this.lblPhotoFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblPhotoFolder.AutoSize = true;
			this.lblPhotoFolder.Location = new System.Drawing.Point(12, 108);
			this.lblPhotoFolder.Name = "lblPhotoFolder";
			this.lblPhotoFolder.Size = new System.Drawing.Size(70, 13);
			this.lblPhotoFolder.TabIndex = 0;
			this.lblPhotoFolder.Text = "&Photo Folder:";
			// 
			// labSaveToPath
			// 
			this.labSaveToPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labSaveToPath.AutoSize = true;
			this.labSaveToPath.Location = new System.Drawing.Point(31, 133);
			this.labSaveToPath.Name = "labSaveToPath";
			this.labSaveToPath.Size = new System.Drawing.Size(51, 13);
			this.labSaveToPath.TabIndex = 0;
			this.labSaveToPath.Text = "&Save To:";
			// 
			// txtSaveToPath
			// 
			this.txtSaveToPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSaveToPath.Location = new System.Drawing.Point(88, 130);
			this.txtSaveToPath.Name = "txtSaveToPath";
			this.txtSaveToPath.ReadOnly = true;
			this.txtSaveToPath.Size = new System.Drawing.Size(491, 20);
			this.txtSaveToPath.TabIndex = 1;
			// 
			// btnSaveToFolderChooser
			// 
			this.btnSaveToFolderChooser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveToFolderChooser.Location = new System.Drawing.Point(585, 130);
			this.btnSaveToFolderChooser.Name = "btnSaveToFolderChooser";
			this.btnSaveToFolderChooser.Size = new System.Drawing.Size(38, 20);
			this.btnSaveToFolderChooser.TabIndex = 2;
			this.btnSaveToFolderChooser.Text = "...";
			this.btnSaveToFolderChooser.UseVisualStyleBackColor = true;
			this.btnSaveToFolderChooser.Click += new System.EventHandler(this.btnSaveToFolderChooser_Click);
			// 
			// lbSuccess
			// 
			this.lbSuccess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbSuccess.FormattingEnabled = true;
			this.lbSuccess.Location = new System.Drawing.Point(15, 179);
			this.lbSuccess.Name = "lbSuccess";
			this.lbSuccess.Size = new System.Drawing.Size(300, 147);
			this.lbSuccess.TabIndex = 11;
			this.lbSuccess.DoubleClick += new System.EventHandler(this.listBox_DoubleClick);
			// 
			// lbError
			// 
			this.lbError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbError.FormattingEnabled = true;
			this.lbError.Location = new System.Drawing.Point(321, 179);
			this.lbError.Name = "lbError";
			this.lbError.Size = new System.Drawing.Size(300, 147);
			this.lbError.TabIndex = 11;
			this.lbError.DoubleClick += new System.EventHandler(this.listBox_DoubleClick);
			// 
			// labFontColor
			// 
			this.labFontColor.AutoSize = true;
			this.labFontColor.Location = new System.Drawing.Point(49, 18);
			this.labFontColor.Name = "labFontColor";
			this.labFontColor.Size = new System.Drawing.Size(58, 13);
			this.labFontColor.TabIndex = 12;
			this.labFontColor.Text = "Font Color:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(95, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "Background Color:";
			// 
			// btnFontColor
			// 
			this.btnFontColor.Location = new System.Drawing.Point(113, 12);
			this.btnFontColor.Name = "btnFontColor";
			this.btnFontColor.Size = new System.Drawing.Size(36, 23);
			this.btnFontColor.TabIndex = 13;
			this.btnFontColor.UseVisualStyleBackColor = true;
			this.btnFontColor.Click += new System.EventHandler(this.btnFontColor_Click);
			// 
			// btnBackgroundColor
			// 
			this.btnBackgroundColor.Location = new System.Drawing.Point(113, 39);
			this.btnBackgroundColor.Name = "btnBackgroundColor";
			this.btnBackgroundColor.Size = new System.Drawing.Size(36, 23);
			this.btnBackgroundColor.TabIndex = 13;
			this.btnBackgroundColor.UseVisualStyleBackColor = true;
			this.btnBackgroundColor.Click += new System.EventHandler(this.btnBackgroundColor_Click);
			// 
			// labComplete
			// 
			this.labComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labComplete.AutoSize = true;
			this.labComplete.Location = new System.Drawing.Point(15, 160);
			this.labComplete.Name = "labComplete";
			this.labComplete.Size = new System.Drawing.Size(51, 13);
			this.labComplete.TabIndex = 16;
			this.labComplete.Text = "Complete";
			// 
			// labError
			// 
			this.labError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labError.AutoSize = true;
			this.labError.Location = new System.Drawing.Point(318, 160);
			this.labError.Name = "labError";
			this.labError.Size = new System.Drawing.Size(29, 13);
			this.labError.TabIndex = 16;
			this.labError.Text = "Error";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(206, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(31, 13);
			this.label1.TabIndex = 14;
			this.label1.Text = "Font:";
			// 
			// btnFontChooser
			// 
			this.btnFontChooser.Location = new System.Drawing.Point(243, 14);
			this.btnFontChooser.Name = "btnFontChooser";
			this.btnFontChooser.Size = new System.Drawing.Size(36, 19);
			this.btnFontChooser.TabIndex = 13;
			this.btnFontChooser.Text = "...";
			this.btnFontChooser.UseVisualStyleBackColor = true;
			this.btnFontChooser.Click += new System.EventHandler(this.btnFontChooser_Click);
			// 
			// fontChooser
			// 
			this.fontChooser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.fontChooser.FontMustExist = true;
			// 
			// lblFontName
			// 
			this.lblFontName.AutoSize = true;
			this.lblFontName.Location = new System.Drawing.Point(285, 18);
			this.lblFontName.Name = "lblFontName";
			this.lblFontName.Size = new System.Drawing.Size(0, 13);
			this.lblFontName.TabIndex = 14;
			// 
			// frmMain
			// 
			this.AcceptButton = this.btnBegin;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnExit;
			this.ClientSize = new System.Drawing.Size(635, 372);
			this.Controls.Add(this.labError);
			this.Controls.Add(this.labComplete);
			this.Controls.Add(this.lblFontName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnBackgroundColor);
			this.Controls.Add(this.btnFontChooser);
			this.Controls.Add(this.btnFontColor);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.labFontColor);
			this.Controls.Add(this.lbError);
			this.Controls.Add(this.lbSuccess);
			this.Controls.Add(this.lblWorking);
			this.Controls.Add(this.btnPhotoFolderChooser);
			this.Controls.Add(this.btnSaveToFolderChooser);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.txtPhotoPath);
			this.Controls.Add(this.txtSaveToPath);
			this.Controls.Add(this.btnBegin);
			this.Controls.Add(this.labSaveToPath);
			this.Controls.Add(this.lblPhotoFolder);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmMain";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Photo Captioner";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnBegin;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.TextBox txtPhotoPath;
		private System.Windows.Forms.Button btnPhotoFolderChooser;
		private System.ComponentModel.BackgroundWorker bwProcessor;
		private System.Windows.Forms.Label lblWorking;
		private System.Windows.Forms.Label lblPhotoFolder;
		private System.Windows.Forms.Label labSaveToPath;
		private System.Windows.Forms.TextBox txtSaveToPath;
		private System.Windows.Forms.Button btnSaveToFolderChooser;
		private System.Windows.Forms.ListBox lbSuccess;
		private System.Windows.Forms.ListBox lbError;
		private System.Windows.Forms.Label labFontColor;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnFontColor;
		private System.Windows.Forms.Button btnBackgroundColor;
		private System.Windows.Forms.Label labComplete;
		private System.Windows.Forms.Label labError;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnFontChooser;
		private System.Windows.Forms.FontDialog fontChooser;
		private System.Windows.Forms.ColorDialog colorChooser;
		private System.Windows.Forms.Label lblFontName;
	}
}

