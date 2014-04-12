using CodeInspectInterfaces;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInspectService.ModuleDefinitions
{
    class CodeInspectModule : IModule
    {
        IUnityContainer container;

        public CodeInspectModule(IUnityContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            this.container.RegisterType<ICodeInspectService, CodeInspectService.Services.CodeInspectService>();
        }
    }
}
