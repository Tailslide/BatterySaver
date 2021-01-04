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

using BatterySaver.Lib;
using BatterySaver.Lib.Actions;
using BatterySaver.Lib.Service;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace BatterySaverTests
{
   /// <summary>
   ///    Tests of the <see cref = "ProfileBatteryEventHandler" /> class
   /// </summary>
   [TestClass]
   public class TestProfileBatteryEventHandler
   {
      /// <summary>
      ///    Tests that actions are executed when the state change DC => AC, occurs
      /// </summary>
      [TestMethod]
      public void TestThatAllActionsGetExecutedOnStateChangeToAc()
      {
         var profile = new Profile( "testProfile" );

         // Build action list
         var actionList = new List<IAction>();
         var mock = new Mock<BaseAction> {CallBase = true};
         actionList.Add( mock.Object );

         // Register the actions with the profile
         profile.AssociateActions( EventType.SwitchToAc, actionList );

         // Create battery service
         var mockBatteryServce = new Mock<IBatteryService>();
         mockBatteryServce.Setup( b => b.OnAcPower ).Returns( true );
         mockBatteryServce.Setup( b => b.IsValidState ).Returns( true );
         mockBatteryServce.Setup( b => b.GetSystemPowerStatus() ).Returns( new SystemPowerStatus {BatteryLifePercent = 100} );

         // Handle the "event"
         var profileBatteryEventHandler = new ProfileBatteryEventHandler( profile );
         profileBatteryEventHandler.HandlePowerStateChange( mockBatteryServce.Object );

         // Verify that our action was executed
         mock.Verify( a => a.Execute(), Times.Once() );
      }

      /// <summary>
      ///    Tests that actions are executed when the state change AC => DC, occurs
      /// </summary>
      [TestMethod]
      public void TestThatAllActionsGetExecutedOnStateChangeToDc()
      {
         var profile = new Profile( "testProfile" );

         // Build action list
         var actionList = new List<IAction>();
         var mock = new Mock<BaseAction> {CallBase = true};
         actionList.Add( mock.Object );

         // Register the actions with the profile
         profile.AssociateActions( EventType.SwitchToBattery, actionList );

         // Create battery service
         var mockBatteryServce = new Mock<IBatteryService>();
         mockBatteryServce.Setup( b => b.OnBattery ).Returns( true );
         mockBatteryServce.Setup( b => b.IsValidState ).Returns( true );
         mockBatteryServce.Setup( b => b.GetSystemPowerStatus() ).Returns( new SystemPowerStatus {BatteryLifePercent = 100} );

         // Handle the "event"
         var profileBatteryEventHandler = new ProfileBatteryEventHandler( profile );
         profileBatteryEventHandler.HandlePowerStateChange( mockBatteryServce.Object );

         // Verify that our action was executed
         mock.Verify( a => a.Execute(), Times.Once() );
      }

      /// <summary>
      ///    Tests that actions are executed iff the current battery power is between 10-50%
      /// </summary>
      [TestMethod]
      public void TestThatAllActionsWithinThresholdGetExecutedOnStateChangeToDc()
      {
         var profile = new Profile( "testProfile" );

         // Build action list
         var actionList = new List<IAction>();
         var mock = new Mock<BaseAction> {CallBase = true};
         mock.Setup( a => a.BatteryPercentMin ).Returns( 10 );
         mock.Setup( a => a.BatteryPercentMax ).Returns( 50 );
         actionList.Add( mock.Object );

         // Register the actions with the profile
         profile.AssociateActions( EventType.SwitchToBattery, actionList );

         // Create battery service
         var mockBatteryServce = new Mock<IBatteryService>();
         mockBatteryServce.Setup( b => b.OnBattery ).Returns( true );
         mockBatteryServce.Setup( b => b.IsValidState ).Returns( true );
         mockBatteryServce.Setup( b => b.GetSystemPowerStatus() ).Returns( new SystemPowerStatus {BatteryLifePercent = 100} );

         // Handle the "event"
         var profileBatteryEventHandler = new ProfileBatteryEventHandler( profile );
         profileBatteryEventHandler.HandlePowerStateChange( mockBatteryServce.Object );

         // Outside of 10-50% range; shouldn't be executed
         mock.Verify( a => a.Execute(), Times.Never() );

         // Move the battery level to 50%
         mockBatteryServce.Setup( b => b.GetSystemPowerStatus() ).Returns( new SystemPowerStatus {BatteryLifePercent = 50} );

         // Handle the "event"
         profileBatteryEventHandler = new ProfileBatteryEventHandler( profile );
         profileBatteryEventHandler.HandlePowerStateChange( mockBatteryServce.Object );

         // Within the range, it should be executed
         mock.Verify( a => a.Execute(), Times.Once() );
      }

      /// <summary>
      ///    Tests that actions are executed once, and only once, when the battery level increases
      /// </summary>
      [TestMethod]
      public void TestBatteryPercentageIncrease()
      {
         var profile = new Profile( "testProfile" );

         // Build action list
         var actionList = new List<IAction>();
         var mock = new Mock<BaseAction> {CallBase = true};
         actionList.Add( mock.Object );

         // Register the actions with the profile
         profile.AssociateActions( EventType.BatteryPercentIncreased, actionList );

         // Create battery service
         var mockBatteryServce = new Mock<IBatteryService>();
         mockBatteryServce.Setup( b => b.OnBattery ).Returns( true );
         mockBatteryServce.Setup( b => b.IsValidState ).Returns( true );
         mockBatteryServce.Setup( b => b.GetSystemPowerStatus() ).Returns( new SystemPowerStatus {BatteryLifePercent = 100} );

         // Handle the "event"
         var profileBatteryEventHandler = new ProfileBatteryEventHandler( profile );
         profileBatteryEventHandler.HandleBatteryLevelChange( mockBatteryServce.Object, 50, 1 );

         // Should execute once
         mock.Verify( a => a.Execute(), Times.Once() );

         // Should be marked as "HasExecuted"
         Assert.AreEqual( true, mock.Object.HasExecuted );

         // Handle the event again
         profileBatteryEventHandler.HandleBatteryLevelChange( mockBatteryServce.Object, 51, 1 );

         // The invocation count should still be 1
         mock.Verify( a => a.Execute(), Times.Once() );
      }

      /// <summary>
      ///    Tests that actions are executed once, and only once, when the battery level decreases
      /// </summary>
      [TestMethod]
      public void TestBatteryPercentageDecrease()
      {
         var profile = new Profile( "testProfile" );

         // Build action list
         var actionList = new List<IAction>();
         var mock = new Mock<BaseAction> {CallBase = true};
         actionList.Add( mock.Object );

         // Register the actions with the profile
         profile.AssociateActions( EventType.BatteryPercentDecreased, actionList );

         // Create battery service
         var mockBatteryServce = new Mock<IBatteryService>();
         mockBatteryServce.Setup( b => b.OnBattery ).Returns( true );
         mockBatteryServce.Setup( b => b.IsValidState ).Returns( true );
         mockBatteryServce.Setup( b => b.GetSystemPowerStatus() ).Returns( new SystemPowerStatus {BatteryLifePercent = 100} );

         // Handle the "event"
         var profileBatteryEventHandler = new ProfileBatteryEventHandler( profile );
         profileBatteryEventHandler.HandleBatteryLevelChange( mockBatteryServce.Object, 50, -1 );

         // Should execute once
         mock.Verify( a => a.Execute(), Times.Once() );

         // Should be marked as "HasExecuted"
         Assert.AreEqual( true, mock.Object.HasExecuted );

         // Handle the event again
         profileBatteryEventHandler.HandleBatteryLevelChange( mockBatteryServce.Object, 51, -1 );

         // The invocation count should still be 1
         mock.Verify( a => a.Execute(), Times.Once() );
      }
   }
}