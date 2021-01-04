namespace BatterySaver.UI.Controls
{
   partial class ProfileListTreeView
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( ProfileListTreeView ) );
         this.profileListImageList = new System.Windows.Forms.ImageList( this.components );
         this.contextMenu = new BatterySaver.UI.Controls.ProfileListContextMenu();
         this.SuspendLayout();
         // 
         // profileListImageList
         // 
         this.profileListImageList.ImageStream = ( ( System.Windows.Forms.ImageListStreamer )( resources.GetObject( "profileListImageList.ImageStream" ) ) );
         this.profileListImageList.TransparentColor = System.Drawing.Color.Empty;
         this.profileListImageList.Images.SetKeyName( 0, "Action" );
         this.profileListImageList.Images.SetKeyName( 1, "Event" );
         this.profileListImageList.Images.SetKeyName( 2, "Profile" );
         // 
         // contextMenu
         // 
         this.contextMenu.Name = "contextMenu";
         // 
         // ProfileListTreeView
         // 
         this.ContextMenuStrip = this.contextMenu;
         this.LineColor = System.Drawing.Color.Black;
         this.Size = new System.Drawing.Size( 100, 100 );
         this.ResumeLayout( false );

      }

      #endregion

      private System.Windows.Forms.ImageList profileListImageList;
      private ProfileListContextMenu contextMenu;
   }
}
