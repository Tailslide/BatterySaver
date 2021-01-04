using System.Windows.Forms;

using BatterySaver.Lib.Actions;

namespace BatterySaver.UI.Controls
{
   /// <summary>
   /// A profile event action tree node
   /// </summary>
   internal class ProfileEventActionTreeNode : TreeNode
   {
      private readonly IAction _action;

      /// <summary>
      ///    Gets the action.
      /// </summary>
      /// <value>The action.</value>
      public IAction Action
      {
         get { return _action; }
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ProfileEventActionTreeNode"/> class.
      /// </summary>
      /// <param name="action">The action.</param>
      /// <param name="iconKey">The icon key.</param>
      public ProfileEventActionTreeNode( IAction action, string iconKey )
         : base( action.Description )
      {
         _action = action;
         ImageKey = iconKey;
         SelectedImageKey = ImageKey;
      }

      /// <summary>
      /// Refreshes this node's label.
      /// </summary>
      public void RefreshLabel()
      {
         Text = _action.Description;
      }
   }
}