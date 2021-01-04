namespace BatterySaver.UI
{
   partial class ActionEditorForm
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
         this.saveButton = new System.Windows.Forms.Button();
         this.cancelButton = new System.Windows.Forms.Button();
         this.actionComponentPanel = new System.Windows.Forms.Panel();
         this.SuspendLayout();
         // 
         // saveButton
         // 
         this.saveButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
         this.saveButton.Location = new System.Drawing.Point( 42, 84 );
         this.saveButton.Name = "saveButton";
         this.saveButton.Size = new System.Drawing.Size( 75, 23 );
         this.saveButton.TabIndex = 1;
         this.saveButton.Text = "&Save";
         this.saveButton.UseVisualStyleBackColor = true;
         this.saveButton.Click += new System.EventHandler( this.SaveButtonClick );
         // 
         // cancelButton
         // 
         this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
         this.cancelButton.CausesValidation = false;
         this.cancelButton.Location = new System.Drawing.Point( 123, 84 );
         this.cancelButton.Name = "cancelButton";
         this.cancelButton.Size = new System.Drawing.Size( 75, 23 );
         this.cancelButton.TabIndex = 2;
         this.cancelButton.Text = "&Cancel";
         this.cancelButton.UseVisualStyleBackColor = true;
         this.cancelButton.Click += new System.EventHandler( this.CancelButtonClick );
         // 
         // actionComponentPanel
         // 
         this.actionComponentPanel.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                     | System.Windows.Forms.AnchorStyles.Left )
                     | System.Windows.Forms.AnchorStyles.Right ) ) );
         this.actionComponentPanel.Location = new System.Drawing.Point( 12, 12 );
         this.actionComponentPanel.Name = "actionComponentPanel";
         this.actionComponentPanel.Size = new System.Drawing.Size( 217, 66 );
         this.actionComponentPanel.TabIndex = 3;
         // 
         // ActionEditorForm
         // 
         this.AcceptButton = this.saveButton;
         this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.cancelButton;
         this.ClientSize = new System.Drawing.Size( 241, 119 );
         this.Controls.Add( this.actionComponentPanel );
         this.Controls.Add( this.cancelButton );
         this.Controls.Add( this.saveButton );
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "ActionEditorForm";
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "ActionEditorForm";
         this.ResumeLayout( false );

      }

      #endregion

      private System.Windows.Forms.Button saveButton;
      private System.Windows.Forms.Button cancelButton;
      private System.Windows.Forms.Panel actionComponentPanel;
   }
}