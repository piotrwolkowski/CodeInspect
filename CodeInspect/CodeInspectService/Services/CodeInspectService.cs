using CodeInspectEntities;
using CodeInspectEntities.CompositeEvents;
using CodeInspectInterfaces;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeInspectService.Services
{
    public class CodeInspectService : ICodeInspectService
    {
        Report report;

        public event EventHandler<ReportCreatedEventArgs> ReportCreated;

        ICodeInspectSettings settings;

        IEventAggregator eventAggregator;

        public CodeInspectService(ICodeInspectSettings settings, IEventAggregator eventAggregator)
        {
            this.settings = settings;
            this.eventAggregator = eventAggregator;
        }

        public async void CreateReport(string projectPath, CancellationToken cancellationToken, IProgress<string> progress)
        {
            this.report = await InspectProject.CreateReport(projectPath, cancellationToken, progress, this.settings);
            OnReportCreated(this.report);
        }

        public bool SaveAsXml(string saveLocation)
        {
            throw new NotImplementedException();
        }

        public List<KeyValuePair<string, decimal>> GetIssueTypeStats()
        {
            throw new NotImplementedException();
        }

        public List<KeyValuePair<string, decimal>> GetIssueSeverityStats()
        {
            throw new NotImplementedException();
        }

        public List<KeyValuePair<string, decimal>> GetIssueFileStats()
        {
            throw new NotImplementedException();
        }

        private void OnReportCreated(Report report)
        {
            ReportCreatedEvent reportCreatedEvent = eventAggregator.GetEvent<ReportCreatedEvent>();
            reportCreatedEvent.Publish(report);
        }
    }
}
