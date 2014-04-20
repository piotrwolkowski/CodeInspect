using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Utilities.Commands
{
    public class TextBoxMouseDownCommandBehavior : CommandBehaviorBase<TextBox>
    {
        public TextBoxMouseDownCommandBehavior(TextBox textBox)
            : base(textBox)
        {
            textBox.MouseDown += MouseDownHandler;
        }

        void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            ExecuteCommand();
        }
    }
}
