using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInspectEntities.CompositeEvents
{
    class SelectedIssueChangedEvent : CompositePresentationEvent<Issue>
    {
        
    }

    public class SelectedIssueChangedEventArgs : EventArgs
    {
        public Issue Issue { get; set; }
    }
}
