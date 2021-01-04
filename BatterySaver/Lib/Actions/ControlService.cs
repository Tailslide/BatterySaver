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
using System.ServiceProcess;

namespace BatterySaver.Lib.Actions
{
   /// <summary>
   ///    An action that allows for the starting and stopping of windows services
   /// </summary>
   ///<remarks>
   ///   <list type = "table">
   ///      <listheader>
   ///         <term>Attribute</term>
   ///         <description>Description</description>
   ///      </listheader>   
   ///      <item>
   ///         <term>serviceName</term>
   ///         <description>The service name or friendly name</description>
   ///      </item>  
   ///      <item>
   ///         <term>action</term>
   ///         <description>[start|stop]</description>
   ///      </item>  
   ///   </list>
   ///   <i>* - Optional</i>
   ///</remarks>
   public sealed class ControlService : BaseAction
   {
      private string _serviceName;
      private ControlServiceAction _action;

      public enum ControlServiceAction
      {
         Start,
         Stop,
      }

      /// <summary>
      /// Gets or sets the name of the service.
      /// </summary>
      /// <value>The name of the service.</value>
      [ActionConfigParameter( IsRequired = true )]
      public string ServiceName
      {
         get { return _serviceName; }
         set { _serviceName = value; }
      }

      /// <summary>
      /// Gets or sets the action.
      /// </summary>
      /// <value>The action.</value>
      [ActionConfigParameter( IsRequired = true )]
      public ControlServiceAction Action
      {
         get { return _action; }
         set { _action = value; }
      }

      /// <summary>
      /// Executes this action
      /// </summary>
      public override void Execute()
      {
         // Perform base execute
         base.Execute();

         ServiceController[] services = ServiceController.GetServices();
         foreach ( var service in services )
         {
            using ( service )
            {
               if ( service.ServiceName == _serviceName || service.DisplayName == _serviceName )
               {
                  try
                  {
                     switch ( _action )
                     {
                        case ControlServiceAction.Start:
                           if ( service.Status != ServiceControllerStatus.StartPending &&
                                service.Status != ServiceControllerStatus.Running )
                           {
                              service.Start();
                           }
                           break;
                        case ControlServiceAction.Stop:
                           if ( service.CanStop && service.Status != ServiceControllerStatus.Stopped &&
                                service.Status != ServiceControllerStatus.StopPending )
                           {
                              service.Stop();
                           }
                           break;
                     }
                  }
                  catch ( InvalidOperationException )
                  {
                     // Drop exception
                  }
               }
            }
         }
      }
   }
}