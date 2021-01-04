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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BatterySaver.Lib;
using BatterySaver.Properties;

namespace BatterySaver.UI
{
   /// <summary>
   ///    Profile editor form
   /// </summary>
   public partial class ProfileEditorForm : Form
   {
      private readonly Profile _profile;

      /// <summary>
      ///    Initializes a new instance of the <see cref = "ProfileEditorForm" /> class.
      /// </summary>
      /// <param name = "profile">The profile.</param>
      public ProfileEditorForm( Profile profile )
      {
         InitializeComponent();
         _profile = profile;
         saveButton.Text = Resources.String_SaveButton;
         cancelButton.Text = Resources.String_CancelButton;
      }

      /// <summary>
      ///    Raises the <see cref = "E:System.Windows.Forms.Form.Load" /> event.
      /// </summary>
      /// <param name = "e">An <see cref = "T:System.EventArgs" /> that contains the event data.</param>
      protected override void OnLoad( EventArgs e )
      {
         profileNameTextBox.Text = _profile.ProfileName;
         isDefaultCheckBox.Checked = _profile.IsDefault;
      }

      /// <summary>
      ///    Cancel button "Click" handler
      /// </summary>
      /// <param name = "sender">The sender.</param>
      /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
      private void CancelButtonClick( object sender, EventArgs e )
      {
         DialogResult = DialogResult.Cancel;
         Close();
      }

      /// <summary>
      ///    Save button "Click" handler
      /// </summary>
      /// <param name = "sender">The sender.</param>
      /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
      private void SaveButtonClick( object sender, EventArgs e )
      {
         _profile.ProfileName = profileNameTextBox.Text;
         _profile.IsDefault = isDefaultCheckBox.Checked;
         DialogResult = DialogResult.OK;
         Close();
      }

      /// <summary>
      ///    ProfileName textbox "Validating" event handler - enforces a non-empty profile name.e
      /// </summary>
      /// <param name = "sender">The sender.</param>
      /// <param name = "e">The <see cref = "System.ComponentModel.CancelEventArgs" /> instance containing the event data.</param>
      private void ProfileNameTextBoxValidating( object sender, CancelEventArgs e )
      {
         profileNameTextBox.BackColor = SystemColors.Window;
         if ( string.IsNullOrEmpty( profileNameTextBox.Text ) )
         {
            e.Cancel = true;
            profileNameTextBox.BackColor = Color.LightPink;
         }
      }
   }
}