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

        IProgress<string> progress;

        public CodeInspectService(ICodeInspectSettings settings, IEventAggregator eventAggregator)
        {
            this.settings = settings;
            this.eventAggregator = eventAggregator;
        }

        public async void CreateReport(string projectPath, CancellationToken cancellationToken)
        {
            this.progress = new Progress<string>((result) =>
                {
                    OnProgressChanged(result);
                });

            this.report = await InspectProject.CreateReport(projectPath, cancellationToken, this.progress, this.settings);
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

        private void OnProgressChanged(string progressMessage)
        {
            ProgressChangedEvent progressChangedEvent = eventAggregator.GetEvent<ProgressChangedEvent>();
            progressChangedEvent.Publish(progressMessage);
        }

        private void OnReportCreated(Report report)
        {
            ReportCreatedEvent reportCreatedEvent = eventAggregator.GetEvent<ReportCreatedEvent>();
            reportCreatedEvent.Publish(report);

            //if (this.ReportCreated != null)
            //{
            //    this.ReportCreated.Invoke(this, new ReportCreatedEventArgs() { Report = report });
            //}
        }
    }
}
