using CodeInspectEntities.CompositeEvents;
using CodeInspectInterfaces;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CommandUI.ViewModels
{
    public class CommandViewModel : NotifyPropertyChangedImplementation
    {
        IEventAggregator eventAggregator;

        private string progressMessage;
        public string ProgressMessage
        {
            get
            {
                return this.progressMessage;
            }

            set
            {
                this.progressMessage = value;
                this.OnPropertyChanged();
            }
        }

        public CommandViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            ProgressChangedEvent progressChangedEvent = eventAggregator.GetEvent<ProgressChangedEvent>();
            progressChangedEvent.Subscribe(OnProgressChanged);
        }

        private void OnProgressChanged(string progressMessage)
        {
            this.ProgressMessage = progressMessage;
        }
    }
}
