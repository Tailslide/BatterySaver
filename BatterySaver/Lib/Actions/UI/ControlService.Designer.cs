namespace BatterySaver.Lib.Actions.UI
{
   partial class ControlService
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
         this.serviceNameLabel = new System.Windows.Forms.Label();
         this.serviceNameTextBox = new System.Windows.Forms.TextBox();
         this.actionComboBox = new System.Windows.Forms.ComboBox();
         this.actionGroupBox.SuspendLayout();
         this.SuspendLayout();
         // 
         // actionGroupBox
         // 
         this.actionGroupBox.Controls.Add( this.actionComboBox );
         this.actionGroupBox.Controls.Add( this.serviceNameLabel );
         this.actionGroupBox.Controls.Add( this.serviceNameTextBox );
         this.actionGroupBox.Size = new System.Drawing.Size( 234, 208 );
         this.actionGroupBox.Text = "ControlService";
         this.actionGroupBox.Controls.SetChildIndex( this.serviceNameTextBox, 0 );
         this.actionGroupBox.Controls.SetChildIndex( this.batteryThresholdGroupBox, 0 );
         this.actionGroupBox.Controls.SetChildIndex( this.serviceNameLabel, 0 );
         this.actionGroupBox.Controls.SetChildIndex( this.actionComboBox, 0 );
         // 
         // batteryThresholdGroupBox
         // 
         this.batteryThresholdGroupBox.Location = new System.Drawing.Point( 8, 81 );
         // 
         // descriptionTextBox
         // 
         this.descriptionTextBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                     | System.Windows.Forms.AnchorStyles.Right ) ) );
         // 
         // serviceNameLabel
         // 
         this.serviceNameLabel.AutoSize = true;
         this.serviceNameLabel.Location = new System.Drawing.Point( 3, 22 );
         this.serviceNameLabel.Name = "serviceNameLabel";
         this.serviceNameLabel.Size = new System.Drawing.Size( 77, 13 );
         this.serviceNameLabel.TabIndex = 5;
         this.serviceNameLabel.Text = "Service Name:";
         // 
         // serviceNameTextBox
         // 
         this.serviceNameTextBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                     | System.Windows.Forms.AnchorStyles.Right ) ) );
         this.serviceNameTextBox.Location = new System.Drawing.Point( 89, 19 );
         this.serviceNameTextBox.Name = "serviceNameTextBox";
         this.serviceNameTextBox.Size = new System.Drawing.Size( 132, 20 );
         this.serviceNameTextBox.TabIndex = 6;
         this.serviceNameTextBox.Validating += new System.ComponentModel.CancelEventHandler( this.ServiceNameTextBoxValidating );
         // 
         // actionComboBox
         // 
         this.actionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.actionComboBox.FormattingEnabled = true;
         this.actionComboBox.Items.AddRange( new object[] {
            "Enable",
            "Disable"} );
         this.actionComboBox.Location = new System.Drawing.Point( 89, 46 );
         this.actionComboBox.Name = "actionComboBox";
         this.actionComboBox.Size = new System.Drawing.Size( 95, 21 );
         this.actionComboBox.TabIndex = 7;
         // 
         // ControlService
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.MaximumSize = new System.Drawing.Size( 900, 260 );
         this.MinimumSize = new System.Drawing.Size( 240, 260 );
         this.Name = "ControlService";
         this.Size = new System.Drawing.Size( 240, 260 );
         this.actionGroupBox.ResumeLayout( false );
         this.actionGroupBox.PerformLayout();
         this.ResumeLayout( false );
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label serviceNameLabel;
      private System.Windows.Forms.TextBox serviceNameTextBox;
      private System.Windows.Forms.ComboBox actionComboBox;
   }
}
