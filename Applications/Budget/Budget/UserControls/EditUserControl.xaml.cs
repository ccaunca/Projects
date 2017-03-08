using Budget.ViewModels;
using System.Windows.Controls;

namespace Budget.UserControls
{
    /// <summary>
    /// Interaction logic for EditUserControl.xaml
    /// </summary>
    public partial class EditUserControl : UserControl
    {
        public EditUserControl()
        {
            InitializeComponent();
            DataContext = EditUserControlViewModel.GetInstance();
        }
    }
}
