﻿#region Copyright © 2010, Ryan Emerle; All rights reserved

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

namespace BatterySaver.Lib.Actions
{
   /// <summary>
   ///    The factory responsible for creating instances of actions
   /// </summary>
   public class ActionFactory
   {
      private static readonly IDictionary<string, Type> _validActionCollection;

      static ActionFactory()
      {
         _validActionCollection = FindActions( Assembly.GetExecutingAssembly() );
      }

      /// <summary>
      ///    Gets the valid action collection.
      /// </summary>
      /// <value>The valid action collection.</value>
      public static IDictionary<string, Type> ValidActionCollection
      {
         get { return _validActionCollection; }
      }

      /// <summary>
      ///    Creates an instance of the given <paramref name = "actionType" /> with the given <paramref name = "parameters" />
      /// </summary>
      /// <param name = "actionType">The name of tye action to crate</param>
      /// <param name = "parameters">The parameters to set on the action</param>
      /// <returns></returns>
      public static IAction Create( string actionType, IDictionary<string, string> parameters )
      {
         return Create( actionType, parameters, true );
      }

      /// <summary>
      /// Creates an instance of the given <paramref name="actionType"/> with the given <paramref name="parameters"/>
      /// </summary>
      /// <param name="actionType">The name of tye action to crate</param>
      /// <param name="parameters">The parameters to set on the action</param>
      /// <param name="validate">if set to <c>true</c> [validate].</param>
      /// <returns></returns>
      public static IAction Create( string actionType, IDictionary<string, string> parameters, bool validate )
      {
         IAction action = null;
         if ( _validActionCollection.ContainsKey( actionType ) )
         {
            action = Activator.CreateInstance( _validActionCollection[actionType] ) as IAction;
            if ( action != null )
            {
               action.SetParameters( parameters, validate );
            }
         }
         return action;
      }

      /// <summary>
      ///    Finds all of the types in the given <paramref name = "assembly" /> that implement <see cref = "IAction" />
      /// </summary>
      /// <param name = "assembly">The assembly containing actions.</param>
      /// <returns>A collection of <see cref = "Type" />s keyed on the type name</returns>
      private static IDictionary<string, Type> FindActions( Assembly assembly )
      {
         var types = assembly.GetTypes();
         IDictionary<string, Type> actionCollection = new Dictionary<string, Type>();
         foreach ( var type in types )
         {
            if ( !type.IsAbstract && type.GetInterfaces().Contains( typeof( IAction ) ) )
            {
               // Implements IAction
               actionCollection.Add( type.Name, type );
            }
         }
         return actionCollection;
      }
   }
}