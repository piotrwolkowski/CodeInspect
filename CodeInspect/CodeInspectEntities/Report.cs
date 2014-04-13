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


        private List<ProjectIssues> projectIssues;
        [XmlArrayAttribute("Issues")]
        [XmlArrayItemAttribute("Project", typeof(ProjectIssues))]
        public List<ProjectIssues> ProjectIssues
        {
            get
            {
                return this.projectIssues;
            }

            set
            {
                this.projectIssues = value;
                //AllIssues = new List<IssueWithDescription>(this.projectIssues
                //    .SelectMany(p => p.Issues
                //        .Select(i =>
                //            {
                //                return new IssueWithDescription
                //                (
                //                    i,
                //                    IssueTypes.Single(it => it.Id == i.TypeId),
                //                    Information
                //                );
                //            })));
            }
        }

        [XmlIgnore]
        public List<IssueWithDescription> AllIssues { get; set; }
    }
}
