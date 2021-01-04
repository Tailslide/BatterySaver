namespace BatterySaver.Lib.Actions.UI
{
   partial class ControlDevice
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
         this.deviceClassIdLabel = new System.Windows.Forms.Label();
         this.deviceClassIdTextBox = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.instancePathTextBox = new System.Windows.Forms.TextBox();
         this.actionComboBox = new System.Windows.Forms.ComboBox();
         this.actionGroupBox.SuspendLayout();
         this.SuspendLayout();
         // 
         // actionGroupBox
         // 
         this.actionGroupBox.Controls.Add( this.actionComboBox );
         this.actionGroupBox.Controls.Add( this.label1 );
         this.actionGroupBox.Controls.Add( this.instancePathTextBox );
         this.actionGroupBox.Controls.Add( this.deviceClassIdLabel );
         this.actionGroupBox.Controls.Add( this.deviceClassIdTextBox );
         this.actionGroupBox.Size = new System.Drawing.Size( 234, 228 );
         this.actionGroupBox.Text = "ControlDevice";
         this.actionGroupBox.Controls.SetChildIndex( this.deviceClassIdTextBox, 0 );
         this.actionGroupBox.Controls.SetChildIndex( this.batteryThresholdGroupBox, 0 );
         this.actionGroupBox.Controls.SetChildIndex( this.deviceClassIdLabel, 0 );
         this.actionGroupBox.Controls.SetChildIndex( this.instancePathTextBox, 0 );
         this.actionGroupBox.Controls.SetChildIndex( this.label1, 0 );
         this.actionGroupBox.Controls.SetChildIndex( this.actionComboBox, 0 );
         // 
         // batteryThresholdGroupBox
         // 
         this.batteryThresholdGroupBox.Location = new System.Drawing.Point( 8, 101 );
         // 
         // descriptionTextBox
         // 
         this.descriptionTextBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                     | System.Windows.Forms.AnchorStyles.Right ) ) );
         // 
         // deviceClassIdLabel
         // 
         this.deviceClassIdLabel.AutoSize = true;
         this.deviceClassIdLabel.Location = new System.Drawing.Point( 3, 22 );
         this.deviceClassIdLabel.Name = "deviceClassIdLabel";
         this.deviceClassIdLabel.Size = new System.Drawing.Size( 83, 13 );
         this.deviceClassIdLabel.TabIndex = 5;
         this.deviceClassIdLabel.Text = "Device ClassID:";
         // 
         // deviceClassIdTextBox
         // 
         this.deviceClassIdTextBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                     | System.Windows.Forms.AnchorStyles.Right ) ) );
         this.deviceClassIdTextBox.Location = new System.Drawing.Point( 89, 19 );
         this.deviceClassIdTextBox.Name = "deviceClassIdTextBox";
         this.deviceClassIdTextBox.Size = new System.Drawing.Size( 132, 20 );
         this.deviceClassIdTextBox.TabIndex = 6;
         this.deviceClassIdTextBox.Validating += new System.ComponentModel.CancelEventHandler( this.DeviceClassIdTextBoxValidating );
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point( 10, 48 );
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size( 76, 13 );
         this.label1.TabIndex = 7;
         this.label1.Text = "Instance Path:";
         // 
         // instancePathTextBox
         // 
         this.instancePathTextBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                     | System.Windows.Forms.AnchorStyles.Right ) ) );
         this.instancePathTextBox.Location = new System.Drawing.Point( 89, 45 );
         this.instancePathTextBox.Name = "instancePathTextBox";
         this.instancePathTextBox.Size = new System.Drawing.Size( 132, 20 );
         this.instancePathTextBox.TabIndex = 8;
         this.instancePathTextBox.Validating += new System.ComponentModel.CancelEventHandler( this.InstancePathTextBoxValidating );
         // 
         // actionComboBox
         // 
         this.actionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.actionComboBox.FormattingEnabled = true;
         this.actionComboBox.Items.AddRange( new object[] {
            "Enable",
            "Disable"} );
         this.actionComboBox.Location = new System.Drawing.Point( 89, 71 );
         this.actionComboBox.Name = "actionComboBox";
         this.actionComboBox.Size = new System.Drawing.Size( 95, 21 );
         this.actionComboBox.TabIndex = 9;
         // 
         // ControlDevice
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.MaximumSize = new System.Drawing.Size( 900, 280 );
         this.MinimumSize = new System.Drawing.Size( 240, 280 );
         this.Name = "ControlDevice";
         this.Size = new System.Drawing.Size( 240, 280 );
         this.actionGroupBox.ResumeLayout( false );
         this.actionGroupBox.PerformLayout();
         this.ResumeLayout( false );
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label deviceClassIdLabel;
      private System.Windows.Forms.TextBox deviceClassIdTextBox;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox instancePathTextBox;
      private System.Windows.Forms.ComboBox actionComboBox;
   }
}
