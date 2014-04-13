using CodeInspectEntities;
using CodeInspectEntities.CompositeEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeInspectInterfaces
{
    public interface ICodeInspectService
    {
        event EventHandler<ReportCreatedEventArgs> ReportCreated;

        void CreateReport(string projectPath, CancellationToken cancellationToken, IProgress<string> progress);

        bool SaveAsXml(string saveLocation);

        List<KeyValuePair<string, decimal>> GetIssueTypeStats();

        List<KeyValuePair<string, decimal>> GetIssueSeverityStats();
        
        List<KeyValuePair<string, decimal>> GetIssueFileStats();
    }
}
