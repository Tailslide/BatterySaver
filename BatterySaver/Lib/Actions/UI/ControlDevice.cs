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

namespace BatterySaver.Lib.Actions.UI
{
   public partial class ControlDevice : BaseUiComponent
   {
      public ControlDevice()
      {
         InitializeComponent();
         actionComboBox.Items.Clear();
         foreach ( Actions.ControlDevice.ControlDeviceAction controlServiceAction in Enum.GetValues( typeof( Actions.ControlDevice.ControlDeviceAction ) ) )
         {
            actionComboBox.Items.Add( controlServiceAction );
         }
      }

      public override IActionUiComponent LoadAction( IAction action )
      {
         base.LoadAction( action );

         Action = action;
         var controlDeviceAction = action as Actions.ControlDevice;
         deviceClassIdTextBox.Text = Convert.ToString( controlDeviceAction.DeviceClassId );
         instancePathTextBox.Text = controlDeviceAction.DeviceInstanceId;
         actionComboBox.SelectedIndex = ( int )controlDeviceAction.Action;
         return this;
      }

      public override IActionUiComponent Save()
      {
         base.Save();

         var action = Action as Actions.ControlDevice;
         if ( action == null )
         {
            action = new Actions.ControlDevice();
         }
         action.DeviceClassId = new Guid( deviceClassIdTextBox.Text );
         action.Action = ( Actions.ControlDevice.ControlDeviceAction )actionComboBox.SelectedIndex;
         action.DeviceInstanceId = instancePathTextBox.Text;
         return this;
      }

      private void DeviceClassIdTextBoxValidating( object sender, System.ComponentModel.CancelEventArgs e )
      {
         MarkValid( sender );
         if ( deviceClassIdTextBox.Text == "" )
         {
            e.Cancel = true;
            MarkError( sender, "Device ClassID is required" );
         }
      }

      private void InstancePathTextBoxValidating( object sender, System.ComponentModel.CancelEventArgs e )
      {
         MarkValid( sender );
         if ( instancePathTextBox.Text == "" )
         {
            e.Cancel = true;
            MarkError( sender, "Device Instance Path is required" );
         }
      }
   }
}