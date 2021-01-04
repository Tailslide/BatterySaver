namespace BatterySaver.UI
{
   partial class ProfileEditorForm
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
         this.profileNameTextBox = new System.Windows.Forms.TextBox();
         this.profileNameLabel = new System.Windows.Forms.Label();
         this.saveButton = new System.Windows.Forms.Button();
         this.cancelButton = new System.Windows.Forms.Button();
         this.isDefaultCheckBox = new System.Windows.Forms.CheckBox();
         this.SuspendLayout();
         // 
         // profileNameTextBox
         // 
         this.profileNameTextBox.Location = new System.Drawing.Point( 88, 6 );
         this.profileNameTextBox.Name = "profileNameTextBox";
         this.profileNameTextBox.Size = new System.Drawing.Size( 217, 20 );
         this.profileNameTextBox.TabIndex = 0;
         this.profileNameTextBox.Validating += new System.ComponentModel.CancelEventHandler( this.ProfileNameTextBoxValidating );
         // 
         // profileNameLabel
         // 
         this.profileNameLabel.AutoSize = true;
         this.profileNameLabel.Location = new System.Drawing.Point( 12, 9 );
         this.profileNameLabel.Name = "profileNameLabel";
         this.profileNameLabel.Size = new System.Drawing.Size( 70, 13 );
         this.profileNameLabel.TabIndex = 1;
         this.profileNameLabel.Text = "Profile Name:";
         // 
         // saveButton
         // 
         this.saveButton.Location = new System.Drawing.Point( 149, 59 );
         this.saveButton.Name = "saveButton";
         this.saveButton.Size = new System.Drawing.Size( 75, 23 );
         this.saveButton.TabIndex = 2;
         this.saveButton.Text = "&Save";
         this.saveButton.UseVisualStyleBackColor = true;
         this.saveButton.Click += new System.EventHandler( this.SaveButtonClick );
         // 
         // cancelButton
         // 
         this.cancelButton.CausesValidation = false;
         this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.cancelButton.Location = new System.Drawing.Point( 230, 59 );
         this.cancelButton.Name = "cancelButton";
         this.cancelButton.Size = new System.Drawing.Size( 75, 23 );
         this.cancelButton.TabIndex = 3;
         this.cancelButton.Text = "&Cancel";
         this.cancelButton.UseVisualStyleBackColor = true;
         this.cancelButton.Click += new System.EventHandler( this.CancelButtonClick );
         // 
         // isDefaultCheckBox
         // 
         this.isDefaultCheckBox.AutoSize = true;
         this.isDefaultCheckBox.Location = new System.Drawing.Point( 88, 33 );
         this.isDefaultCheckBox.Name = "isDefaultCheckBox";
         this.isDefaultCheckBox.Size = new System.Drawing.Size( 140, 17 );
         this.isDefaultCheckBox.TabIndex = 4;
         this.isDefaultCheckBox.Text = "This is the default profile";
         this.isDefaultCheckBox.UseVisualStyleBackColor = true;
         // 
         // ProfileEditorForm
         // 
         this.AcceptButton = this.saveButton;
         this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.cancelButton;
         this.ClientSize = new System.Drawing.Size( 317, 94 );
         this.Controls.Add( this.isDefaultCheckBox );
         this.Controls.Add( this.cancelButton );
         this.Controls.Add( this.saveButton );
         this.Controls.Add( this.profileNameLabel );
         this.Controls.Add( this.profileNameTextBox );
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "ProfileEditorForm";
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "ProfileEditorForm";
         this.ResumeLayout( false );
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox profileNameTextBox;
      private System.Windows.Forms.Label profileNameLabel;
      private System.Windows.Forms.Button saveButton;
      private System.Windows.Forms.Button cancelButton;
      private System.Windows.Forms.CheckBox isDefaultCheckBox;
   }
}