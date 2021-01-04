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
   ///   An action capable of killing processes
   ///</summary>
   ///<remarks>
   ///   <list type = "table">
   ///      <listheader>
   ///         <term>Attribute</term>
   ///         <description>Description</description>
   ///      </listheader>   
   ///      <item>
   ///         <term>processName</term>
   ///         <description>The name of the executable to be killed</description>
   ///      </item>  
   ///   </list>
   ///   <i>* - Optional</i>
   ///</remarks>
   public sealed class KillProcess : BaseAction
   {
      private string _processName;

      /// <summary>
      ///    Gets or sets the name of the process.
      /// </summary>
      /// <value>The name of the process.</value>
      [ActionConfigParameter( IsRequired = true)]
      public string ProcessName
      {
         get { return _processName; }
         set { _processName = value; }
      }

      /// <summary>
      ///    Executes this action
      /// </summary>
      public override void Execute()
      {
         // Perform base execute
         base.Execute();

         var processes = ProcessService.FindProcessesByExeName( _processName );
         foreach ( var process in processes )
         {
            process.Kill();
         }
      }
   }
}