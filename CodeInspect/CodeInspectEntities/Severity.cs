using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CodeInspectEntities
{
    public enum Severity
    {
        [XmlEnum(Name = "ERROR")]
        Error,

        [XmlEnum(Name = "WARNING")]
        Warning,

        [XmlEnum(Name = "SUGGESTION")]
        Suggestion,

        [XmlEnum(Name = "HINT")]
        Hint,
    }
}
