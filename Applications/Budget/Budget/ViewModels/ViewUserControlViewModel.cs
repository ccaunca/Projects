using Budget.Converter;
using Budget.Models;
using Budget.UserControls;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive;
using System.Windows;

namespace Budget.ViewModels
{
    public class ViewUserControlViewModel : ReactiveObject
    {
        private ReactiveList<Transaction> _transactions;
        public ReactiveList<Transaction> Transactions
        {
            get { return _transactions; }
            set { this.RaiseAndSetIfChanged(ref _transactions, value); }
        }
        private static ViewUserControlViewModel _instance;
        public static ViewUserControlViewModel GetInstance()
        {
            if (_instance == null)
                _instance = new ViewUserControlViewModel();
            return _instance;
        }
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { this.RaiseAndSetIfChanged(ref _date, value); }
        }
        private Transaction _selectedTransaction;
        public Transaction SelectedTransaction
        {
            get { return _selectedTransaction; }
            set { this.RaiseAndSetIfChanged(ref _selectedTransaction, value); }
        }
        //public static readonly DependencyProperty SelectedDatesProperty = DependencyProperty.RegisterAttached(
        //    "SelectedDates", typeof(ReactiveList<DateTime>), typeof(ViewUserControl), new PropertyMetadata(false));
        //public static void SetSelectedDates(DependencyObject target, ReactiveList<DateTime> value)
        //{
        //    target.SetValue(SelectedDatesProperty, value);
        //}
        //public static ReactiveList<DateTime> GetSelectedDates(DependencyObject target)
        //{
        //    return (ReactiveList<DateTime>)target.GetValue(SelectedDatesProperty);
        //}
        public IObserver<Nullable<DateTime>> DateTimeObserver;
        private ViewUserControlViewModel()
        {
            DateTimeObserver = Observer.Create<Nullable<DateTime>>(
                date => UpdateDate(date),
                ex => HandleException(ex),
                () => Debug.WriteLine("ViewUserControlViewModel dateTimeObserver OnCompleted.")
            );
            Transactions = new ReactiveList<Transaction>();
            Transactions.PropertyChanged += Transactions_PropertyChanged;
        }
        private void Transactions_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
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
    }
}
