namespace BatterySaver.Lib.Actions.UI
{
   partial class KillProcess
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
         this.actionGroupBox.SuspendLayout();
         this.SuspendLayout();
         // 
         // actionGroupBox
         // 
         this.actionGroupBox.Controls.Add( this.processNameLabel );
         this.actionGroupBox.Controls.Add( this.processNameTextBox );
         this.actionGroupBox.Size = new System.Drawing.Size( 234, 178 );
         this.actionGroupBox.Text = "KillProcess";
         this.actionGroupBox.Controls.SetChildIndex( this.processNameTextBox, 0 );
         this.actionGroupBox.Controls.SetChildIndex( this.batteryThresholdGroupBox, 0 );
         this.actionGroupBox.Controls.SetChildIndex( this.processNameLabel, 0 );
         // 
         // batteryThresholdGroupBox
         // 
         this.batteryThresholdGroupBox.Location = new System.Drawing.Point( 8, 50 );
         // 
         // descriptionTextBox
         // 
         this.descriptionTextBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                     | System.Windows.Forms.AnchorStyles.Right ) ) );
         // 
         // processNameLabel
         // 
         this.processNameLabel.AutoSize = true;
         this.processNameLabel.Location = new System.Drawing.Point( 3, 22 );
         this.processNameLabel.Name = "processNameLabel";
         this.processNameLabel.Size = new System.Drawing.Size( 79, 13 );
         this.processNameLabel.TabIndex = 5;
         this.processNameLabel.Text = "Process Name:";
         // 
         // processNameTextBox
         // 
         this.processNameTextBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                     | System.Windows.Forms.AnchorStyles.Right ) ) );
         this.processNameTextBox.Location = new System.Drawing.Point( 89, 19 );
         this.processNameTextBox.Name = "processNameTextBox";
         this.processNameTextBox.Size = new System.Drawing.Size( 132, 20 );
         this.processNameTextBox.TabIndex = 6;
         this.processNameTextBox.Validating += new System.ComponentModel.CancelEventHandler( this.ProcessNameTextBoxValidating );
         // 
         // KillProcess
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.MaximumSize = new System.Drawing.Size( 900, 230 );
         this.MinimumSize = new System.Drawing.Size( 240, 230 );
         this.Name = "KillProcess";
         this.Size = new System.Drawing.Size( 240, 230 );
         this.actionGroupBox.ResumeLayout( false );
         this.actionGroupBox.PerformLayout();
         this.ResumeLayout( false );
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label processNameLabel;
      private System.Windows.Forms.TextBox processNameTextBox;
   }
}
