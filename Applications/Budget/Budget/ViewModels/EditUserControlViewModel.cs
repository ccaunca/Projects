using Budget.Converter;
using Budget.Helpers;
using Budget.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive;
using System.Windows;

namespace Budget.ViewModels
{
    public class EditUserControlViewModel : ReactiveObject
    {
        private ReactiveList<Transaction> _transactions;
        public ReactiveList<Transaction> Transactions
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
        private Transaction _selectedTransaction;
        public Transaction SelectedTransaction
        {
            get { return _selectedTransaction; }
            set { this.RaiseAndSetIfChanged(ref _selectedTransaction, value); }
        }
        private int _selectedCategoryIndex;
        public int SelectedCategoryIndex
        {
            get { return _selectedCategoryIndex; }
            set { this.RaiseAndSetIfChanged(ref _selectedCategoryIndex, value); }
        }
        private bool _changesMade;
        public bool ChangesMade
        {
            get { return _changesMade; }
            set { this.RaiseAndSetIfChanged(ref _changesMade, value); }
        }
        public ReactiveCommand DeleteTransactionCommand { get; private set; }
        public ReactiveCommand SaveChangesCommand { get; private set; }
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
            var canSaveChangesCommand = this.WhenAnyValue(x => x.ChangesMade,
                (changes) => changes == true);
            SaveChangesCommand = ReactiveCommand.Create(() => SaveChanges(), canSaveChangesCommand);
            Transactions = new ReactiveList<Transaction>();
            Transactions.CollectionChanged += this.Transactions_CollectionChanged;
        }
        private void Transactions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach(var item in e.NewItems)
                {

                }
            }
            if (e.OldItems != null)
            {
                foreach(var item in e.OldItems)
                {

                }
            }
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
            foreach(Transaction transaction in Transactions)
            {
                if (transaction.HasUpdated)
                {
                    CarloniusRepository.UpdateTransaction(TransactionConverter.ConvertToBudget_Transaction(transaction));
                    transaction.HasUpdated = false;
                    ChangesMade = false;
                    MessageBox.Show("Changes Saved Successfully!", "Transaction Update", MessageBoxButton.OK, MessageBoxImage.None);
                }
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
        private void UpdateTransactions(DateTime date)
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
    }
}
