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
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using BatterySaver.Lib.Actions;
using BatterySaver.Lib.Extensions;
using BatterySaver.Lib.Service;

namespace BatterySaver.Lib.Configuration
{
   /// <summary>
   /// The configuration writer; writes configuration out to a stream
   /// </summary>
   public class ConfigurationWriter
   {
      private readonly ProfileService _profileService;
      private const string NAMESPACE_URI = null;

      /// <summary>
      /// Initializes a new instance of the <see cref="ConfigurationWriter"/> class.
      /// </summary>
      /// <param name="profileService">The profile service.</param>
      public ConfigurationWriter( ProfileService profileService )
      {
         _profileService = profileService;
      }

      /// <summary>
      /// Writes the configuration.
      /// </summary>
      /// <param name="outputStream">The output stream.</param>
      public void WriteConfiguration( Stream outputStream )
      {
         var xmlDocument = CreateXmlDocument();

         // Create the writer
         var settings = new XmlWriterSettings();
         settings.Indent = true;
         settings.IndentChars = "   ";

         // Write
         using ( var outputWriter = XmlWriter.Create( outputStream, settings ) )
         {
            xmlDocument.WriteTo( outputWriter );
         }
      }

      private XmlDocument CreateXmlDocument()
      {
         var xmlDocument = new XmlDocument();
         var rootNode = xmlDocument.CreateNode( XmlNodeType.Element, "profiles", NAMESPACE_URI );
         xmlDocument.AppendChild( rootNode );

         var profileList = _profileService.GetProfileList();
         foreach ( var profile in profileList )
         {
            rootNode.AppendChild( CreateProfileNode( rootNode, profile ) );
         }
         return xmlDocument;
      }

      private static XmlElement CreateProfileNode( XmlNode parentNode, Profile profile )
      {
         var document = parentNode.OwnerDocument;

         // Build the profile node
         var profileNode = document.CreateNode( XmlNodeType.Element, "profile", NAMESPACE_URI ) as XmlElement;
         profileNode.SetAttribute( "name", profile.ProfileName );
         profileNode.SetAttribute( "default", Convert.ToString( profile.IsDefault ) );

         // Add event handlers
         profileNode.AppendChild( CreateEventHandlersNode( profileNode, profile ) );
         return profileNode;
      }

      private static XmlNode CreateEventHandlersNode( XmlNode parentNode, Profile profile )
      {
         var document = parentNode.OwnerDocument;

         // Build the eventHandlers node
         var eventHandlersNode = document.CreateNode( XmlNodeType.Element, "eventHandlers", NAMESPACE_URI ) as XmlElement;
         foreach ( EventType eventType in Enum.GetValues( typeof ( EventType ) ) )
         {
            var eventTypeNode = document.CreateNode( XmlNodeType.Element, Convert.ToString( eventType ).LowerFirst(), NAMESPACE_URI ) as XmlElement;
            eventHandlersNode.AppendChild( eventTypeNode );
            foreach ( var action in profile.GetActionsForEvent( eventType ) )
            {
               eventTypeNode.AppendChild( CreateActionNode( eventTypeNode, action ) );
            }
         }
         return eventHandlersNode;
      }

      private static XmlNode CreateActionNode( XmlNode parentNode, IAction action )
      {
         var document = parentNode.OwnerDocument;
         var actionNode = document.CreateNode( XmlNodeType.Element, "action", NAMESPACE_URI ) as XmlElement;
         var parameters = action.GetParameters();
         actionNode.SetAttribute( "type", action.GetType().Name );
         foreach ( var parameter in parameters )
         {
            actionNode.SetAttribute( parameter.Key.LowerFirst(), parameter.Value );
         }
         return actionNode;
      }
   }
}