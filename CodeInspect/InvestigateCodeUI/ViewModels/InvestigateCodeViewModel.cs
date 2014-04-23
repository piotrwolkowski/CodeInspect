using CodeInspectEntities;
using CodeInspectEntities.CompositeEvents;
using CodeInspectInterfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Utilities;

namespace InvestigateCodeUI.ViewModels
{
    public class InvestigateCodeViewModel : NotifyPropertyChangedImplementation
    {
        private readonly string CANCEL_TEXT = "Cancel";
        private readonly string GET_ISSUES_TEXT = "Get Issues";

        private CancellationTokenSource cancelGetIssues;

        private IEventAggregator eventAggregator;

        private ICodeInspectService codeInspectService;
        private bool isRequestingIssues;
        public bool IsRequestingIssues
        {
            get
            {
                return this.isRequestingIssues;
            }
            set
            {
                if (this.isRequestingIssues != value)
                {
                    this.isRequestingIssues = value;
                    if (this.isRequestingIssues)
                    {
                        this.GetIssuesButtonText = CANCEL_TEXT;
                    }
                    else
                    {
                        this.GetIssuesButtonText = GET_ISSUES_TEXT;
                    }
                }
            }
        }

        private string getIssuesButtonText;
        public string GetIssuesButtonText
        {
            get
            {
                return this.getIssuesButtonText;
            }
            set
            {
                this.getIssuesButtonText = value;
                this.OnPropertyChanged();
            }
        }

        private ICommand getProjectPathCommand;
        public ICommand GetProjectPathCommand
        {
            get
            {
                if (this.getProjectPathCommand == null)
                {
                    this.getProjectPathCommand = new DelegateCommand
                        (
                            this.GetProjectPath, 
                            () => { return !this.IsRequestingIssues; }
                        );
                }
                return this.getProjectPathCommand;
            }
            private set
            {
                this.getProjectPathCommand = value;
            }
        }

        private ICommand getIssuesCommand;
        public ICommand GetIssuesCommand
        {
            get
            {
                if (this.getIssuesCommand == null)
                {
                    this.getIssuesCommand = new DelegateCommand
                        (
                            this.GetIssues,
                            () => { return !string.IsNullOrWhiteSpace(this.projectToInvestigatePath); }
                        );
                }
                return this.getIssuesCommand;
            }
            private set
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

                ((DelegateCommand)this.GetIssuesCommand).RaiseCanExecuteChanged();
            }
        }

        public InvestigateCodeViewModel(ICodeInspectService codeInspectService, IEventAggregator eventAggregator)
        {
            this.GetIssuesButtonText = GET_ISSUES_TEXT;
            this.codeInspectService = codeInspectService;

            ReportCreatedEvent reportCreatedEvent = eventAggregator.GetEvent<ReportCreatedEvent>();
            reportCreatedEvent.Subscribe(OnReportCreated);
        }

        private void GetIssues()
        {
            if (!this.IsRequestingIssues)
            {
                this.IsRequestingIssues = true;
                this.cancelGetIssues = new CancellationTokenSource();

                this.codeInspectService.CreateReport(projectToInvestigatePath, this.cancelGetIssues.Token);
            }
            else
            {
                this.cancelGetIssues.Cancel();
                this.IsRequestingIssues = false;
            }
        }

        private void GetProjectPath()
        {
            this.ProjectToInvestigatePath = this.RequestFileTypePath("sln", "csproj", "xml");
        }

        /// <summary>
        /// A method that invokes an open file dialog with filters for provided file types.
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        private string RequestFileTypePath(params string[] fileType)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            if (fileType.Count() > 0)
            {
                openFile.DefaultExt = fileType[0];
                string filters = string.Join("|", fileType.Select(f => string.Format("{0} (*.{0})|*{0}", f)));
                openFile.Filter = filters;
            }
            DialogResult result = openFile.ShowDialog();

            if (result == DialogResult.OK)
            {
                return openFile.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        private void OnReportCreated(Report report)
        {
            this.IsRequestingIssues = false;
        }
    }
}