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

using BatterySaver.Properties;

namespace BatterySaver.UI.Controls
{
   /// <summary>
   ///    The context menu for the profile list
   /// </summary>
   internal class ProfileListContextMenu : ContextMenuStrip
   {
      private ToolStripMenuItem _editActionMenu;
      private ToolStripMenuItem _addActionMenu;
      private ToolStripMenuItem _deleteActionMenu;

      /// <summary>
      ///    Occurs when the "Edit Profile" menu item is clicked.
      /// </summary>
      public event EventHandler EditClick
      {
         add { _editActionMenu.Click += value; }
         remove { _editActionMenu.Click -= value; }
      }

      /// <summary>
      ///    Occurs when the "Add Profile" menu item is clicked
      /// </summary>
      public event EventHandler AddClick
      {
         add { _addActionMenu.Click += value; }
         remove { _addActionMenu.Click -= value; }
      }

      /// <summary>
      ///    Occurs when the "Delete Profile" menu item is clicked
      /// </summary>
      public event EventHandler DeleteClick
      {
         add { _deleteActionMenu.Click += value; }
         remove { _deleteActionMenu.Click -= value; }
      }

      /// <summary>
      ///    Initializes the context menu.
      /// </summary>
      public void InitializeContextMenu()
      {
         _editActionMenu = new ToolStripMenuItem( ProfileEditorResources.String_EditProfile );

         // Add action
         _addActionMenu = new ToolStripMenuItem( ProfileEditorResources.String_AddProfile );

         // Delete action
         _deleteActionMenu = new ToolStripMenuItem( ProfileEditorResources.String_DeleteProfile );

         // Populate context menu
         Items.Add( _editActionMenu );
         Items.Add( _addActionMenu );
         Items.Add( "-" );
         Items.Add( _deleteActionMenu );
      }

      /// <summary>
      ///    Enables the delete menu item.
      /// </summary>
      /// <param name = "enabled">if set to <c>true</c> the menu item will be enabled, otherwise it will be disabled.</param>
      public void EnableDelete( bool enabled )
      {
         _deleteActionMenu.Enabled = enabled;
      }

      /// <summary>
      ///    Enables the add menu item.
      /// </summary>
      /// <param name = "enabled">if set to <c>true</c> the menu item will be enabled, otherwise it will be disabled.</param>
      public void EnableAdd( bool enabled )
      {
         _addActionMenu.Enabled = enabled;
      }

      /// <summary>
      ///    Enables the edi menu item.
      /// </summary>
      /// <param name = "enabled">if set to <c>true</c> the menu item will be enabled, otherwise it will be disabled.</param>
      public void EnableEdit( bool enabled )
      {
         _editActionMenu.Enabled = enabled;
      }
   }
}