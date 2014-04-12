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
    internal class InspectProject
    {

        internal static Report CreateReport(string projectPath, CancellationToken cancellationToken, IProgress<string> progress, ICodeInspectSettings codeInspectSettings)
        {
            throw new NotImplementedException();
        }
    }
}
