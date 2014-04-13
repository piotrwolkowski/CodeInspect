using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Utilities.Commands
{
    public static class TreeViewSelect
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(TreeViewSelect),
            new PropertyMetadata(OnSetCommandCallback));

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached(
            "CommandParameter",
            typeof(object),
            typeof(TreeViewSelect),
            new PropertyMetadata(OnSetCommandParameterCallback));

        private static readonly DependencyProperty SelectCommandBehaviorProperty =
            DependencyProperty.RegisterAttached(
            "SelectCommandBehavior",
            typeof(object),
            typeof(TreeViewSelect),
            null);

        private static void OnSetCommandCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            TreeView tv = dependencyObject as TreeView;

            if (tv != null)
            {
                TreeViewSelectionChangedCommandBehavior behavior = GetOrCreateBehavior(tv);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        private static void OnSetCommandParameterCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            TreeView tv = dependencyObject as TreeView;

            if (tv != null)
            {
                TreeViewSelectionChangedCommandBehavior behavior = GetOrCreateBehavior(tv);
                behavior.CommandParameter = e.NewValue;
            }
        }

        private static TreeViewSelectionChangedCommandBehavior GetOrCreateBehavior(TreeView treeView)
        {
            TreeViewSelectionChangedCommandBehavior behavior =
                treeView.GetValue(SelectCommandBehaviorProperty) as TreeViewSelectionChangedCommandBehavior;

            if (behavior == null)
            {
                behavior = new TreeViewSelectionChangedCommandBehavior(treeView);
                treeView.SetValue(SelectCommandBehaviorProperty, behavior);
            }

            return behavior;
        }

        public static ICommand GetCommand(TreeView treeView)
        {
            return treeView.GetValue(CommandProperty) as ICommand;
        }

        public static void SetCommand(TreeView treeView, ICommand command)
        {
            treeView.SetValue(CommandProperty, command);
        }

        public static object GetCommandParameter(TreeView treeView)
        {
            return treeView.GetValue(CommandParameterProperty);
        }

        public static void SetCommandParameter(TreeView treeView, object parameter)
        {
            treeView.SetValue(CommandParameterProperty, parameter);
        }
    }
}
