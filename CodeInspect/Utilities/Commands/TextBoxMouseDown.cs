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
    public class TextBoxMouseDown
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(TextBoxMouseDown),
            new PropertyMetadata(OnSetCommandCallback));

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached(
            "CommandParameter",
            typeof(object),
            typeof(TextBoxMouseDown),
            new PropertyMetadata(OnSetCommandParameterCallback));

        private static readonly DependencyProperty SelectCommandBehaviorProperty =
            DependencyProperty.RegisterAttached(
            "SelectCommandBehavior",
            typeof(object),
            typeof(TextBoxMouseDown),
            null);

        private static void OnSetCommandCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = dependencyObject as TextBox;

            if (tb != null)
            {
                TextBoxMouseDownCommandBehavior behavior = GetOrCreateBehavior(tb);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        private static void OnSetCommandParameterCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = dependencyObject as TextBox;

            if (tb != null)
            {
                TextBoxMouseDownCommandBehavior behavior = GetOrCreateBehavior(tb);
                behavior.CommandParameter = e.NewValue;
            }
        }

        private static TextBoxMouseDownCommandBehavior GetOrCreateBehavior(TextBox textBox)
        {
            TextBoxMouseDownCommandBehavior behavior =
                textBox.GetValue(SelectCommandBehaviorProperty) as TextBoxMouseDownCommandBehavior;

            if (behavior == null)
            {
                behavior = new TextBoxMouseDownCommandBehavior(textBox);
                textBox.SetValue(SelectCommandBehaviorProperty, behavior);
            }

            return behavior;
        }

        public static ICommand GetCommand(TextBox textBox)
        {
            return textBox.GetValue(CommandProperty) as ICommand;
        }

        public static void SetCommand(TextBox textBox, ICommand command)
        {
            textBox.SetValue(CommandProperty, command);
        }

        public static object GetCommandParameter(TextBox textBox)
        {
            return textBox.GetValue(CommandParameterProperty);
        }

        public static void SetCommandParameter(TextBox textBox, object parameter)
        {
            textBox.SetValue(CommandParameterProperty, parameter);
        }
    }



}
