using IssueSelectionUI.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueSelectionUI.ModuleDefinitions
{
    public class IssueSelectionModule : IModule
    {
        IRegionManager regionManager;
        public IssueSelectionModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion("IssueSelectionRegion",
                typeof(IssueSelectionView));
        }
    }
}
