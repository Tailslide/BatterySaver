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
using System.Threading;

using BatterySaver.Lib.Extensions;
using BatterySaver.Lib.Service;

namespace BatterySaver.Lib
{
   /// <summary>
   ///    A battery event handler based on <see cref = "Profile" />s
   /// </summary>
   public class ProfileBatteryEventHandler
   {
      private const int INTERVAL_MS_BATTERY_STATUS_MONITOR = 5000;

      private readonly Profile _profile;
      private SystemPowerStatus _lastKnownState;
      private IBatteryService _batteryService;
      private EventType? _lastEvent;
      private EventType? _lastPowerEvent;
      private static Thread _monitorThread;

      /// <summary>
      ///    Initializes a new instance of the <see cref = "ProfileBatteryEventHandler" /> class.
      /// </summary>
      /// <param name = "profile">The profile.</param>
      public ProfileBatteryEventHandler( Profile profile )
      {
         _profile = profile;
      }

      /// <summary>
      ///    Handles a power state change.
      /// </summary>
      /// <param name = "batteryService">The battery service.</param>
      public void HandlePowerStateChange( IBatteryService batteryService )
      {
            // State change, make sure we're in a valid state
            if (batteryService.IsValidState)
            {
                if (_lastPowerEvent ==null 
                    // FSIGAP - just keep track of ac state changes.. battery level changes were causing battery/AC code to run
                     || _lastPowerEvent == EventType.SwitchToAc && batteryService.OnBattery // changed from ac to on battery
                     || _lastPowerEvent == EventType.SwitchToBattery && batteryService.OnAcPower // changes from battery to on ac
                     )
                {
                    EventType eventType;
                    if (batteryService.OnAcPower)
                        eventType = EventType.SwitchToAc;
                    else
                        eventType= EventType.SwitchToBattery;

                    // Check the event state
                    //ProcessStateChange(eventType);
                    _lastPowerEvent = eventType;
                    // Execute all of the actions for this event if they are within the threshold
                    var batteryLifePercent = batteryService.GetSystemPowerStatus().BatteryLifePercent;
                    _profile.GetActionsForEvent(eventType)
                       .TakeWhile(a => batteryLifePercent >= (a.BatteryPercentMin) && batteryLifePercent <= (a.BatteryPercentMax))
                       .Each(a => a.Execute());
                }
            }
      }

      /// <summary>
      ///    Executes all of the actions for the given <paramref name = "eventType" /> irrespective of the current power state.
      /// </summary>
      /// <param name = "eventType">Type of the event.</param>
      public void ExecuteActionsForEvent( EventType eventType )
      {
         _profile.GetActionsForEvent( eventType ).Each( a => a.Execute() );
      }

      /// <summary>
      ///    Handles a battery level change.
      /// </summary>
      /// <param name = "batteryService">The battery service.</param>
      /// <param name = "batteryLifePercent">The battery life percent.</param>
      /// <param name = "percentageDelta">The amout of change in percent.</param>
      public void HandleBatteryLevelChange( IBatteryService batteryService, int batteryLifePercent, int percentageDelta )
      {
         EventType eventType = EventType.BatteryPercentDecreased;
         if ( percentageDelta > 0 )
         {
            eventType = EventType.BatteryPercentIncreased;
         }

         // Check the event state
         ProcessStateChange( eventType );

         // Execute all of the actions that are within the battery threshold and have not yet been executed
         // NOTE: actions are only executed ONCE after they've reached their threshold
         _profile.GetActionsForEvent( eventType )
            .TakeWhile( a => !a.HasExecuted && batteryLifePercent >= ( a.BatteryPercentMin ) && batteryLifePercent <= ( a.BatteryPercentMax ) )
            .Each( a => a.Execute() );
      }

      /// <summary>
      ///    Starts the power state monitor.
      /// </summary>
      /// <remarks>
      ///    We could register for the power setting notifications via RegisterPowerSettingNotification, but this only availablein Vista+
      /// </remarks>
      /// <param name = "batteryService">The battery service.</param>
      public void StartBatteryLevelMonitor( IBatteryService batteryService )
      {
         if ( _monitorThread == null )
         {
            // Save the battery service
            _batteryService = batteryService;
                if (batteryService.OnAcPower)
                    _lastPowerEvent = EventType.SwitchToAc;
                else
                    _lastPowerEvent = EventType.SwitchToBattery;

                // Create monitoring thread
                var threadStart = new ThreadStart( MonitorBatteryLevel );
            _monitorThread = new Thread( threadStart ) { Name = "Batter State Monitor - BW Worker" };
            _monitorThread.Start();
         }
      }

      /// <summary>
      ///    Monitors the state.
      /// </summary>
      private void MonitorBatteryLevel()
      {
         while ( Thread.CurrentThread.IsAlive )
         {
            MonitorBatteryLevel( _batteryService );
            Thread.Sleep( INTERVAL_MS_BATTERY_STATUS_MONITOR );
         }
      }

      /// <summary>
      ///    Monitors the state.
      /// </summary>
      /// <param name = "batteryService">The battery service.</param>
      private void MonitorBatteryLevel( IBatteryService batteryService )
      {
         if ( _lastKnownState == null )
         {
            _lastKnownState = batteryService.GetSystemPowerStatus();
         }
         var currentState = batteryService.GetSystemPowerStatus();
         if ( currentState.BatteryLifePercent != _lastKnownState.BatteryLifePercent )
         {
            _lastKnownState = currentState;
            HandleBatteryLevelChange( batteryService, currentState.BatteryLifePercent, currentState.BatteryLifePercent - _lastKnownState.BatteryLifePercent );
            return;
         }
         _lastKnownState = currentState;
      }

      private void ProcessStateChange( EventType eventType )
      {
         if ( _lastEvent != null && eventType != _lastEvent )
         {
            // We have an event change, reset all of the actions (specifically we want to revert 
            // the HasExecuted flag so we will reexecute actions for battery percent events
            ResetActions();
         }
         _lastEvent = eventType;
      }

      private void ResetActions()
      {
         foreach ( EventType eventType in Enum.GetValues( typeof( EventType ) ) )
         {
            _profile.GetActionsForEvent( eventType ).Each( a => a.Reset() );
         }
      }
   }
}