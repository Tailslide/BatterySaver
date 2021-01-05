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

namespace BatterySaver.Lib.Configuration
{
   /// <summary>
   ///    A reader for processing configuration from an input stream
   /// </summary>
   public class ConfigurationReader
   {
      private const string XPATH_LOG = "/profiles/LogToFile";
      private const string XPATH_PROFILES = "/profiles/profile";
      private const string XPATH_EVENTHANDLER_ACTION = "eventHandlers/{0}/*";

      private XmlDocument _xmlDocument;
      private readonly StreamReader _inputStream;
      private IList<Profile> _profileList;

      /// <summary>
      ///    Initializes a new instance of the <see cref = "ConfigurationReader" /> class.
      /// </summary>
      /// <param name = "inputStream">The input stream.</param>
      public ConfigurationReader( StreamReader inputStream )
      {
         _inputStream = inputStream;
      }

      /// <summary>
      ///    Fully reads the stream and loads the configuration.
      /// </summary>
      public void LoadConfiguration()
      {
         GetXml();
         GetLogToFile();
      }
      
      public bool GetLogToFile()
      {
            var logNode = GetXml().SelectSingleNode(XPATH_LOG);
            if (logNode != null)
            {
                var enabled = logNode.Attributes["enabled"];
                if (enabled != null)
                {
                    return bool.Parse(enabled.Value);
                }
                else return false;
            }
            else return false;
        }

      /// <summary>
      ///    Gets the profiles from the configuration
      /// </summary>
      /// <returns>A list of <see cref = "Profile" />s.</returns>
      public IList<Profile> GetProfiles()
      {
         if ( _profileList == null )
         {
            var profileNodes = GetXml().SelectNodes( XPATH_PROFILES );
            _profileList = new List<Profile>();
            if ( profileNodes != null )
            {
               foreach ( XmlNode profileNode in profileNodes )
               {
                  var profileName = profileNode.Attributes["name"].Value;
                  Profile profile = new Profile( profileName );
                  profile.IsDefault = Convert.ToBoolean( profileNode.GetAttributeValue( "default" ) );
                  _profileList.Add( profile );

                  // Load the actions for the profile);
                  foreach ( var value in Enum.GetValues( typeof ( EventType ) ) )
                  {
                     IEnumerable<IAction> profileActions = GetProfileActions( profileNode, (EventType)value );
                     profile.AssociateActions( ( EventType )value, profileActions );
                  }
               }
            }
         }
         return _profileList;
      }

      /// <summary>
      ///    Gets the actions for the given <paramref name = "eventType" /> for the given <paramref name = "profileNode" />.
      /// </summary>
      /// <param name = "profileNode">The profile node.</param>
      /// <param name = "eventType">Type of the event.</param>
      /// <returns>A list of <see cref = "IAction" />s for the given <paramref name = "eventType" />, or an empty list.</returns>
      private static IEnumerable<IAction> GetProfileActions( XmlNode profileNode, EventType eventType )
      {
         string eventTypeString = eventType.ToString();
         eventTypeString = eventTypeString[0].ToString().ToLower() + eventTypeString.Substring( 1 );

         var nodeList = profileNode.SelectNodes( string.Format( XPATH_EVENTHANDLER_ACTION, eventTypeString ) );
         IList<IAction> actionList = new List<IAction>();
         if ( nodeList != null )
         {
            foreach ( XmlNode node in nodeList )
            {
               IDictionary<string, string> parameters = ( from XmlAttribute xmlAttribute
                                                             in node.Attributes
                                                          where xmlAttribute.Name != "type"
                                                          select new {key = xmlAttribute.Name, value = xmlAttribute.InnerText}
                                                        ).ToDictionary( item => item.key, item => item.value );

               // Create and add the action
               if ( node.Name == "action")
               {
                  // Generic action, use the "type" attribute to figure out the type
                  actionList.Add( ActionFactory.Create( node.Attributes["type"].InnerText, parameters ) );
               }
               else
               {
                  // Specific action; the node is the name of the action
                  actionList.Add( ActionFactory.Create( node.Name, parameters ) );
               }
            }
         }
         return actionList;
      }

      /// <summary>
      ///    Gets the <see cref = "XmlDocument" /> DOM for the given config.
      /// </summary>
      /// <returns>An <see cref = "XmlDocument" /> based on the input stream's contents</returns>
      private XmlDocument GetXml()
      {
         if ( _xmlDocument == null )
         {
            _xmlDocument = new XmlDocument();
            _xmlDocument.LoadXml( _inputStream.ReadToEnd() );
         }
         return _xmlDocument;
      }
   }
}