using CodeInspectInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInspectSettings
{
    public class Settings : ICodeInspectSettings
    {
        public string InspectCodePath
        {
            get { return @"C:\Program Files (x86)\JetBrains\jb-commandline-8.1.23.523\inspectcode.exe"; }
        }

        public bool SolutionWideAnalysis
        {
            get { throw new NotImplementedException(); }
        }

        public bool TreatWarningsAsErrors
        {
            get { throw new NotImplementedException(); }
        }
    }
}
