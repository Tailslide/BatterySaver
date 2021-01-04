namespace BatterySaver.Lib.Actions.UI
{
   partial class BaseUiComponent
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
         this.components = new System.ComponentModel.Container();
         this.actionGroupBox = new System.Windows.Forms.GroupBox();
         this.batteryThresholdGroupBox = new System.Windows.Forms.GroupBox();
         this.minPercentLabel = new System.Windows.Forms.Label();
         this.maximumBatteryPercentageSlider = new System.Windows.Forms.TrackBar();
         this.maxPercentLabel = new System.Windows.Forms.Label();
         this.minimumBatteryPercentageSlider = new System.Windows.Forms.TrackBar();
         this.descriptionTextBox = new System.Windows.Forms.TextBox();
         this.descriptionLabel = new System.Windows.Forms.Label();
         this.toolTip = new System.Windows.Forms.ToolTip( this.components );
         this.actionGroupBox.SuspendLayout();
         this.batteryThresholdGroupBox.SuspendLayout();
         ( ( System.ComponentModel.ISupportInitialize )( this.maximumBatteryPercentageSlider ) ).BeginInit();
         ( ( System.ComponentModel.ISupportInitialize )( this.minimumBatteryPercentageSlider ) ).BeginInit();
         this.SuspendLayout();
         // 
         // actionGroupBox
         // 
         this.actionGroupBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                     | System.Windows.Forms.AnchorStyles.Left )
                     | System.Windows.Forms.AnchorStyles.Right ) ) );
         this.actionGroupBox.Controls.Add( this.batteryThresholdGroupBox );
         this.actionGroupBox.Location = new System.Drawing.Point( 3, 49 );
         this.actionGroupBox.Name = "actionGroupBox";
         this.actionGroupBox.Size = new System.Drawing.Size( 234, 206 );
         this.actionGroupBox.TabIndex = 0;
         this.actionGroupBox.TabStop = false;
         this.actionGroupBox.Text = "Edit Action";
         // 
         // batteryThresholdGroupBox
         // 
         this.batteryThresholdGroupBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
                     | System.Windows.Forms.AnchorStyles.Right ) ) );
         this.batteryThresholdGroupBox.Controls.Add( this.minPercentLabel );
         this.batteryThresholdGroupBox.Controls.Add( this.maximumBatteryPercentageSlider );
         this.batteryThresholdGroupBox.Controls.Add( this.maxPercentLabel );
         this.batteryThresholdGroupBox.Controls.Add( this.minimumBatteryPercentageSlider );
         this.batteryThresholdGroupBox.Location = new System.Drawing.Point( 6, 79 );
         this.batteryThresholdGroupBox.Name = "batteryThresholdGroupBox";
         this.batteryThresholdGroupBox.Size = new System.Drawing.Size( 220, 121 );
         this.batteryThresholdGroupBox.TabIndex = 4;
         this.batteryThresholdGroupBox.TabStop = false;
         this.batteryThresholdGroupBox.Text = "Battery Threshold";
         // 
         // minPercentLabel
         // 
         this.minPercentLabel.AutoSize = true;
         this.minPercentLabel.Location = new System.Drawing.Point( 6, 35 );
         this.minPercentLabel.Name = "minPercentLabel";
         this.minPercentLabel.Size = new System.Drawing.Size( 59, 13 );
         this.minPercentLabel.TabIndex = 2;
         this.minPercentLabel.Text = "Minimum %";
         // 
         // maximumBatteryPercentageSlider
         // 
         this.maximumBatteryPercentageSlider.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                     | System.Windows.Forms.AnchorStyles.Right ) ) );
         this.maximumBatteryPercentageSlider.Location = new System.Drawing.Point( 71, 65 );
         this.maximumBatteryPercentageSlider.Maximum = 100;
         this.maximumBatteryPercentageSlider.Name = "maximumBatteryPercentageSlider";
         this.maximumBatteryPercentageSlider.Size = new System.Drawing.Size( 144, 45 );
         this.maximumBatteryPercentageSlider.TabIndex = 1;
         this.maximumBatteryPercentageSlider.TickFrequency = 10;
         this.maximumBatteryPercentageSlider.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
         this.maximumBatteryPercentageSlider.Value = 100;
         this.maximumBatteryPercentageSlider.ValueChanged += new System.EventHandler( this.MaximumBatteryPercentageSliderValueChanged );
         // 
         // maxPercentLabel
         // 
         this.maxPercentLabel.AutoSize = true;
         this.maxPercentLabel.Location = new System.Drawing.Point( 6, 81 );
         this.maxPercentLabel.Name = "maxPercentLabel";
         this.maxPercentLabel.Size = new System.Drawing.Size( 62, 13 );
         this.maxPercentLabel.TabIndex = 3;
         this.maxPercentLabel.Text = "Maximum %";
         // 
         // minimumBatteryPercentageSlider
         // 
         this.minimumBatteryPercentageSlider.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                     | System.Windows.Forms.AnchorStyles.Right ) ) );
         this.minimumBatteryPercentageSlider.Location = new System.Drawing.Point( 71, 19 );
         this.minimumBatteryPercentageSlider.Maximum = 100;
         this.minimumBatteryPercentageSlider.Name = "minimumBatteryPercentageSlider";
         this.minimumBatteryPercentageSlider.Size = new System.Drawing.Size( 144, 45 );
         this.minimumBatteryPercentageSlider.TabIndex = 0;
         this.minimumBatteryPercentageSlider.TickFrequency = 10;
         this.minimumBatteryPercentageSlider.ValueChanged += new System.EventHandler( this.MinimumBatteryPercentageSliderValueChanged );
         // 
         // descriptionTextBox
         // 
         this.descriptionTextBox.Location = new System.Drawing.Point( 3, 23 );
         this.descriptionTextBox.Name = "descriptionTextBox";
         this.descriptionTextBox.Size = new System.Drawing.Size( 234, 20 );
         this.descriptionTextBox.TabIndex = 6;
         this.descriptionTextBox.Validating += new System.ComponentModel.CancelEventHandler( this.DescriptionTextBoxValidating );
         // 
         // descriptionLabel
         // 
         this.descriptionLabel.AutoSize = true;
         this.descriptionLabel.BackColor = System.Drawing.Color.Transparent;
         this.descriptionLabel.Location = new System.Drawing.Point( 0, 6 );
         this.descriptionLabel.Name = "descriptionLabel";
         this.descriptionLabel.Size = new System.Drawing.Size( 63, 13 );
         this.descriptionLabel.TabIndex = 7;
         this.descriptionLabel.Text = "Description:";
         // 
         // toolTip
         // 
         this.toolTip.AutoPopDelay = 5000;
         this.toolTip.InitialDelay = 10;
         this.toolTip.IsBalloon = true;
         this.toolTip.ReshowDelay = 100;
         // 
         // BaseUiComponent
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add( this.descriptionLabel );
         this.Controls.Add( this.descriptionTextBox );
         this.Controls.Add( this.actionGroupBox );
         this.Name = "BaseUiComponent";
         this.Size = new System.Drawing.Size( 240, 258 );
         this.actionGroupBox.ResumeLayout( false );
         this.batteryThresholdGroupBox.ResumeLayout( false );
         this.batteryThresholdGroupBox.PerformLayout();
         ( ( System.ComponentModel.ISupportInitialize )( this.maximumBatteryPercentageSlider ) ).EndInit();
         ( ( System.ComponentModel.ISupportInitialize )( this.minimumBatteryPercentageSlider ) ).EndInit();
         this.ResumeLayout( false );
         this.PerformLayout();

      }

      #endregion

      protected System.Windows.Forms.GroupBox actionGroupBox;
      private System.Windows.Forms.Label minPercentLabel;
      private System.Windows.Forms.TrackBar maximumBatteryPercentageSlider;
      private System.Windows.Forms.Label maxPercentLabel;
      private System.Windows.Forms.TrackBar minimumBatteryPercentageSlider;
      protected System.Windows.Forms.GroupBox batteryThresholdGroupBox;
      protected System.Windows.Forms.TextBox descriptionTextBox;
      protected System.Windows.Forms.Label descriptionLabel;
      private System.Windows.Forms.ToolTip toolTip;

   }
}
