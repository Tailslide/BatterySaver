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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BatterySaver.UI
{
   /// <summary>
   ///    A helper class for working with the system tray
   /// </summary>
   internal class SystemTray : IDisposable
   {
      private readonly IList<KeyValuePair<string, Action>> _dispatchTable;
      private readonly NotifyIcon _notifyIcon;
      private static readonly SystemTray _instance;

      /// <summary>
      ///    Initializes the <see cref = "SystemTray" /> class.
      /// </summary>
      static SystemTray()
      {
         _instance = new SystemTray();
      }

      /// <summary>
      ///    Initializes a new instance of the <see cref = "SystemTray" /> class.
      /// </summary>
      private SystemTray()
      {
         _notifyIcon = new NotifyIcon();
         _dispatchTable = new List<KeyValuePair<string, Action>>();
      }

      /// <summary>
      ///    Releases unmanaged resources and performs other cleanup operations before the
      ///    <see cref = "SystemTray" /> is reclaimed by garbage collection.
      /// </summary>
      ~SystemTray()
      {
         Dispose();
      }

      /// <summary>
      ///    Gets the instance.
      /// </summary>
      /// <value>The instance.</value>
      public static SystemTray Instance
      {
         get { return _instance; }
      }

      /// <summary>
      ///    Gets the dispatch table.
      /// </summary>
      /// <value>The dispatch table.</value>
      public IList<KeyValuePair<string, Action>> DispatchTable
      {
         get { return _dispatchTable; }
      }

      /// <summary>
      ///    Creates the system tray icon.
      /// </summary>
      /// <param name = "name">The name.</param>
      /// <param name = "icon">The icon.</param>
      public void CreateSystemTrayIcon( string name, Icon icon )
      {
         var rootMenu = new ContextMenu();
         Menu parent = rootMenu;
         foreach ( var keyValuePair in _dispatchTable )
         {
            Action contextAction = keyValuePair.Value;
            string caption = keyValuePair.Key;
            if ( caption.EndsWith( ">" ) )
            {
               var subMenu = new MenuItem( caption.Replace( ">", "" ).Trim() );
               parent.MenuItems.Add( subMenu );
               parent = subMenu;
               continue;
            }
            if ( caption == "<" )
            {
               parent = rootMenu;
               continue;
            }
            parent.MenuItems.Add( caption, ( sender, e ) => contextAction() );
         }

         _notifyIcon.ContextMenu = rootMenu;
         _notifyIcon.Text = name;
         _notifyIcon.Icon = icon;
         _notifyIcon.Visible = true;
      }

      /// <summary>
      ///    Shows the ballon tip.
      /// </summary>
      /// <param name = "text">The text.</param>
      /// <param name = "timeout">The timeout.</param>
      public void ShowBallonTip( string text, int timeout )
      {
         _notifyIcon.BalloonTipText = text;
         _notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
         _notifyIcon.BalloonTipTitle = _notifyIcon.Text;
         _notifyIcon.ShowBalloonTip( timeout );
      }

      /// <summary>
      ///    Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
      /// </summary>
      public void Dispose()
      {
         _notifyIcon.Dispose();
      }
   }
}