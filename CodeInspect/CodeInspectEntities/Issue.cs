using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CodeInspectEntities
{
    public class Issue
    {
        [XmlAttribute]
        public string Message { get; set; }

        [XmlAttribute]
        public int Line { get; set; }

        [XmlAttribute]
        public string Offset { get; set; }

        [XmlAttribute]
        public string File { get; set; }

        [XmlAttribute]
        public string TypeId { get; set; }
    }
}
