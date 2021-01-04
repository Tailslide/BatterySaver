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
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

using BatterySaver.Lib.Utility;

namespace BatterySaver.Lib.Service
{
   /// <summary>
   /// Utility/service class for interacting with devices
   /// </summary>
   /// <remarks>
   /// Adapted from http://jo0ls-dotnet-stuff.blogspot.com/2009/01/enabledisable-device-programmatically.html     
   /// </remarks>
   public static class DeviceService
   {
      /// <summary>
      /// Enable or disable a device.
      /// </summary>
      /// <param name="classGuid">The class guid of the device. Available in the device manager.</param>
      /// <param name="instanceId">The device instance id of the device. Available in the device manager.</param>
      /// <param name="enable">True to enable, False to disable.</param>
      /// <remarks>Will throw an exception if the device is not Disableable.</remarks>
      public static void SetDeviceEnabled( Guid classGuid, string instanceId, bool enable )
      {
         SafeDeviceInfoSetHandle deviceInfoSetHandle = null;
         try
         {
            // Get the handle to a device information set for all devices matching classGuid that are present on the 
            // system.
            deviceInfoSetHandle = Win32.SetupDiGetClassDevs( ref classGuid, null, IntPtr.Zero, SetupDiGetClassDevsFlags.Present );

            // Get the device information data for each matching device.
            DeviceInfoData[] deviceInfoData = GetDeviceInfoData( deviceInfoSetHandle );

            // Find the index of our instance. i.e. the actual device
            int instanceIndex = GetIndexOfInstance( deviceInfoSetHandle, deviceInfoData, instanceId );

            // Disable...
            EnableDevice( deviceInfoSetHandle, deviceInfoData[instanceIndex], enable );
         }
         finally
         {
            if ( deviceInfoSetHandle != null )
            {
               if ( deviceInfoSetHandle.IsClosed == false )
               {
                  deviceInfoSetHandle.Close();
               }
               deviceInfoSetHandle.Dispose();
            }
         }
      }

      private static DeviceInfoData[] GetDeviceInfoData( SafeDeviceInfoSetHandle handle )
      {
         List<DeviceInfoData> deviceInfoDataList = new List<DeviceInfoData>();
         DeviceInfoData deviceInfoData = new DeviceInfoData();
         int deviceInfoDataStructSize = Marshal.SizeOf( deviceInfoData );
         deviceInfoData.Size = deviceInfoDataStructSize;
         int index = 0;
         while ( Win32.SetupDiEnumDeviceInfo( handle, index, ref deviceInfoData ) )
         {
            deviceInfoDataList.Add( deviceInfoData );
            index += 1;
            deviceInfoData = new DeviceInfoData { Size = deviceInfoDataStructSize };
         }
         return deviceInfoDataList.ToArray();
      }

      // Find the index of the particular DeviceInfoData for the instanceId.
      private static int GetIndexOfInstance( SafeDeviceInfoSetHandle deviceInfoSetHandle,
                                             DeviceInfoData[] deviceInfoData, string instanceId )
      {
         const int insufficientBufferError = 122;
         for ( int deviceInfoIndex = 0; deviceInfoIndex <= deviceInfoData.Length - 1; deviceInfoIndex++ )
         {
            StringBuilder buffer = new StringBuilder( 1 );
            int requiredSize;
            bool result = Win32.SetupDiGetDeviceInstanceId( deviceInfoSetHandle.DangerousGetHandle(),
                                                            ref deviceInfoData[deviceInfoIndex], buffer,
                                                            buffer.Capacity, out requiredSize );
            if ( result == false && Marshal.GetLastWin32Error() == insufficientBufferError )
            {
               buffer.Capacity = requiredSize;
               result = Win32.SetupDiGetDeviceInstanceId( deviceInfoSetHandle.DangerousGetHandle(),
                                                          ref deviceInfoData[deviceInfoIndex], buffer,
                                                          buffer.Capacity, out requiredSize );
            }
            if ( result == false )
            {
               throw new Win32Exception();
            }
            if ( instanceId.Equals( buffer.ToString() ) )
            {
               return deviceInfoIndex;
            }
         }
         // not found
         return -1;
      }

      private static void EnableDevice( SafeDeviceInfoSetHandle handle, DeviceInfoData diData, bool enable )
      {
         PropertyChangeParameters propertyChangeParameters = new PropertyChangeParameters
                                                                 {
                                                                    Size = 8,
                                                                    DiFunction = DiFunction.PropertyChange,
                                                                    Scope = Scopes.Global,
                                                                    StateChange =
                                                                        enable
                                                                            ? StateChangeAction.Enable
                                                                            : StateChangeAction.Disable
                                                                 };

         // Set the parameters
         bool result = Win32.SetupDiSetClassInstallParams( handle, ref diData, ref propertyChangeParameters,
                                                           Marshal.SizeOf( propertyChangeParameters ) );
         if ( result == false )
         {
            throw new Win32Exception();
         }

         // Apply the parameters
         result = Win32.SetupDiCallClassInstaller( DiFunction.PropertyChange, handle, ref diData );
         if ( result == false )
         {
            int err = Marshal.GetLastWin32Error();
            if ( err == ( int )SetupApiError.NotDisableable )
            {
               throw new ArgumentException( "Device can't be disabled (programmatically or in Device Manager)." );
            }
            if ( err <= ( int )SetupApiError.NoAssociatedClass &&
                 err >= ( int )SetupApiError.OnlyValidateViaAuthenticode )
            {
               throw new Win32Exception( "SetupAPI error: " + ( ( SetupApiError )err ) );
            }
            throw new Win32Exception();
         }
      }
   }
}