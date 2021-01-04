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

using BatterySaver.Lib.Actions;

namespace BatterySaver.Lib
{
   /// <summary>
   ///    A configuration profile.  A profile has a list of actions, these actions will be
   ///    executed when the profile is active.
   /// </summary>
   public class Profile
   {
      private readonly IDictionary<EventType, IList<IAction>> _eventActionCollection;

      /// <summary>
      ///    Initializes a new instance of the <see cref = "Profile" /> class.
      /// </summary>
      /// <param name = "profileName">Name of the profile.</param>
      public Profile( string profileName )
      {
         ProfileName = profileName;

         // Initialize event type action buckets
         var eventTypes = Enum.GetValues( typeof ( EventType ) );
         _eventActionCollection = new Dictionary<EventType, IList<IAction>>( eventTypes.Length );
         foreach ( EventType eventType in eventTypes )
         {
            _eventActionCollection[eventType] = new List<IAction>();
         }
      }

      /// <summary>
      ///    Gets or sets a value indicating whether this is the default profile.
      /// </summary>
      /// <value>
      ///    <c>true</c> if this profile is the default; otherwise, <c>false</c>.
      /// </value>
      public bool IsDefault { get; set; }

      /// <summary>
      ///    Gets or sets the name of the profile.
      /// </summary>
      /// <value>The name of the profile.</value>
      public string ProfileName { get; set; }

      /// <summary>
      ///    Executes the actions for the given event type.
      /// </summary>
      /// <param name = "eventType">Type of the event.</param>
      public IEnumerable<IAction> GetActionsForEvent( EventType eventType )
      {
         return _eventActionCollection[eventType];
      }

      /// <summary>
      /// Removes the action from event.
      /// </summary>
      /// <param name="eventType">Type of the event.</param>
      /// <param name="actionToRemove">The action to remove.</param>
      public void RemoveActionFromEvent( EventType eventType, IAction actionToRemove )
      {
         _eventActionCollection[eventType].Remove(actionToRemove);
      }

      /// <summary>
      ///    Associates the given <paramref name = "action" /> to the given <paramref name = "eventType" />.
      /// </summary>
      /// <param name = "eventType">Type of the event.</param>
      /// <param name = "action">The action to associate with the event.</param>
      public void AssociateAction( EventType eventType, IAction action )
      {
         _eventActionCollection[eventType].Add( action );
      }

      /// <summary>
      ///    Associates the given <paramref name = "actions" /> to the given <paramref name = "eventType" />.
      /// </summary>
      /// <param name = "eventType">Type of the event.</param>
      /// <param name = "actions">The actions to associate with the event.</param>
      public void AssociateActions( EventType eventType, IEnumerable<IAction> actions )
      {
         ( _eventActionCollection[eventType] as List<IAction> ).AddRange( actions );
      }
   }
}