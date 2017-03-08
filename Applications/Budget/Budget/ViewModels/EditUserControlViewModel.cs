using Budget.Helpers;
using Budget.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Budget.ViewModels
{
    public class EditUserControlViewModel : ReactiveObject
    {
        private ObservableCollection<Budget_Transactions> _transactions;
        public ObservableCollection<Budget_Transactions> Transactions
        {
            get { return _transactions; }
            set { this.RaiseAndSetIfChanged(ref _transactions, value); }
        }
        private static EditUserControlViewModel _instance;
        public static EditUserControlViewModel GetInstance()
        {
            if (_instance == null)
                _instance = new EditUserControlViewModel();
            return _instance;
        }
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { this.RaiseAndSetIfChanged(ref _date, value); }
        }
        private List<Budget_GetAllCategories_Result> _categories;
        public List<Budget_GetAllCategories_Result> Categories
        {
            get { return _categories; }
            set { this.RaiseAndSetIfChanged(ref _categories, value); }
        }
        private Budget_Transactions _selectedTransaction;
        public Budget_Transactions SelectedTransaction
        {
            get { return _selectedTransaction; }
            set { this.RaiseAndSetIfChanged(ref _selectedTransaction, value); }
        }
        public ReactiveCommand DeleteTransactionCommand { get; private set; }
        public IObserver<Nullable<DateTime>> DateTimeObserver;
        private EditUserControlViewModel()
        {
            Categories = CarloniusRepository.GetAllCategories();
            DateTimeObserver = Observer.Create<Nullable<DateTime>>(
                date => UpdateDate(date),
                ex => HandleException(ex),
                () => Debug.WriteLine("EditUserControlViewModel dateTimeObserver OnCompleted.")
            );
            var canDeleteTransaction = this.WhenAny(x => x.SelectedTransaction,
                (selectedTrans) => selectedTrans != null);
            DeleteTransactionCommand = ReactiveCommand.Create(() => DeleteTransaction(), canDeleteTransaction);
        }

        private void DeleteTransaction()
        {
            CarloniusRepository.DeleteTransaction(SelectedTransaction);
            Transactions.Remove(SelectedTransaction);
        }

        private void HandleException(Exception ex)
        {
            Debug.WriteLine("Exception occurred {0}", ex.Message);
            throw ex;
        }
        private void UpdateTransactions(DateTime date)
        {
            Transactions = CarloniusRepository.GetTransactionsByDateTime(date);
        }
        private void UpdateDate(DateTime? date)
        {
            if (date.HasValue)
            {
                Date = date.Value;
                UpdateTransactions(Date);
            }
        }
    }
}
