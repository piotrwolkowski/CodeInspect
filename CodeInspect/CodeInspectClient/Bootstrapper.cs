using CodeInspectService.ModuleDefinitions;
using CodeInspectSettings.ModuleDefinitions;
using InvestigateCodeUI.ModuleDefinitions;
using IssueListUI.ModuleDefinitions;
using IssueSelectionUI.ModuleDefinitions;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            IModuleCatalog moduleCatalog = new ModuleCatalog();

            Type settingsModule = typeof(SettingsModule);
            moduleCatalog.AddModule(new ModuleInfo() { ModuleName = settingsModule.Name, ModuleType = settingsModule.AssemblyQualifiedName });

            Type codeInspectModule = typeof(CodeInspectModule);
            moduleCatalog.AddModule(new ModuleInfo() { ModuleName = codeInspectModule.Name, ModuleType = codeInspectModule.AssemblyQualifiedName, DependsOn = new Collection<string>() { settingsModule.Name } });

            Type investigateCodeModule = typeof(InvestigateCodeModule);
            moduleCatalog.AddModule(new ModuleInfo() { ModuleName = investigateCodeModule.Name, ModuleType = investigateCodeModule.AssemblyQualifiedName, DependsOn = new Collection<string>() { codeInspectModule.Name } });

            Type issueListModule = typeof(IssueListModule);
            moduleCatalog.AddModule(new ModuleInfo() { ModuleName = issueListModule.Name, ModuleType = issueListModule.AssemblyQualifiedName, DependsOn = new Collection<string>() { investigateCodeModule.Name } });

            Type issueSelectionModule = typeof(IssueSelectionModule);
            moduleCatalog.AddModule(new ModuleInfo() { ModuleName = issueSelectionModule.Name, ModuleType = issueSelectionModule.AssemblyQualifiedName, DependsOn = new Collection<string>() { issueListModule.Name } });

            return moduleCatalog;
        }
    }
}
