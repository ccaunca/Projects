﻿using Budget.Converter;
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
        private static ViewUserControlViewModel _instance;
        private IEnumerable<DateTime> _dates;
        private Transaction _selectedTransaction;
        public ReactiveList<Transaction> Transactions
        {
            get { return _transactions; }
            set { this.RaiseAndSetIfChanged(ref _transactions, value); }
        }
        public IEnumerable<DateTime> Dates
        {
            get { return _dates; }
            set { this.RaiseAndSetIfChanged(ref _dates, value); }
        }
        public Transaction SelectedTransaction
        {
            get { return _selectedTransaction; }
            set { this.RaiseAndSetIfChanged(ref _selectedTransaction, value); }
        }
        public IObserver<IEnumerable<DateTime>> DateTimeObserver;
        public static ViewUserControlViewModel GetInstance()
        {
            if (_instance == null)
                _instance = new ViewUserControlViewModel();
            return _instance;
        }
        private ViewUserControlViewModel()
        {
            //DateTimeObserver = Observer.Create<IEnumerable<DateTime>>(
            //    dates => UpdateDates(dates),
            //    ex => HandleException(ex),
            //    () => Debug.WriteLine("ViewUserControlViewModel dateTimeObserver OnCompleted.")
            //);
            Transactions = new ReactiveList<Transaction>();
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
        private void HandleException(Exception ex)
        {
            Debug.WriteLine("Exception occurred {0}", ex.Message);
            throw ex;
        }
    }
}
