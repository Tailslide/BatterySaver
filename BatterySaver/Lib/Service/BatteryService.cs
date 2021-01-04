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
using System.Runtime.InteropServices;

using BatterySaver.Lib.Utility;

namespace BatterySaver.Lib.Service
{
   /// <summary>
   ///   A service class for get batter and power information
   /// </summary>
   public class BatteryService : IBatteryService
   {
      /// <summary>
      /// Gets the detailed system power status
      /// </summary>
      /// <returns>A <see cref="SystemPowerStatus"/></returns>
      public SystemPowerStatus GetSystemPowerStatus()
      {
         SystemPowerStatus powerStatus = new SystemPowerStatus();
         Win32.GetSystemPowerStatus( powerStatus );
         return powerStatus;
      }

      /// <summary>
      /// Gets the value indicating whether or not the system is running on battery
      /// </summary>
      public bool OnBattery
      {
         get { return GetSystemPowerStatus().ACLineStatus != AcLineStatus.Online; }
      }

      /// <summary>
      /// Gets the value indicating whether or not the system is running on AC
      /// </summary>
      public bool OnAcPower
      {
         get { return GetSystemPowerStatus().ACLineStatus == AcLineStatus.Online; }
      }

      /// <summary>
      /// Gets a value indicating whether the batter/power subsystem is in a valid/known state.
      /// </summary>
      /// <value>
      /// 	<c>true</c> if in valid state; otherwise, <c>false</c>.
      /// </value>
      public bool IsValidState
      {
         get
         {
            var powerStatus = GetSystemPowerStatus();
            return powerStatus.ACLineStatus != AcLineStatus.Unknown && Enum.IsDefined( typeof( BatteryFlag ), powerStatus.BatteryFlag );
         }
      }
   }

   /// <summary>
   /// The AC Line status
   /// </summary>
   public enum AcLineStatus : byte
   {
      /// <summary>
      /// Offline
      /// </summary>
      Offline = 0,
      
      /// <summary>
      /// Online
      /// </summary>
      Online = 1,

      /// <summary>
      /// Unknown/invalid
      /// </summary>
      Unknown = 255
   }

   /// <summary>
   /// The flag indicating battery status
   /// </summary>
   public enum BatteryFlag : byte
   {
      BetweenHighAndLow=0,
      /// <summary>
      /// High—the battery capacity is at more than 66 percent
      /// </summary>
      High = 1,

      /// <summary>
      /// Low—the battery capacity is at less than 33 percent
      /// </summary>
      Low = 2,

      /// <summary>
      /// Critical—the battery capacity is at less than five percent
      /// </summary>
      Critical = 4,

      /// <summary>
      /// Charging
      /// </summary>
      Charging = 8,

      /// <summary>
      /// No system battery
      /// </summary>
      NoSystemBattery = 128,

      /// <summary>
      /// Unknown status—unable to read the battery flag information
      /// </summary>
      Unknown = 255
   }

   [StructLayout( LayoutKind.Sequential )]
   public class SystemPowerStatus
   {
      /// <summary>
      /// The AC power status.
      /// </summary>
      public AcLineStatus ACLineStatus;

      /// <summary>
      /// The battery charge status
      /// </summary>
      public BatteryFlag BatteryFlag;

      /// <summary>
      /// The percentage of full battery charge remaining. This member can be a value in the range 0 to 100, or 255 if status is unknown.
      /// </summary>
      public Byte BatteryLifePercent;

      /// <summary>
      /// Reserved; must be zero.
      /// </summary>
      public Byte Reserved1;

      /// <summary>
      /// Number of seconds of life remaining, -1 if unknown
      /// </summary>
      public Int32 BatteryLifeTime;

      /// <summary>
      /// The number of seconds of battery life when at full charge, or –1 if full battery lifetime is unknown.
      /// </summary>
      public Int32 BatteryFullLifeTime;
   }
}