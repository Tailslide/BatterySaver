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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using BatterySaver.Lib;
using BatterySaver.Lib.Configuration;
using BatterySaver.Lib.Extensions;
using BatterySaver.Lib.Service;
using BatterySaver.Properties;
using BatterySaver.UI;

using Microsoft.Win32;

namespace BatterySaver
{
   /// <summary>
   ///    The application initalization class
   /// </summary>
   internal static class BatterySaverCore
   {
      private static ProfileBatteryEventHandler _profileBatteryEventHandler;
      private static readonly IBatteryService _batteryService;
      private static string _lastConfigurationFile;
      private static string _lastProfile;

      static BatterySaverCore()
      {
         _batteryService = new BatteryService();

         // Remove the event listener on application exit
         Application.ApplicationExit += ( sender, e ) =>
                                           {
                                              SystemEvents.PowerModeChanged -= OnSystemEventsOnPowerModeChanged;
                                              SystemTray.Instance.Dispose();
                                           };
      }

      /// <summary>
      ///    Initializes the application with the specified configuration file.
      /// </summary>
      /// <remarks>
      ///    This method needs to be callable multiple times since it is called by "reload" actions.
      /// </remarks>
      /// <param name = "configurationFile">The configuration file.</param>
      /// <param name = "requestedProfileName">Name of the requested profile.</param>
      public static void Initialize( string configurationFile, string requestedProfileName )
      {
         // Initialize the profile service
         var profileService = ReadConfiguration( configurationFile );

         // Kill any other running copies - this allows the user to rerun the app to 
         // reload the configuration
         KillOtherCopies();

         // Wire up power change delegate
         Profile activeProfile = GetActiveProfile( profileService, requestedProfileName );
         _profileBatteryEventHandler = new ProfileBatteryEventHandler( activeProfile );
         _profileBatteryEventHandler.StartBatteryLevelMonitor( _batteryService );

         // Wire into the system power change event (WM_POWERBROADCAST)
         SystemEvents.PowerModeChanged -= OnSystemEventsOnPowerModeChanged;
         SystemEvents.PowerModeChanged += OnSystemEventsOnPowerModeChanged;

         // Remember the last settings so we can "reload"
         _lastConfigurationFile = configurationFile;
         _lastProfile = activeProfile.ProfileName;

         // Show the systray icon
         DisplaySystemTrayIcon( profileService );

         // Reduce our memory footprint
         ProcessService.TrimCurrentWorkingSet();
      }

      private static ProfileService ReadConfiguration( string configurationFile )
      {
         using ( Stream fileStream = File.OpenRead( configurationFile ) )
         {
            var configurationReader = new ConfigurationReader( new StreamReader( fileStream ) );
            configurationReader.LoadConfiguration();
            return new ProfileService( configurationReader );
         }
      }

      /// <summary>
      /// Persists the changes to the last known configuration file.
      /// </summary>
      /// <param name="profileService">The profile service.</param>
      public static void PersistChanges( ProfileService profileService )
      {
         PersistChanges( _lastConfigurationFile, profileService );
      }

      /// <summary>
      /// Persists the changes to the given configuration file.
      /// </summary>
      /// <param name="configurationFile">The configuration file to which changes should be persisted.</param>
      /// <param name="profileService">The profile service.</param>
      private static void PersistChanges( string configurationFile,ProfileService profileService )
      {
         using ( Stream fileStream = File.Open( configurationFile, FileMode.Truncate, FileAccess.Write ) )
         {
            var configurationWriter = new ConfigurationWriter( profileService );
            configurationWriter.WriteConfiguration( fileStream );
         }
      }

      private static void DisplaySystemTrayIcon( ProfileService profileService )
      {
         // Create actions for the context menu
         SystemTray.Instance.DispatchTable.Clear();

         // Force event submenu
         SystemTray.Instance.DispatchTable.Add( new KeyValuePair<string, Action>( Resources.String_ForceEvent + " >", null ) );
         Enum.GetValues( typeof ( EventType ) )
            .Cast<EventType>()
            .Each( eventType => SystemTray.Instance.DispatchTable.Add( new KeyValuePair<string, Action>( string.Format( "{0}", eventType ), () => _profileBatteryEventHandler.ExecuteActionsForEvent( eventType ) ) ) );
         SystemTray.Instance.DispatchTable.Add( new KeyValuePair<string, Action>( "<", null ) );
         SystemTray.Instance.DispatchTable.Add( new KeyValuePair<string, Action>( "-", null ) );

         // Profile chooser
         profileService.GetProfileList().Each( p => SystemTray.Instance.DispatchTable.Add( new KeyValuePair<string, Action>( Resources.String_LoadProfile + ": " + p.ProfileName, () => Initialize( _lastConfigurationFile, p.ProfileName ) ) ) );
         SystemTray.Instance.DispatchTable.Add( new KeyValuePair<string, Action>( "-", null ) );

         // Reload configuration
         SystemTray.Instance.DispatchTable.Add( new KeyValuePair<string, Action>( Resources.String_ReloadConfiguration + " (" + _lastProfile + ")", () => Initialize( _lastConfigurationFile, _lastProfile ) ) );
         SystemTray.Instance.DispatchTable.Add( new KeyValuePair<string, Action>( "-", null ) );

         // Options..
         SystemTray.Instance.DispatchTable.Add( new KeyValuePair<string, Action>( Resources.String_DisplayConfigurationEditor, () => new ConfigurationEditorForm( ReadConfiguration( _lastConfigurationFile ) ).Visible = true ) );
         SystemTray.Instance.DispatchTable.Add( new KeyValuePair<string, Action>( "-", null ) );

         // Exit
         SystemTray.Instance.DispatchTable.Add( new KeyValuePair<string, Action>( Resources.String_Exit, Application.Exit ) );

         // Display the icon
         SystemTray.Instance.CreateSystemTrayIcon( BuildProductName(), new Icon( Resources.Icon_Application, 16, 16 ) );

         SystemTray.Instance.ShowBallonTip( string.Format( Resources.String_ProfileLoaded, _lastProfile ), 1000 );
      }

      private static string BuildProductName()
      {
         Assembly executingAssembly = Assembly.GetExecutingAssembly();
         var version = executingAssembly.GetName().Version;
         return string.Format( "{0} v{1}.{2}.{3}", executingAssembly.GetName().Name, version.Major, version.Minor, version.Revision );
      }

      private static Profile GetActiveProfile( ProfileService profileService, string requestedProfileName )
      {
         var profile = string.IsNullOrEmpty( requestedProfileName ) ? profileService.LoadDefaultProfile() : profileService.LoadNamedProfile( requestedProfileName );
         if ( profile == null )
         {
            throw new InvalidOperationException( Resources.Error_NoProfilesFound );
         }
         return profile;
      }

      private static void OnSystemEventsOnPowerModeChanged( object sender, PowerModeChangedEventArgs e )
      {
         if ( e.Mode == PowerModes.StatusChange )
         {
            _profileBatteryEventHandler.HandlePowerStateChange( _batteryService );
         }
      }

      private static void KillOtherCopies()
      {
         var processes = ProcessService.FindProcessesByExeName( Path.GetFileName( Application.ExecutablePath ) );
         processes.Each( p =>
                            {
                               if ( Process.GetCurrentProcess().Id != p.Id )
                               {
                                  p.Kill();
                               }
                            } );
      }
   }
}