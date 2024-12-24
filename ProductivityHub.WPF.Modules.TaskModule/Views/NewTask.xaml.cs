using ProductivityHub.WPF.Core.Interfaces;
using System.Windows.Controls;

namespace ProductivityHub.WPF.Modules.TaskModule.Views
{
    /// <summary>
    /// Interaction logic for NewTask
    /// </summary>
    public partial class NewTask : UserControl, IDocumentLayout
    {
        public NewTask()
        {
            InitializeComponent();
            DocumentName = "New Task";
        }

        public string DocumentName { get ; set; }
    }
}
