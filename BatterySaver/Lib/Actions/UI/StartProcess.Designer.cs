namespace BatterySaver.Lib.Actions.UI
{
   partial class StartProcess
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

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.processNameLabel = new System.Windows.Forms.Label();
         this.processNameTextBox = new System.Windows.Forms.TextBox();
         this.workingDirectoryLabel = new System.Windows.Forms.Label();
         this.workingDirectoryTextBox = new System.Windows.Forms.TextBox();
         this.argumentsLabel = new System.Windows.Forms.Label();
         this.argumentsTextBox = new System.Windows.Forms.TextBox();
         this.allowMultipleInstancesCheckBox = new System.Windows.Forms.CheckBox();
         this.actionGroupBox.SuspendLayout();
         this.SuspendLayout();
         // 
         // actionGroupBox
         // 
         this.actionGroupBox.Controls.Add( this.allowMultipleInstancesCheckBox );
         this.actionGroupBox.Controls.Add( this.argumentsLabel );
         this.actionGroupBox.Controls.Add( this.argumentsTextBox );
         this.actionGroupBox.Controls.Add( this.workingDirectoryLabel );
         this.actionGroupBox.Controls.Add( this.workingDirectoryTextBox );
         this.actionGroupBox.Controls.Add( this.processNameLabel );
         this.actionGroupBox.Controls.Add( this.processNameTextBox );
         this.actionGroupBox.Size = new System.Drawing.Size( 234, 258 );
         this.actionGroupBox.Text = "StartProcess";
         this.actionGroupBox.Controls.SetChildIndex( this.processNameTextBox, 0 );
         this.actionGroupBox.Controls.SetChildIndex( this.processNameLabel, 0 );
         this.actionGroupBox.Controls.SetChildIndex( this.batteryThresholdGroupBox, 0 );
         this.actionGroupBox.Controls.SetChildIndex( this.workingDirectoryTextBox, 0 );
         this.actionGroupBox.Controls.SetChildIndex( this.workingDirectoryLabel, 0 );
         this.actionGroupBox.Controls.SetChildIndex( this.argumentsTextBox, 0 );
         this.actionGroupBox.Controls.SetChildIndex( this.argumentsLabel, 0 );
         this.actionGroupBox.Controls.SetChildIndex( this.allowMultipleInstancesCheckBox, 0 );
         // 
         // batteryThresholdGroupBox
         // 
         this.batteryThresholdGroupBox.Location = new System.Drawing.Point( 7, 131 );
         // 
         // descriptionTextBox
         // 
         this.descriptionTextBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                     | System.Windows.Forms.AnchorStyles.Right ) ) );
         // 
         // processNameLabel
         // 
         this.processNameLabel.AutoSize = true;
         this.processNameLabel.Location = new System.Drawing.Point( 3, 23 );
         this.processNameLabel.Name = "processNameLabel";
         this.processNameLabel.Size = new System.Drawing.Size( 79, 13 );
         this.processNameLabel.TabIndex = 5;
         this.processNameLabel.Text = "Process Name:";
         // 
         // processNameTextBox
         // 
         this.processNameTextBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                     | System.Windows.Forms.AnchorStyles.Right ) ) );
         this.processNameTextBox.Location = new System.Drawing.Point( 90, 19 );
         this.processNameTextBox.Name = "processNameTextBox";
         this.processNameTextBox.Size = new System.Drawing.Size( 132, 20 );
         this.processNameTextBox.TabIndex = 6;
         this.processNameTextBox.Validating += new System.ComponentModel.CancelEventHandler( this.ProcessNameTextBoxValidating );
         // 
         // workingDirectoryLabel
         // 
         this.workingDirectoryLabel.AutoSize = true;
         this.workingDirectoryLabel.Location = new System.Drawing.Point( 16, 51 );
         this.workingDirectoryLabel.Name = "workingDirectoryLabel";
         this.workingDirectoryLabel.Size = new System.Drawing.Size( 66, 13 );
         this.workingDirectoryLabel.TabIndex = 7;
         this.workingDirectoryLabel.Text = "Working Dir:";
         // 
         // workingDirectoryTextBox
         // 
         this.workingDirectoryTextBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                     | System.Windows.Forms.AnchorStyles.Right ) ) );
         this.workingDirectoryTextBox.Location = new System.Drawing.Point( 90, 47 );
         this.workingDirectoryTextBox.Name = "workingDirectoryTextBox";
         this.workingDirectoryTextBox.Size = new System.Drawing.Size( 132, 20 );
         this.workingDirectoryTextBox.TabIndex = 8;
         this.workingDirectoryTextBox.Validating += new System.ComponentModel.CancelEventHandler( this.WorkingDirectoryTextBoxValidating );
         // 
         // argumentsLabel
         // 
         this.argumentsLabel.AutoSize = true;
         this.argumentsLabel.Location = new System.Drawing.Point( 22, 79 );
         this.argumentsLabel.Name = "argumentsLabel";
         this.argumentsLabel.Size = new System.Drawing.Size( 60, 13 );
         this.argumentsLabel.TabIndex = 9;
         this.argumentsLabel.Text = "Arguments:";
         // 
         // argumentsTextBox
         // 
         this.argumentsTextBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                     | System.Windows.Forms.AnchorStyles.Right ) ) );
         this.argumentsTextBox.Location = new System.Drawing.Point( 90, 75 );
         this.argumentsTextBox.Name = "argumentsTextBox";
         this.argumentsTextBox.Size = new System.Drawing.Size( 132, 20 );
         this.argumentsTextBox.TabIndex = 10;
         // 
         // allowMultipleInstancesCheckBox
         // 
         this.allowMultipleInstancesCheckBox.AutoSize = true;
         this.allowMultipleInstancesCheckBox.Location = new System.Drawing.Point( 90, 101 );
         this.allowMultipleInstancesCheckBox.Name = "allowMultipleInstancesCheckBox";
         this.allowMultipleInstancesCheckBox.Size = new System.Drawing.Size( 139, 17 );
         this.allowMultipleInstancesCheckBox.TabIndex = 11;
         this.allowMultipleInstancesCheckBox.Text = "Allow Multiple Instances";
         this.allowMultipleInstancesCheckBox.UseVisualStyleBackColor = true;
         // 
         // StartProcess
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.MaximumSize = new System.Drawing.Size( 900, 310 );
         this.MinimumSize = new System.Drawing.Size( 240, 310 );
         this.Name = "StartProcess";
         this.Size = new System.Drawing.Size( 240, 310 );
         this.actionGroupBox.ResumeLayout( false );
         this.actionGroupBox.PerformLayout();
         this.ResumeLayout( false );
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label workingDirectoryLabel;
      private System.Windows.Forms.TextBox workingDirectoryTextBox;
      private System.Windows.Forms.Label processNameLabel;
      private System.Windows.Forms.TextBox processNameTextBox;
      private System.Windows.Forms.CheckBox allowMultipleInstancesCheckBox;
      private System.Windows.Forms.Label argumentsLabel;
      private System.Windows.Forms.TextBox argumentsTextBox;
   }
}
