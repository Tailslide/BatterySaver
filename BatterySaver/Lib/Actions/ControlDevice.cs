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

using BatterySaver.Lib.Service;

namespace BatterySaver.Lib.Actions
{
   ///<summary>
   ///   An action that allows for the enabling and disabling of devices
   ///</summary>
   ///<remarks>
   ///   <list type = "table">
   ///      <listheader>
   ///         <term>Attribute</term>
   ///         <description>Description</description>
   ///      </listheader>   
   ///      <item>
   ///         <term>deviceClassId</term>
   ///         <description>The GUID for the device class</description>
   ///      </item>  
   ///      <item>
   ///         <term>deviceInstanceId</term>
   ///         <description>The instance path of the device</description>
   ///      </item>  
   ///      <item>
   ///         <term>action</term>
   ///         <description>[enable|disable]</description>
   ///      </item>  
   ///   </list>
   ///   <i>* - Optional</i>
   ///</remarks>
   public class ControlDevice : BaseAction
   {
      private Guid _deviceClassId;
      private string _deviceInstanceId;
      private ControlDeviceAction _action;

      public enum ControlDeviceAction
      {
         Enable,
         Disable
      }

      /// <summary>
      ///    Gets or sets the device class id.
      /// </summary>
      /// <value>The device class id.</value>
      [ActionConfigParameter( IsRequired = true )]
      public Guid DeviceClassId
      {
         get { return _deviceClassId; }
         set { _deviceClassId = value; }
      }

      /// <summary>
      ///    Gets or sets the device instance id.
      /// </summary>
      /// <value>The device instance id.</value>
      [ActionConfigParameter( IsRequired = true )]
      public string DeviceInstanceId
      {
         get { return _deviceInstanceId; }
         set { _deviceInstanceId = value; }
      }

      /// <summary>
      ///    Gets or sets the action.
      /// </summary>
      /// <value>The action.</value>
      [ActionConfigParameter( IsRequired = true )]
      public ControlDeviceAction Action
      {
         get { return _action; }
         set { _action = value; }
      }

      /// <summary>
      ///    Executes this action
      /// </summary>
      public override void Execute()
      {
         // Perform base execute
         base.Execute();

         bool enabled = true;
         if ( _action == ControlDeviceAction.Disable )
         {
            enabled = false;
         }
         DeviceService.SetDeviceEnabled( _deviceClassId, _deviceInstanceId, enabled );
      }
   }
}