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

using BatterySaver.Lib.Configuration;

namespace BatterySaver.Lib.Service
{
   /// <summary>
   ///    A service for handling profiles
   /// </summary>
   public class ProfileService
   {
      private readonly ConfigurationReader _configuration;

      /// <summary>
      ///    Initializes a new instance of the <see cref = "ProfileService" /> class.
      /// </summary>
      /// <param name = "configuration">The configuration reader.</param>
      public ProfileService( ConfigurationReader configuration )
      {
         _configuration = configuration;
      }

      /// <summary>
      /// Gets the profile list.
      /// </summary>
      /// <returns></returns>
      public IList<Profile> GetProfileList()
      {
         return _configuration.GetProfiles();
      }

      /// <summary>
      ///    Loads the named profile.
      /// </summary>
      /// <param name = "profileName">Name of the profile.</param>
      /// <returns>The profile with the given <paramref name = "profileName" /> or null if profiles doesn't exit</returns>
      public Profile LoadNamedProfile( string profileName )
      {
         var profileList = _configuration.GetProfiles();
         return profileList.FirstOrDefault( p => p.ProfileName == profileName );
      }

      /// <summary>
      ///    Loads the default profile.
      /// </summary>
      /// <returns>The default profile or null if none exists.</returns>
      public Profile LoadDefaultProfile()
      {
         var profileList = _configuration.GetProfiles();
         if ( profileList.Count == 1 )
         {
            return profileList[0];
         }
         return profileList.FirstOrDefault( p => p.IsDefault );
      }
   }
}