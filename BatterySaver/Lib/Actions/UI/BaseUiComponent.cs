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

namespace BatterySaver.Lib.Actions.UI
{
   public partial class BaseUiComponent : UserControl, IActionUiComponent
   {
      protected IAction Action { get; set; }

      public BaseUiComponent()
      {
         InitializeComponent();
      }

      /// <summary>
      ///    Gets the control.
      /// </summary>
      /// <value>The control.</value>
      public virtual Control Control
      {
         get { return this; }
      }

      /// <summary>
      ///    Loads the action.
      /// </summary>
      /// <param name = "action">The action.</param>
      /// <returns></returns>
      public virtual IActionUiComponent LoadAction( IAction action )
      {
         minimumBatteryPercentageSlider.Value = action.BatteryPercentMin;
         maximumBatteryPercentageSlider.Value = action.BatteryPercentMax;
         descriptionTextBox.Text = action.Description;
         return this;
      }

      /// <summary>
      ///    Saves this instance.
      /// </summary>
      /// <returns></returns>
      public virtual IActionUiComponent Save()
      {
         Action.BatteryPercentMin = (byte)minimumBatteryPercentageSlider.Value;
         Action.BatteryPercentMax = (byte)maximumBatteryPercentageSlider.Value;
         Action.Description = descriptionTextBox.Text;
         return this;
      }

      /// <summary>
      ///    Marks the given control as erroreous.
      /// </summary>
      /// <param name = "control">The control.</param>
      /// <param name = "message">The message.</param>
      protected void MarkError( object control, string message )
      {
         ( control as Control ).BackColor = Color.LightPink;
         toolTip.SetToolTip( control as Control, message );
      }

      /// <summary>
      ///    Marks the given control as valid.
      /// </summary>
      /// <param name = "control">The control.</param>
      protected void MarkValid( object control )
      {
         ( control as Control ).BackColor = SystemColors.Window;
         toolTip.SetToolTip( control as Control, null );
      }

      private void DescriptionTextBoxValidating( object sender, System.ComponentModel.CancelEventArgs e )
      {
         MarkValid( sender as Control );
         if ( descriptionTextBox.Text == "" )
         {
            MarkError( sender as Control, "Description is required" );
            e.Cancel = true;
         }
      }

      private void MinimumBatteryPercentageSliderValueChanged( object sender, EventArgs e )
      {
         // Prevent the minimum from exceeding the maximum
         if ( maximumBatteryPercentageSlider.Value < minimumBatteryPercentageSlider.Value )
         {
            maximumBatteryPercentageSlider.Value = minimumBatteryPercentageSlider.Value;
         }
      }

      private void MaximumBatteryPercentageSliderValueChanged( object sender, EventArgs e )
      {
         // Prevent the minimum from exceeding the maximum
         if ( maximumBatteryPercentageSlider.Value < minimumBatteryPercentageSlider.Value )
         {
            minimumBatteryPercentageSlider.Value = maximumBatteryPercentageSlider.Value;
         }
      }
   }
}