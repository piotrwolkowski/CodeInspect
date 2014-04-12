using InvestigateCodeUI.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestigateCodeUI.ModuleDefinitions
{
    public class InvestigateCodeModule : IModule
    {
        IRegionManager regionManager;
        public InvestigateCodeModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion("InvestigateCodeRegion",
                typeof(InvestigateCodeView));
        }
    }
}
