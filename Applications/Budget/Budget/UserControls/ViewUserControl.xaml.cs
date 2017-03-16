using Budget.ViewModels;
using System.Windows.Controls;

namespace Budget.UserControls
{
    /// <summary>
    /// Interaction logic for ViewUserControl.xaml
    /// </summary>
    public partial class ViewUserControl : UserControl
    {
        public ViewUserControl()
        {
            InitializeComponent();
            DataContext = ViewUserControlViewModel.GetInstance();
        }
    }
}
