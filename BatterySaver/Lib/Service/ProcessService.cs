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
using System.Diagnostics;

using BatterySaver.Lib.Utility;

namespace BatterySaver.Lib.Service
{
   /// <summary>
   ///    A utility/service class for interacting with and enumerating processes
   /// </summary>
   public static class ProcessService
   {
      /// <summary>
      ///    Gets the current process.
      /// </summary>
      /// <value>The current process.</value>
      public static Process CurrentProcess
      {
         get { return Process.GetCurrentProcess(); }
      }

      /// <summary>
      ///    Trims the working set.
      /// </summary>
      public static void TrimCurrentWorkingSet()
      {
         Win32.TrimWorkingSet( CurrentProcess );
      }

      /// <summary>
      ///    Finds processes by the name of the exe.
      /// </summary>
      /// <param name = "exeName">Name of the exe.</param>
      /// <returns>A list of processes that match the given <paramref name = "exeName" /></returns>
      public static IList<Process> FindProcessesByExeName( string exeName )
      {
         // process.MainModule can throw an AccessDenied error, so we check that that the ProcessName
         // is a subset of the exeName.
         return FindProcesses( process => ( exeName.Contains( process.ProcessName ) && process.MainModule != null && process.MainModule.ModuleName == exeName ) );
      }

      /// <summary>
      ///    Finds the processes that match the <paramref name = "matcher" /> predicate
      /// </summary>
      /// <param name = "matcher">The matcher predicate.</param>
      /// <returns>A list of processes for which the <paramref name = "matcher" /> predicate returns true.</returns>
      public static IList<Process> FindProcesses( Predicate<Process> matcher )
      {
         Process[] processes = Process.GetProcesses();
         IList<Process> matchingProcesses = new List<Process>();
         foreach ( var process in processes )
         {
            try
            {
               if ( matcher( process ) )
               {
                  matchingProcesses.Add( process );
               }
            }
            catch ( Win32Exception )
            {
               // Access denied; ignore
            }
         }
         return matchingProcesses;
      }
   }
}