using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CodeInspectEntities
{
    [XmlRoot]
    public class Report
    {
        [XmlElement]
        public ReportInformation Information { get; set; }

        [XmlArray]
        [XmlArrayItem("IssueType", typeof(IssueType))]
        public List<IssueType> IssueTypes { get; set; }

        [XmlArrayAttribute("Issues")]
        [XmlArrayItemAttribute("Project", typeof(ProjectIssues))]
        public List<ProjectIssues> ProjectIssues { get; set; }
    }
}
