using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CodeInspectEntities
{
    public class ReportInformation
    {
        [XmlElement]
        public string Solution { get; set; }

        [XmlElement("InspectionScope")]
        public ReportInformationInspectionScope InspectionScope { get; set; }
    }
}
