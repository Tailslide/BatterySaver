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

using BatterySaver.Lib.Service;
using BatterySaver.Properties;

namespace BatterySaver.UI
{
   /// <summary>
   ///    Profile editor form - main configuration form
   /// </summary>
   public partial class ConfigurationEditorForm : Form
   {
      private readonly ProfileService _profileService;

      /// <summary>
      ///    Initializes a new instance of the <see cref = "ConfigurationEditorForm" /> class.
      /// </summary>
      /// <param name = "profileService">The profile service.</param>
      public ConfigurationEditorForm( ProfileService profileService )
      {
         _profileService = profileService;
         InitializeComponent();
      }

      /// <summary>
      ///    Raises the <see cref = "E:System.Windows.Forms.Form.Closed" /> event.
      /// </summary>
      /// <param name = "e">The <see cref = "T:System.EventArgs" /> that contains the event data.</param>
      protected override void OnClosed( EventArgs e )
      {
         ProcessService.TrimCurrentWorkingSet();
      }

      /// <summary>
      ///    Raises the <see cref = "E:System.Windows.Forms.Form.Load" /> event.
      /// </summary>
      /// <param name = "e">An <see cref = "T:System.EventArgs" /> that contains the event data.</param>
      protected override void OnLoad( EventArgs e )
      {
         SuspendLayout();

         // Configure the form
         Text = ProfileEditorResources.String_Title;
         Icon = Resources.Icon_Application;
         saveButton.Text = Resources.String_SaveButton;
         cancelButton.Text = Resources.String_CancelButton;

         // Group boxes
         profileListGroupBox.Text = ProfileEditorResources.String_ProfileListTitle;

         // Configure the profile list
         profileListTreeView.ProfileList = _profileService.GetProfileList();
         profileListTreeView.AfterSelect += ProfileListTreeViewAfterSelect;
         ProfileListTreeViewAfterSelect( null, null );

         ResumeLayout();
      }

      /// <summary>
      ///    ProfileListTreeView "AfterSelect" event handler.  Updates the form when a profile is selected.
      /// </summary>
      /// <param name = "sender">The sender.</param>
      /// <param name = "e">The <see cref = "System.Windows.Forms.TreeViewEventArgs" /> instance containing the event data.</param>
      private void ProfileListTreeViewAfterSelect( object sender, TreeViewEventArgs e )
      {
         profileEventTreeView.Profile = profileListTreeView.SelectedProfile;
         profileListTreeView.HighlightSelected();

         // Update the group box title
         profileActionsGroupBox.Text = string.Format( ProfileEditorResources.String_ProfileActionsTitle, profileListTreeView.SelectedProfile.ProfileName );
      }

      /// <summary>
      ///    Save button "Click" handler
      /// </summary>
      /// <param name = "sender">The sender.</param>
      /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
      private void SaveButtonClick( object sender, EventArgs e )
      {
         if ( MessageBox.Show( ProfileEditorResources.String_SaveConfiguration, Resources.String_Save, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes )
         {
            BatterySaverCore.PersistChanges( _profileService );
            Close();
            SystemTray.Instance.ShowBallonTip( ProfileEditorResources.String_ConfigurationUpdate, 1000 );
         }
      }

      /// <summary>
      ///    Cancel button "Click" handler
      /// </summary>
      /// <param name = "sender">The sender.</param>
      /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
      private void CancelButtonClick( object sender, EventArgs e )
      {
         bool pendingChanges = profileEventTreeView.PendingChanges | profileListTreeView.PendingChanges;
         if ( !pendingChanges || MessageBox.Show( ProfileEditorResources.String_AbandonChanges, Resources.String_Cancel, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes )
         {
            Close();
         }
      }
   }
}