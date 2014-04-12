using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInspectInterfaces
{
    public interface ICodeInspectSettings
    {
        string InspectCodePath { get; }
        bool SolutionWideAnalysis { get; }
        bool TreatWarningsAsErrors { get; }
    }
}
