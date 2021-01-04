using System.Windows.Forms;

using BatterySaver.Lib;

namespace BatterySaver.UI.Controls
{
   /// <summary>
   /// A profile event tree ndoe
   /// </summary>
   internal class ProfileEventTreeNode : TreeNode
   {
      private readonly EventType _eventType;

      /// <summary>
      ///    Gets the type of the event.
      /// </summary>
      /// <value>The type of the event.</value>
      public EventType EventType
      {
         get { return _eventType; }
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ProfileEventTreeNode"/> class.
      /// </summary>
      /// <param name="eventType">Type of the event.</param>
      /// <param name="iconKey">The icon key.</param>
      public ProfileEventTreeNode( EventType eventType, string iconKey )
         : base( eventType.ToString() )
      {
         _eventType = eventType;
         ImageKey = iconKey;
         SelectedImageKey = ImageKey;
      }

      /// <summary>
      /// Adds the node.
      /// </summary>
      /// <param name="actionNode">The action node.</param>
      public void AddNode( ProfileEventActionTreeNode actionNode )
      {
         Nodes.Add( actionNode );
      }
   }
}