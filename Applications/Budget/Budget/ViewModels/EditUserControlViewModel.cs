using Budget.Converter;
using Budget.Helpers;
using Budget.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive;
using System.Windows;

namespace Budget.ViewModels
{
    public class EditUserControlViewModel : ReactiveObject
    {
        private static EditUserControlViewModel _instance;
        private ReactiveList<Transaction> _transactions;
        private DateTime _date;
        private List<Budget_GetAllCategories_Result> _categories;
        private Transaction _selectedTransaction;
        private int _selectedCategoryIndex;
        private bool _changesMade;
        public ReactiveList<Transaction> Transactions
        {
            get { return _transactions; }
            set { this.RaiseAndSetIfChanged(ref _transactions, value); }
        }
        public DateTime Date
        {
            get { return _date; }
            set { this.RaiseAndSetIfChanged(ref _date, value); }
        }
        public List<Budget_GetAllCategories_Result> Categories
        {
            get { return _categories; }
            set { this.RaiseAndSetIfChanged(ref _categories, value); }
        }
        public Transaction SelectedTransaction
        {
            get { return _selectedTransaction; }
            set { this.RaiseAndSetIfChanged(ref _selectedTransaction, value); }
        }
        public int SelectedCategoryIndex
        {
            get { return _selectedCategoryIndex; }
            set { this.RaiseAndSetIfChanged(ref _selectedCategoryIndex, value); }
        }
        public bool ChangesMade
        {
            get { return _changesMade; }
            set { this.RaiseAndSetIfChanged(ref _changesMade, value); }
        }
        public ReactiveCommand DeleteTransactionCommand { get; private set; }
        public ReactiveCommand SaveChangesCommand { get; private set; }
        public IObserver<Nullable<DateTime>> DateTimeObserver;
        public static EditUserControlViewModel GetInstance()
        {
            if (_instance == null)
                _instance = new EditUserControlViewModel();
            return _instance;
        }
        private EditUserControlViewModel()
        {
            GetAllCategories();
            DateTimeObserver = Observer.Create<Nullable<DateTime>>(
                date => UpdateDate(date),
                ex => HandleException(ex),
                () => Debug.WriteLine("EditUserControlViewModel dateTimeObserver OnCompleted.")
            );
            var canDeleteTransaction = this.WhenAny(x => x.SelectedTransaction,
                (selectedTrans) => selectedTrans != null);
            DeleteTransactionCommand = ReactiveCommand.Create(() => DeleteTransaction(), canDeleteTransaction);
            var canSaveChangesCommand = this.WhenAnyValue(x => x.ChangesMade,
                (changes) => changes == true);
            SaveChangesCommand = ReactiveCommand.Create(() => SaveChanges(), canSaveChangesCommand);
            Transactions = new ReactiveList<Transaction>();
        }
        internal void TransactionPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Transaction updatedTransaction = sender as Transaction;
            updatedTransaction.PropertyChanged -= EditUserControlViewModel.GetInstance().TransactionPropertyChanged;
            updatedTransaction.HasUpdated = true;
            updatedTransaction.ModifiedDate = DateTimeHelper.PstNow();
            updatedTransaction.PropertyChanged += EditUserControlViewModel.GetInstance().TransactionPropertyChanged;
            ChangesMade = true;
        }
        private void SaveChanges()
        {
            DateTime date = DateTimeHelper.PstNow();
            int changeSuccessCount = 0;
            foreach(Transaction transaction in Transactions)
            {
                if (transaction.HasUpdated)
                {
                    CarloniusRepository.UpdateTransaction(TransactionConverter.ConvertToBudget_Transaction(transaction));
                    CarloniusRepository.AddDateTimeLookup(transaction.DateTime);
                    transaction.HasUpdated = false;
                    ChangesMade = false;
                    date = transaction.DateTime;
                    changeSuccessCount++;
                }
            }
            MessageBox.Show(string.Format("{0} Change(s) Saved Successfully!", changeSuccessCount), "Transaction Update", MessageBoxButton.OK, MessageBoxImage.None);
            if (DateTimeHelper.AreEqual(date, Date))
            {
                UpdateTransactions(Date);
            }
        }
        private void DeleteTransaction()
        {
            CarloniusRepository.DeleteTransaction(TransactionConverter.ConvertToBudget_Transaction(SelectedTransaction));
            Transactions.Remove(SelectedTransaction);
        }
        private void HandleException(Exception ex)
        {
            Debug.WriteLine("Exception occurred {0}", ex.Message);
            throw ex;
        }
        public void UpdateTransactions(DateTime date)
        {
            Transactions = TransactionConverter.ConvertToTransactions(CarloniusRepository.GetTransactionsByDateTime(date));
        }
        private void UpdateDate(DateTime? date)
        {
            if (date.HasValue)
            {
                Date = date.Value;
                UpdateTransactions(Date);
            }
        }
        public void GetAllCategories()
        {
            Categories = CarloniusRepository.GetAllCategories();
        }
    }
}
