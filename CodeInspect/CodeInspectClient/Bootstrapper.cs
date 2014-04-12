using InvestigateCodeUI.ModuleDefinitions;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CodeInspectClient
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return new Shell();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            //return base.CreateModuleCatalog();

            //Uri catalogUri = new Uri("catalog.xaml", UriKind.Relative);
            //IModuleCatalog moduleCatalog = Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(catalogUri);

            IModuleCatalog moduleCatalog = new ModuleCatalog();

            Type investigateCodeModule = typeof(InvestigateCodeModule);
            moduleCatalog.AddModule(new ModuleInfo() { ModuleName = investigateCodeModule.Name, ModuleType = investigateCodeModule.AssemblyQualifiedName });

            return moduleCatalog;
            //return Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(
            //    new Uri("catalog.xaml", UriKind.Relative));
        }
    }
}
