using CodeInspectEntities;
using CodeInspectEntities.CompositeEvents;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace IssueListUI.ViewModels
{
    public class IssueListViewModel : NotifyPropertyChangedImplementation
    {

        private ObservableCollection<ProjectIssues> projectIssues;
        public ObservableCollection<ProjectIssues> ProjectIssues
        {
            get
            {
                return this.projectIssues;
            }
            set
            {
                this.projectIssues = value;
                OnPropertyChanged();
            }
        }

        IEventAggregator eventAggregator;

        public IssueListViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            ReportCreatedEvent reportCreatedEvent = eventAggregator.GetEvent<ReportCreatedEvent>();
            reportCreatedEvent.Subscribe(OnReportCreated);
        }

        private void OnReportCreated(Report report)
        {
            this.ProjectIssues = new ObservableCollection<ProjectIssues>(report.ProjectIssues);
        }
    }
}
