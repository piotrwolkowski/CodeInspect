using CommandUI.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandUI.ModuleDefinitions
{
    public class CommandModule : IModule
    {
        IRegionManager regionManager;
        public CommandModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion("CommandRegion",
                typeof(CommandView));    
        }
    }
}
