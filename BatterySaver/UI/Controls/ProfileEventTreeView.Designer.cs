namespace BatterySaver.UI.Controls
{
   partial class ProfileEventTreeView
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( ProfileEventTreeView ) );
         this.profileEventImageList = new System.Windows.Forms.ImageList( this.components );
         this.contextMenu = new ProfileEventTreeViewContextMenu();

         this.SuspendLayout();
         // 
         // profileEventImageList
         // 
         this.profileEventImageList.ImageStream = ( ( System.Windows.Forms.ImageListStreamer )( resources.GetObject( "profileEventImageList.ImageStream" ) ) );
         this.profileEventImageList.TransparentColor = System.Drawing.Color.Empty;
         this.profileEventImageList.Images.SetKeyName( 0, "Action" );
         this.profileEventImageList.Images.SetKeyName( 1, "Event" );
         this.profileEventImageList.Images.SetKeyName( 2, "Profile" );
         // 
         // contextMenu
         // 
         this.contextMenu.Name = "contextMenu";
         this.contextMenu.Size = new System.Drawing.Size( 61, 4 );
         // 
         // ProfileEventTreeView
         // 
         this.ContextMenuStrip = this.contextMenu;
         this.LineColor = System.Drawing.Color.Black;
         this.ResumeLayout( false );

      }

      #endregion

      private System.Windows.Forms.ImageList profileEventImageList;
      private ProfileEventTreeViewContextMenu contextMenu;
   }
}
