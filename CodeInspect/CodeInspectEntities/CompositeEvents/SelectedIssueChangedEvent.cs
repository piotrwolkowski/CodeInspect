using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInspectEntities.CompositeEvents
{
    public class SelectedIssueChangedEvent : CompositePresentationEvent<IssueWithDescription>
    {
        
    }

    public class SelectedIssueChangedEventArgs : EventArgs
    {
        public IssueWithDescription IssueWithDescription { get; set; }
    }
}
