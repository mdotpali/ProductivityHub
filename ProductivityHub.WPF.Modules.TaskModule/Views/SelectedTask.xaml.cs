using Prism.Regions;
using ProductivityHub.WPF.Core.Interfaces;
using ProductivityHub.WPF.Modules.TaskModule.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace ProductivityHub.WPF.Modules.TaskModule.Views
{
    /// <summary>
    /// Interaction logic for SelectedTask
    /// </summary>
    public partial class SelectedTask : UserControl, IDocumentLayout
    {
        public SelectedTask()
        {
            InitializeComponent();

        }

        private string _documentName;
        public string DocumentName
        {
            get { return _documentName; }
            set
            {
                if (_documentName != value)
                {
                    _documentName = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
