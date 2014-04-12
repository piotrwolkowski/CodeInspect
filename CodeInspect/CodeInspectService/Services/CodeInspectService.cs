using CodeInspectEntities;
using CodeInspectInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeInspectService.Services
{
    class CodeInspectService : ICodeInspectService
    {
        ICodeInspectSettings settings;

        public CodeInspectService(ICodeInspectSettings settings)
        {
            this.settings = settings;
        }

        public Report CreateReport(string projectPath, CancellationToken cancellationToken, IProgress<string> progress)
        {
            return InspectProject.CreateReport(projectPath, cancellationToken, progress, this.settings);
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
    }
}
