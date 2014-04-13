using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace Utilities.Commands
{
    public class TreeViewSelectionChangedCommandBehavior : CommandBehaviorBase<TreeView>
    {
        public TreeViewSelectionChangedCommandBehavior(TreeView treeView)
            : base(treeView)
        {
            treeView.SelectedItemChanged += SelectionChangedHandler;
        }

        void SelectionChangedHandler(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ExecuteCommand();
        }
    }
}
