using CodeInspectEntities;
using CodeInspectInterfaces;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Utilities;

namespace InvestigateCodeUI.ViewModels
{
    public class InvestigateCodeViewModel : NotifyPropertyChangedImplementation
    {
        ICodeInspectService codeInspectService;

        private ICommand getIssuesCommand;
        public ICommand GetIssuesCommand
        {
            get
            {
                if (this.getIssuesCommand == null)
                {
                    this.getIssuesCommand = new DelegateCommand(this.GetIssues);
                }
                return this.getIssuesCommand;
            }
            set
            {
                this.getIssuesCommand = value;
            }
        }

        private string projectToInvestigatePath;
        public string ProjectToInvestigatePath
        {
            get
            {
                return this.projectToInvestigatePath;
            }
            set
            {
                this.projectToInvestigatePath = value;
                this.OnPropertyChanged();
            }
        }

        public InvestigateCodeViewModel(ICodeInspectService codeInspectService)
        {
            this.codeInspectService = codeInspectService;
        }

        private void GetIssues()
        {
            this.codeInspectService.CreateReport(projectToInvestigatePath, new CancellationToken(), null);
        }
    }
}
