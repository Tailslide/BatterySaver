using BatterySaver.UI.Controls;

namespace BatterySaver.UI
{
   partial class ConfigurationEditorForm
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose( bool disposing )
      {
         if ( disposing && ( components != null ) )
         {
            components.Dispose();
         }
         base.Dispose( disposing );
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
            this.components = new System.ComponentModel.Container();
            this.profileListGroupBox = new System.Windows.Forms.GroupBox();
            this.profileListTreeView = new BatterySaver.UI.Controls.ProfileListTreeView();
            this.profileActionsGroupBox = new System.Windows.Forms.GroupBox();
            this.profileEventTreeView = new BatterySaver.UI.Controls.ProfileEventTreeView();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.chkLogToFile = new System.Windows.Forms.CheckBox();
            this.profileListGroupBox.SuspendLayout();
            this.profileActionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // profileListGroupBox
            // 
            this.profileListGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.profileListGroupBox.Controls.Add(this.profileListTreeView);
            this.profileListGroupBox.Location = new System.Drawing.Point(16, 15);
            this.profileListGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.profileListGroupBox.Name = "profileListGroupBox";
            this.profileListGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.profileListGroupBox.Size = new System.Drawing.Size(200, 432);
            this.profileListGroupBox.TabIndex = 1;
            this.profileListGroupBox.TabStop = false;
            this.profileListGroupBox.Text = "Profiles";
            // 
            // profileListTreeView
            // 
            this.profileListTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.profileListTreeView.ImageIndex = 0;
            this.profileListTreeView.LabelEdit = true;
            this.profileListTreeView.Location = new System.Drawing.Point(4, 20);
            this.profileListTreeView.Margin = new System.Windows.Forms.Padding(4);
            this.profileListTreeView.MinimumSize = new System.Drawing.Size(132, 122);
            this.profileListTreeView.Name = "profileListTreeView";
            this.profileListTreeView.ProfileList = null;
            this.profileListTreeView.SelectedImageIndex = 0;
            this.profileListTreeView.ShowLines = false;
            this.profileListTreeView.ShowRootLines = false;
            this.profileListTreeView.Size = new System.Drawing.Size(187, 404);
            this.profileListTreeView.TabIndex = 0;
            // 
            // profileActionsGroupBox
            // 
            this.profileActionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.profileActionsGroupBox.Controls.Add(this.profileEventTreeView);
            this.profileActionsGroupBox.Location = new System.Drawing.Point(224, 15);
            this.profileActionsGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.profileActionsGroupBox.Name = "profileActionsGroupBox";
            this.profileActionsGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.profileActionsGroupBox.Size = new System.Drawing.Size(552, 432);
            this.profileActionsGroupBox.TabIndex = 2;
            this.profileActionsGroupBox.TabStop = false;
            this.profileActionsGroupBox.Text = "Profile Actions";
            // 
            // profileEventTreeView
            // 
            this.profileEventTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.profileEventTreeView.ImageIndex = 0;
            this.profileEventTreeView.LabelEdit = true;
            this.profileEventTreeView.Location = new System.Drawing.Point(8, 23);
            this.profileEventTreeView.Margin = new System.Windows.Forms.Padding(4);
            this.profileEventTreeView.Name = "profileEventTreeView";
            this.profileEventTreeView.SelectedImageIndex = 0;
            this.profileEventTreeView.Size = new System.Drawing.Size(535, 400);
            this.profileEventTreeView.TabIndex = 0;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(560, 450);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(100, 28);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "&Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(668, 450);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 28);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // chkLogToFile
            // 
            this.chkLogToFile.AutoSize = true;
            this.chkLogToFile.Location = new System.Drawing.Point(16, 455);
            this.chkLogToFile.Name = "chkLogToFile";
            this.chkLogToFile.Size = new System.Drawing.Size(101, 21);
            this.chkLogToFile.TabIndex = 5;
            this.chkLogToFile.Text = "Log To File";
            this.chkLogToFile.UseVisualStyleBackColor = true;
            this.chkLogToFile.CheckedChanged += new System.EventHandler(this.chkLogToFile_CheckedChanged);
            // 
            // ConfigurationEditorForm
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(792, 494);
            this.Controls.Add(this.chkLogToFile);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.profileActionsGroupBox);
            this.Controls.Add(this.profileListGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(794, 297);
            this.Name = "ConfigurationEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConfigurationEditorForm";
            this.profileListGroupBox.ResumeLayout(false);
            this.profileActionsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion

      private ProfileListTreeView profileListTreeView;
      private System.Windows.Forms.GroupBox profileListGroupBox;
      private System.Windows.Forms.GroupBox profileActionsGroupBox;
      private ProfileEventTreeView profileEventTreeView;
      private System.Windows.Forms.Button saveButton;
      private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox chkLogToFile;
    }
}