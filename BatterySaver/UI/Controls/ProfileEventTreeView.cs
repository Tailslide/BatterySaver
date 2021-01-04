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
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BatterySaver.Lib;
using BatterySaver.Lib.Actions;
using BatterySaver.Properties;

namespace BatterySaver.UI.Controls
{
   /// <summary>
   ///    A treeview representation of profile events and associated actions
   /// </summary>
   public partial class ProfileEventTreeView : TreeView
   {
      #region Declarations

      private const string IMAGELIST_ICON_ACTION = "Action";
      private const string IMAGELIST_ICON_EVENT = "Event";
      private Profile _profile;
      private bool _pendingChanges;

      #endregion

      #region Public Properties

      /// <summary>
      ///    Gets a value indicating whether there are pending changes.
      /// </summary>
      /// <value><c>true</c> if there are pending changes; otherwise, <c>false</c>.</value>
      public bool PendingChanges
      {
         get { return _pendingChanges; }
      }

      /// <summary>
      ///    Gets or sets the profile.
      /// </summary>
      /// <value>The profile.</value>
      public Profile Profile
      {
         set
         {
            _profile = value;
            PopulateTree();
         }
      }

      #endregion

      #region Constructor

      /// <summary>
      ///    Initializes a new instance of the <see cref = "ProfileEventTreeView" /> class.
      /// </summary>
      public ProfileEventTreeView()
      {
         InitializeComponent();
         InitializeContextMenu();
         ImageList = profileEventImageList;
         LabelEdit = true;
      }

      #endregion

      #region Private Methods

      /// <summary>
      ///    Populates the tree.
      /// </summary>
      private void PopulateTree()
      {
         BeginUpdate();
         Nodes.Clear();
         if ( _profile != null )
         {
            foreach ( var eventType in Enum.GetValues( typeof ( EventType ) ) )
            {
               var eventNode = new ProfileEventTreeNode( (EventType)eventType, IMAGELIST_ICON_EVENT );

               // Add all of the actions to to the event
               foreach ( var action in _profile.GetActionsForEvent( (EventType)eventType ) )
               {
                  eventNode.AddNode( new ProfileEventActionTreeNode( action, IMAGELIST_ICON_ACTION ) );
               }

               // Add the node to the tree
               Nodes.Add( eventNode );
            }
         }
         EndUpdate();
      }

      #region Context Menu

      /// <summary>
      ///    Initializes the context menu.
      /// </summary>
      private void InitializeContextMenu()
      {
         // Initialize
         contextMenu.InitializeContextMenu( ActionFactory.ValidActionCollection.Keys );

         // Edit action
         contextMenu.EditClick += EditActionClick;

         // Add action
         contextMenu.AddItemClick += AddActionClick;

         // Delete action
         contextMenu.DeleteClick += DeleteActionMenuClick;

         // Control what get's displayed on opening
         contextMenu.Opening += ContextMenuOpening;
      }

      /// <summary>
      ///    Event handler for the "Add Action" context menu item "Click" event
      /// </summary>
      /// <param name = "sender">The sender.</param>
      /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
      private void AddActionClick( object sender, EventArgs e )
      {
         var parent = SelectedNode;
         if ( parent is ProfileEventActionTreeNode )
         {
            parent = parent.Parent;
         }

         // Create a new action
         IAction action = ActionFactory.Create( ( sender as ToolStripMenuItem ).Text, null, false );
         action.Description = string.Empty;

         // Get the form to display
         var form = ActionEditorFormFactory.CreateActionEditor( action );
         form.LoadAction( action );

         // Display the form
         if ( form.ShowDialog( FindForm() ) == DialogResult.OK )
         {
            // If we saved the result, add the node to the tree
            parent.Nodes.Add( new ProfileEventActionTreeNode( action, IMAGELIST_ICON_ACTION ) );

            // Add the action to the collection
            EventType eventType = ( parent as ProfileEventTreeNode ).EventType;
            _profile.AssociateAction( eventType, action );
            _pendingChanges = true;
         }
      }

      /// <summary>
      ///    Event handler for the "Edit Action" context menu item "Click" event
      /// </summary>
      /// <param name = "sender">The sender.</param>
      /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
      private void EditActionClick( object sender, EventArgs e )
      {
         if ( SelectedNode is ProfileEventActionTreeNode )
         {
            var node = SelectedNode as ProfileEventActionTreeNode;
            var form = ActionEditorFormFactory.CreateActionEditor( node.Action );
            form.LoadAction( node.Action );
            form.ShowDialog( FindForm() );
            node.RefreshLabel();
            _pendingChanges = true;
         }
      }

      /// <summary>
      ///    Event handler for the "Delete Action" context menu item "Click" event
      /// </summary>
      /// <param name = "sender">The sender.</param>
      /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
      private void DeleteActionMenuClick( object sender, EventArgs e )
      {
         if ( SelectedNode is ProfileEventActionTreeNode )
         {
            if ( MessageBox.Show( ProfileEditorResources.String_DeleteActionPrompt, ProfileEditorResources.String_DeleteAction, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes )
            {
               var parent = SelectedNode.Parent as ProfileEventTreeNode;

               // Remove the action from the event
               _profile.RemoveActionFromEvent( parent.EventType, ( SelectedNode as ProfileEventActionTreeNode ).Action );

               // Remove the action from the tree
               parent.Nodes.Remove( SelectedNode );
               _pendingChanges = true;
            }
         }
      }

      /// <summary>
      ///    Event handler for the "Opening" event on the context menu.  Sets the menu items that are enabled based on the context.
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
         if ( !( SelectedNode is ProfileEventActionTreeNode ) )
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

      #endregion

      #region Event Handlers

      /// <summary>
      ///    Raises the <see cref = "E:System.Windows.Forms.TreeView.NodeMouseDoubleClick" /> event.  Enables editing of the node label.
      /// </summary>
      /// <param name = "e">A <see cref = "T:System.Windows.Forms.TreeNodeMouseClickEventArgs" /> that contains the event data.</param>
      protected override void OnNodeMouseDoubleClick( TreeNodeMouseClickEventArgs e )
      {
         base.OnNodeMouseDoubleClick( e );
         e.Node.BeginEdit();
      }

      /// <summary>
      ///    Raises the <see cref = "E:System.Windows.Forms.TreeView.BeforeLabelEdit" /> event.  Prevents editing of non-action labels.
      /// </summary>
      /// <param name = "e">A <see cref = "T:System.Windows.Forms.NodeLabelEditEventArgs" /> that contains the event data.</param>
      protected override void OnBeforeLabelEdit( NodeLabelEditEventArgs e )
      {
         base.OnBeforeLabelEdit( e );
         if ( !( e.Node is ProfileEventActionTreeNode ) )
         {
            e.CancelEdit = true;
         }
      }

      /// <summary>
      ///    Raises the <see cref = "E:System.Windows.Forms.TreeView.AfterLabelEdit" /> event.  Propagates the label to the underlying object.
      /// </summary>
      /// <param name = "e">A <see cref = "T:System.Windows.Forms.NodeLabelEditEventArgs" /> that contains the event data.</param>
      protected override void OnAfterLabelEdit( NodeLabelEditEventArgs e )
      {
         base.OnAfterLabelEdit( e );
         if ( !string.IsNullOrEmpty( e.Label ) )
         {
            var actionNode = e.Node as ProfileEventActionTreeNode;
            actionNode.Action.Description = e.Label;
         }
         else
         {
            e.CancelEdit = true;
         }
      }

      #endregion

      #endregion
   }
}