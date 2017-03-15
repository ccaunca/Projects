using Budget.ViewModels;
using System.Windows.Controls;

namespace Budget.UserControls
{
    /// <summary>
    /// Interaction logic for AddUserControl.xaml
    /// </summary>
    public partial class AddUserControl : UserControl
    {
        public AddUserControl()
        {
            InitializeComponent();
            DataContext = AddUserControlViewModel.GetInstance();
        }
    }
}
