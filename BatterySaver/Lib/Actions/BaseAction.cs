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
using System.Reflection;
using System.Text;

using BatterySaver.Lib.Extensions;

namespace BatterySaver.Lib.Actions
{
   public abstract class BaseAction : IAction
   {
      private byte _batteryPercentMin = 0;
      private byte _batteryPercentMax = 100;
      private string _name;

      /// <summary>
      ///    Gets or sets a value indicating whether this instance has executed.
      /// </summary>
      /// <value>
      ///    <c>true</c> if this instance has executed; otherwise, <c>false</c>.
      /// </value>
      public bool HasExecuted { get; set; }

      /// <summary>
      ///    Resets this instance.
      /// </summary>
      public void Reset()
      {
         HasExecuted = false;
      }

      /// <summary>
      ///    Gets the friendly name of the action.
      /// </summary>
      /// <value>The friendly name.</value>
      [ActionConfigParameter()]
      public virtual string Description
      {
         get { return _name ?? ( _name = GetType().Name ); }
         set { _name = value; }
      }

      /// <summary>
      ///    Executes this action
      /// </summary>
      public virtual void Execute()
      {
         HasExecuted = true;
      }

      /// <summary>
      ///    Gets the battery percent threshold minimum value.
      /// </summary>
      /// <value>The battery percent threshold minimum value.</value>
      [ActionConfigParameter]
      public virtual byte BatteryPercentMin
      {
         get { return _batteryPercentMin; }
         set { _batteryPercentMin = value; }
      }

      /// <summary>
      ///    Gets the battery percent threshold maximum value.
      /// </summary>
      /// <value>The battery percent threshold maximum value.</value>
      [ActionConfigParameter]
      public virtual byte BatteryPercentMax
      {
         get { return _batteryPercentMax; }
         set { _batteryPercentMax = value; }
      }

      /// <summary>
      ///    Sets the parameters for this action
      /// </summary>
      /// <param name = "parameters">The parameters</param>
      /// <param name = "validate">If <c>true</c>, validate the parameters.</param>
      public virtual void SetParameters( IDictionary<string, string> parameters, bool validate )
      {
         // If we have no parameters, default it to an empty collection
         if ( parameters == null )
         {
            parameters = new Dictionary<string, string>();
         }

         // Process properties
         var type = GetType();
         var properties = type.GetProperties( BindingFlags.Public | BindingFlags.Instance );
         IDictionary<string, string> results = new Dictionary<string, string>();
         foreach ( var propertyInfo in properties )
         {
            var attributes = propertyInfo.GetCustomAttributes( typeof ( ActionConfigParameterAttribute ), true );
            if ( attributes != null && attributes.Length > 0 )
            {
               var parameterName = propertyInfo.Name.LowerFirst();
               if ( !parameters.ContainsKey( parameterName ) )
               {
                  if ( validate && ( attributes[0] as ActionConfigParameterAttribute ).IsRequired )
                  {
                     // Parameter is required but not supplied
                     throw new InvalidOperationException( "Parameter " + parameterName + " is required" );
                  }
               }
               else
               {
                  string parameterValue = parameters[parameterName];
                  object value;
                  if ( propertyInfo.PropertyType.IsEnum )
                  {
                     // The property is an Enum; parse the value
                     value = Enum.Parse( propertyInfo.PropertyType, parameterValue, true );
                  }
                  else if ( propertyInfo.PropertyType == typeof ( Guid ) )
                  {
                     value = new Guid( parameterValue );
                  }
                  else
                  {
                     // The property is a standard type; convert
                     value = Convert.ChangeType( parameterValue, propertyInfo.PropertyType );
                  }
                  propertyInfo.SetValue( this, value, null );
               }
            }
         }
      }

      /// <summary>
      ///    Gets the parameters for this action.
      /// </summary>
      /// <returns>A dictionary of parameter values for this action.</returns>
      public virtual IDictionary<string, string> GetParameters()
      {
         var type = GetType();
         var properties = type.GetProperties( BindingFlags.Public | BindingFlags.Instance );
         IDictionary<string, string> results = new Dictionary<string, string>();
         foreach ( var propertyInfo in properties )
         {
            var attributes = propertyInfo.GetCustomAttributes( typeof ( ActionConfigParameterAttribute ), true );
            if ( attributes != null && attributes.Length > 0 )
            {
               results[propertyInfo.Name] = Convert.ToString( propertyInfo.GetValue( this, null ) );
            }
         }
         return results;
      }
   }
}