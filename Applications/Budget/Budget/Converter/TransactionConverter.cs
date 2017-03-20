using Budget.Enum;
using Budget.Models;
using Budget.ViewModels;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace Budget.Converter
{
    public class TransactionConverter
    {
        public static ReactiveList<Transaction> ConvertToTransactions(ObservableCollection<Budget_Transactions> transactions)
        {
            ReactiveList<Transaction> trans = new ReactiveList<Transaction>();
            foreach (Budget_Transactions transaction in transactions)
            {
                Transaction newTransaction = new Transaction
                {
                    TransactionID = transaction.TransactionID,
                    Amount = transaction.Amount,
                    Category = Transaction.GetCategory(transaction.CategoryID),
                    CategoryID = transaction.CategoryID,
                    CreatedDate = transaction.CreatedDate,
                    DateTime = transaction.DateTime,
                    Description = transaction.Description,
                    ModifiedDate = transaction.ModifiedDate,
                    HasUpdated = false
                };
                EditUserControlViewModel vm = EditUserControlViewModel.GetInstance();
                newTransaction.PropertyChanged += vm.TransactionPropertyChanged;
                trans.Add(newTransaction);
            }
            return trans;
        }
        public static Budget_Transactions ConvertToBudget_Transaction(Transaction selectedTransaction)
        {
            return new Budget_Transactions
            {
                Amount = selectedTransaction.Amount,
                CategoryID = selectedTransaction.CategoryID,
                CreatedDate = selectedTransaction.CreatedDate,
                DateTime = selectedTransaction.DateTime,
                Description = selectedTransaction.Description,
                ModifiedDate = selectedTransaction.ModifiedDate,
                TransactionID = selectedTransaction.TransactionID
            };
        }
    }
}
