using CodeInspectInterfaces;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInspectSettings.ModuleDefinitions
{
    public class SettingsModule : IModule
    {
        IUnityContainer container;
        public SettingsModule(IUnityContainer container)
        {
            this.container = container;
        }
        public void Initialize()
        {
            this.container.RegisterInstance<ICodeInspectSettings>(new Settings());
        }
    }
}
