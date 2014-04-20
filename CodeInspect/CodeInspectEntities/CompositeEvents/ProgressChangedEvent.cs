using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInspectEntities.CompositeEvents
{
    public class ProgressChangedEvent : CompositePresentationEvent<string>
    {

    }

    public class ProgressChangedEventArgs : EventArgs
    {
        public string ProgressMessage { get; set; }
    }
}
