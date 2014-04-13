using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInspectEntities
{
    public class IssueWithDescription
    {
        public Issue Issue { get; private set; }
        public IssueType IssueType { get; private set; }
        public ReportInformation ParentReportDetails { get; private set; }

        public IssueWithDescription(Issue issue, IssueType issueType, ReportInformation additionalData)
        {
            this.Issue = issue;
            this.IssueType = issueType;
            this.ParentReportDetails = additionalData;
        }
    }
}
