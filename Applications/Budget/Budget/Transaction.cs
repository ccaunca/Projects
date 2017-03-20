using System;
using System.ComponentModel;
using Budget.Enum;
using ReactiveUI;
using Budget.Models;

namespace Budget
{
    public class Transaction : ReactiveObject
    {
        private string _category;
        public string Category
        {
            get { return _category; }
            set { this.RaiseAndSetIfChanged(ref _category, value); }
        }
        private int _transactionId;
        public int TransactionID
        {
            get { return _transactionId; }
            set { this.RaiseAndSetIfChanged(ref _transactionId, value); }
        }
        private System.DateTime _dateTime;
        public System.DateTime DateTime
        {
            get { return _dateTime; }
            set { this.RaiseAndSetIfChanged(ref _dateTime, value); }
        }

        internal static string GetCategory(int categoryID)
        {
            return CarloniusRepository.GetCategoryByID(categoryID);
        }

        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set { this.RaiseAndSetIfChanged(ref _amount, value); }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set { this.RaiseAndSetIfChanged(ref _description, value); }
        }
        private int _categoryID;
        public int CategoryID
        {
            get { return _categoryID; }
            set { this.RaiseAndSetIfChanged(ref _categoryID, value); }
        }
        private System.DateTime _createdDate;
        public System.DateTime CreatedDate
        {
            get { return _createdDate; }
            set { this.RaiseAndSetIfChanged(ref _createdDate, value); }
        }
        private System.DateTime? _modifiedDate;
        public System.DateTime? ModifiedDate
        {
            get { return _modifiedDate; }
            set { this.RaiseAndSetIfChanged(ref _modifiedDate, value); }
        }
        private bool _hasUpdated;
        public bool HasUpdated
        {
            get { return _hasUpdated; }
            set { this.RaiseAndSetIfChanged(ref _hasUpdated, value); }
        }
        public Transaction()
        {

        }
    }
}
