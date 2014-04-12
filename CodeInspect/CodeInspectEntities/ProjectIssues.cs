using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CodeInspectEntities
{
    public class ProjectIssues
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement("Issue", typeof(Issue))]
        public List<Issue> Issues { get; set; }
    }
}
