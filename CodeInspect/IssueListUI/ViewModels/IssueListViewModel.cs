using CodeInspectEntities;
using CodeInspectEntities.CompositeEvents;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Utilities;

namespace IssueListUI.ViewModels
{
    public class IssueListViewModel : NotifyPropertyChangedImplementation
    {
        IEventAggregator eventAggregator;

        ICommand selectionChangedCommand;
        public ICommand SelectionChangedCommand
        {
            get
            {
                if (selectionChangedCommand == null)
                {
                    selectionChangedCommand = new DelegateCommand<IssueWithDescription>(OnSelectedIssueChanged);
                }
                return selectionChangedCommand;
            }
        }

        private ObservableCollection<IssueWithDescription> projectIssues;
        public ObservableCollection<IssueWithDescription> ProjectIssues
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

        public IssueListViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            ReportCreatedEvent reportCreatedEvent = eventAggregator.GetEvent<ReportCreatedEvent>();
            reportCreatedEvent.Subscribe(OnReportCreated);
        }

        private void OnSelectedIssueChanged(IssueWithDescription selectedIssue)
        {
            SelectedIssueChangedEvent selectedIssueChangedEvent =
                eventAggregator.GetEvent<SelectedIssueChangedEvent>();

            selectedIssueChangedEvent.Publish(selectedIssue);
        }

        private void OnReportCreated(Report report)
        {
            this.ProjectIssues = new ObservableCollection<IssueWithDescription>(report.AllIssues);
        }
    }
}
