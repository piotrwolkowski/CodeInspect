using CodeInspectEntities;
using CodeInspectEntities.CompositeEvents;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace IssueSelectionUI.ViewModels
{
    public class IssueSelectionViewModel : NotifyPropertyChangedImplementation
    {
        private IssueWithDescription selectedIssue;
        public IssueWithDescription SelectedIssue
        {
            get
            {
                return this.selectedIssue;
            }
            set
            {
                this.selectedIssue = value;
                this.OnPropertyChanged();
            }
        }

        public IssueSelectionViewModel(IEventAggregator eventAggregator)
        {
            SelectedIssueChangedEvent selectedIssueChangedEvent = eventAggregator.GetEvent<SelectedIssueChangedEvent>();
            selectedIssueChangedEvent.Subscribe(OnSelectedIssueChanged);
        }

        private void OnSelectedIssueChanged(IssueWithDescription selectedIssue)
        {
            this.SelectedIssue = selectedIssue;
        }
    }
}
