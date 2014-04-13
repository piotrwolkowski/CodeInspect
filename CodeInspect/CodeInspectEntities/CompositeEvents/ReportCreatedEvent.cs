using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;

namespace CodeInspectEntities.CompositeEvents
{
    public class ReportCreatedEvent : CompositePresentationEvent<Report>
    {
    }

    public class ReportCreatedEventArgs : EventArgs
    {
        public Report Report { get; set; }
    }
}
