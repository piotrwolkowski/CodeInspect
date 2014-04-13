using IssueListUI.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueListUI.ModuleDefinitions
{
    public class IssueListModule : IModule
    {
        IRegionManager regionManager;
        public IssueListModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion("IssueListRegion",
                typeof(IssueListView));
        }
    }
}
