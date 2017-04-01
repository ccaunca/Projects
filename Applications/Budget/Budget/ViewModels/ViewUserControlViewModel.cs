using Budget.Converter;
using Budget.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;

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
        private IEnumerable<DateTime> _dates;
        public IEnumerable<DateTime> Dates
        {
            get { return _dates; }
            set { this.RaiseAndSetIfChanged(ref _dates, value); }
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
        public IObserver<IEnumerable<DateTime>> DateTimeObserver;
        private ViewUserControlViewModel()
        {
            DateTimeObserver = Observer.Create<IEnumerable<DateTime>>(
                dates => UpdateDates(dates),
                ex => HandleException(ex),
                () => Debug.WriteLine("ViewUserControlViewModel dateTimeObserver OnCompleted.")
            );
            Transactions = new ReactiveList<Transaction>();
        }
        private void HandleException(Exception ex)
        {
            Debug.WriteLine("Exception occurred {0}", ex.Message);
            throw ex;
        }
        public void UpdateTransactions(IEnumerable<DateTime> dates)
        {
            Transactions = TransactionConverter.ConvertToTransactions(CarloniusRepository.GetTransactionsByDateTimes(dates));
        }
        private void UpdateDates(IEnumerable<DateTime> dates)
        {
            if (dates != null)
            {
                Dates = dates;
                UpdateTransactions(Dates);
            }
        }
    }
}
