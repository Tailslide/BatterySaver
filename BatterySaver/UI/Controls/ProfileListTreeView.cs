#region Copyright © 2010, Ryan Emerle; All rights reserved

// Copyright © 2010, Ryan Emerle
// All rights reserved.
// http://www.emerle.net/
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions
// are met:
// 
// - Redistributions of source code must retain the above copyright
// notice, this list of conditions and the following disclaimer.
// 
// - Neither the name of the Ryan Emerle, nor the names of any
// contributors may be used to endorse or promote products
// derived from this software without specific prior written 
// permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
// FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE 
// COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES INCLUDING,
// BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; 
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER 
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT 
// LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
// POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BatterySaver.Lib;
using BatterySaver.Lib.Extensions;
using BatterySaver.Properties;

namespace BatterySaver.UI.Controls
{
   /// <summary>
   ///    A treeview for displaying available profiles
   /// </summary>
   public partial class ProfileListTreeView : TreeView
   {
      #region Declarations

      private const string IMAGELIST_ICON_PROFILE = "Profile";
      private IList<Profile> _profileList;
      private bool _pendingChanges;

      /// <summary>
      ///    Occurs when the last node is deleted.
      /// </summary>
      public event EventHandler LastNodeDeleted;

      #endregion

      #region Properties

      /// <summary>
      ///    Gets a value indicating whether ther are pending changes.
      /// </summary>
      /// <value><c>true</c> if there are pending changes; otherwise, <c>false</c>.</value>
      public bool PendingChanges
      {
         get { return _pendingChanges; }
      }

      /// <summary>
      ///    Gets or sets the profile list.
      /// </summary>
      /// <value>The profile list.</value>
      public IList<Profile> ProfileList
      {
         get { return _profileList; }
         set
         {
            _profileList = value;
            PopulateProfileList( value );
         }
      }

      /// <summary>
      ///    Gets the selected profile.
      /// </summary>
      /// <value>The selected profile.</value>
      public Profile SelectedProfile
      {
         get { return ( SelectedNode as ProfileTreeNode ).Profile; }
      }

      #endregion

      #region Constructors

      /// <summary>
      ///    Initializes a new instance of the <see cref = "ProfileListTreeView" /> class.
      /// </summary>
      public ProfileListTreeView()
      {
         InitializeComponent();
         ShowLines = false;
         ShowRootLines = false;
         LabelEdit = true;
         ImageList = profileListImageList;
         InitializeContextMenu();
      }

      #endregion

      #region Public Methods

      /// <summary>
      ///    Highlights the selected node
      /// </summary>
      public void HighlightSelected()
      {
         BeginUpdate();
         foreach ( TreeNode node in Nodes )
         {
            node.NodeFont = new Font( Font, FontStyle.Regular );
         }
         SelectedNode.NodeFont = new Font( Font, FontStyle.Bold );
         EndUpdate();
      }

      #endregion

      #region Private Methods

      /// <summary>
      ///    Populates the profile list.
      /// </summary>
      /// <param name = "value">The value.</param>
      private void PopulateProfileList( IList<Profile> value )
      {
         BeginUpdate();
         Nodes.Clear();
         if ( value != null )
         {
            for ( int index = 0; index < value.Count; index++ )
            {
               var profile = value[index];
               var profileNode = new ProfileTreeNode( profile );
               Nodes.Add( profileNode );
               if ( index == 0 )
               {
                  SelectedNode = profileNode;
               }
            }
         }
         EndUpdate();
      }

      /// <summary>
      ///    Refreshes the profile list.
      /// </summary>
      private void RefreshProfileList()
      {
         ( (List<Profile>)_profileList ).Sort( ( x, y ) => string.Compare( x.ProfileName, y.ProfileName ) );
         PopulateProfileList( _profileList );
      }

      #region Context Menu

      /// <summary>
      ///    Initializes the context menu.
      /// </summary>
      private void InitializeContextMenu()
      {
         // Initialize
         contextMenu.InitializeContextMenu();

         // Edit action
         contextMenu.EditClick += EditActionClick;

         // Add action
         contextMenu.AddClick += AddActionClick;

         // Delete action
         contextMenu.DeleteClick += DeleteActionMenuClick;

         // Control what get's displayed on opening
         contextMenu.Opening += ContextMenuOpening;
      }

      /// <summary>
      ///    Event handler for the Opening event on the context menu.  Control what options are available based on the selected node.
      /// </summary>
      /// <param name = "sender">The sender.</param>
      /// <param name = "e">The <see cref = "System.ComponentModel.CancelEventArgs" /> instance containing the event data.</param>
      private void ContextMenuOpening( object sender, System.ComponentModel.CancelEventArgs e )
      {
         if ( SelectedNode == null )
         {
            e.Cancel = true;
            return;
         }
         if ( !( SelectedNode is ProfileTreeNode ) )
         {
            contextMenu.EnableDelete( false );
            contextMenu.EnableEdit( false );
         }
         else
         {
            contextMenu.EnableDelete( true );
            contextMenu.EnableEdit( true );
         }
      }

      /// <summary>
      ///    The event handler for the Click event on the "Delete Action" menu item
      /// </summary>
      /// <param name = "sender">The sender.</param>
      /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
      private void DeleteActionMenuClick( object sender, EventArgs e )
      {
         if ( MessageBox.Show( ProfileEditorResources.String_DeleteProfilePrompt, ProfileEditorResources.String_DeleteProfile, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes )
         {
            var profileNode = SelectedNode as ProfileTreeNode;
            _profileList.Remove( profileNode.Profile );
            Nodes.Remove( profileNode );
            OnLastNodeDeleted( EventArgs.Empty );
            Invalidate();
            _pendingChanges = true;
         }
      }

      /// <summary>
      ///    The event handler for the Click event on the "Add Action" menu item
      /// </summary>
      /// <param name = "sender">The sender.</param>
      /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
      private void AddActionClick( object sender, EventArgs e )
      {
         var profile = new Profile( string.Empty );
         var profileEditor = new ProfileEditorForm( profile );
         if ( profileEditor.ShowDialog( FindForm() ) == DialogResult.OK )
         {
            // Do Add
            _profileList.Add( profile );

            // Sort the list
            RefreshProfileList();
            _pendingChanges = true;
         }
      }

      /// <summary>
      ///    The event handler for the Click event on the "Edit Action" menu item
      /// </summary>
      /// <param name = "sender">The sender.</param>
      /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
      private void EditActionClick( object sender, EventArgs e )
      {
         var profileNode = SelectedNode as ProfileTreeNode;
         var profileEditor = new ProfileEditorForm( profileNode.Profile );
         if ( profileEditor.ShowDialog( FindForm() ) == DialogResult.OK )
         {
            // Make sure only one is the default
            if ( profileNode.Profile.IsDefault )
            {
               _profileList.Each( p => p.IsDefault = false );
               profileNode.Profile.IsDefault = true;
            }

            RefreshProfileList();
            _pendingChanges = true;
         }
      }

      #endregion

      #region Event Handlers

      /// <summary>
      ///    Raises the <see cref = "E:LastNodeDeleted" /> event.
      /// </summary>
      /// <param name = "args">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
      protected virtual void OnLastNodeDeleted( EventArgs args )
      {
         if ( LastNodeDeleted != null )
         {
            LastNodeDeleted( this, args );
         }
      }

      /// <summary>
      ///    Raises the <see cref = "E:System.Windows.Forms.TreeView.AfterLabelEdit" /> event.
      /// </summary>
      /// <param name = "e">A <see cref = "T:System.Windows.Forms.NodeLabelEditEventArgs" /> that contains the event data.</param>
      protected override void OnAfterLabelEdit( NodeLabelEditEventArgs e )
      {
         var profileNode = e.Node as ProfileTreeNode;
         if ( !string.IsNullOrEmpty( e.Label ) )
         {
            profileNode.Profile.ProfileName = e.Label;
            RefreshProfileList();
         }
         else
         {
            e.CancelEdit = true;
            e.Node.Text = profileNode.Profile.ProfileName;
         }
      }

      /// <summary>
      ///    Raises the <see cref = "E:System.Windows.Forms.TreeView.NodeMouseDoubleClick" /> event.
      /// </summary>
      /// <param name = "e">A <see cref = "T:System.Windows.Forms.TreeNodeMouseClickEventArgs" /> that contains the event data.</param>
      protected override void OnNodeMouseDoubleClick( TreeNodeMouseClickEventArgs e )
      {
         e.Node.BeginEdit();
      }

      #endregion

      #endregion

      #region ProfileTreeNode Class

      /// <summary>
      ///    A tree node that holds a profile
      /// </summary>
      private class ProfileTreeNode : TreeNode
      {
         private readonly Profile _profile;

         /// <summary>
         ///    Gets the profile.
         /// </summary>
         /// <value>The profile.</value>
         public Profile Profile
         {
            get { return _profile; }
         }

         /// <summary>
         ///    Initializes a new instance of the <see cref = "ProfileTreeNode" /> class.
         /// </summary>
         /// <param name = "profile">The profile.</param>
         public ProfileTreeNode( Profile profile )
            : base( profile.ProfileName )
         {
            _profile = profile;
            ImageKey = IMAGELIST_ICON_PROFILE;
            SelectedImageKey = ImageKey;
         }
      }

      #endregion
   }
}