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

namespace BatterySaver.Lib.Actions
{
   /// <summary>
   ///    The interface that all actions must implement
   /// </summary>
   public interface IAction
   {
      /// <summary>
      ///    Gets the description of the action.
      /// </summary>
      /// <value>The description.</value>
      string Description { get; set; }

      /// <summary>
      ///    Executes this action
      /// </summary>
      void Execute();

      /// <summary>
      ///    Gets the battery percent threshold minimum value.
      /// </summary>
      /// <value>The battery percent threshold minimum value.</value>
      byte BatteryPercentMin { get; set; }

      /// <summary>
      ///    Gets the battery percent threshold maximum value.
      /// </summary>
      /// <value>The battery percent threshold maximum value.</value>
      byte BatteryPercentMax { get; set; }

      /// <summary>
      ///    Gets or sets a value indicating whether this instance has executed.
      /// </summary>
      /// <value>
      ///    <c>true</c> if this instance has executed; otherwise, <c>false</c>.
      /// </value>
      bool HasExecuted { get; set; }

      /// <summary>
      ///    Sets the parameters for this action
      /// </summary>
      /// <param name = "parameters">The parameters</param>
      /// <param name = "validate">If <c>true</c>, validate the parameters.</param>
      void SetParameters( IDictionary<string, string> parameters, bool validate );

      /// <summary>
      ///    Gets the parameters for this action.
      /// </summary>
      /// <returns>A dictionary of parameter values for this action.</returns>
      IDictionary<string, string> GetParameters();

      /// <summary>
      ///    Resets this instance.
      /// </summary>
      void Reset();
   }
}