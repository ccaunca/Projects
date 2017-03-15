using Budget.Enum;
using Budget.Models;
using Budget.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    Category = (CategoryEnum)transaction.CategoryID,
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
        private static CategoryEnum GetCategoryEnum(int categoryId)
        {
            CategoryEnum cat = CategoryEnum.Adventure;
            switch(categoryId)
            {
                case 2:
                    cat = CategoryEnum.DiningOut;
                    break;
                case 3:
                    cat = CategoryEnum.Mortgage;
                    break;
                case 4:
                    cat = CategoryEnum.Adventure;
                    break;
                case 5:
                    cat = CategoryEnum.Gas;
                    break;
                case 6:
                    cat = CategoryEnum.Gifts;
                    break;
                case 7:
                    cat = CategoryEnum.Household;
                    break;
                case 8:
                    cat = CategoryEnum.Shopping;
                    break;
                case 9:
                    cat = CategoryEnum.Entertainment;
                    break;
                case 1:
                default:
                    cat = CategoryEnum.Groceries;
                    break;
            }
            return cat;
        }

        public static Budget_Transactions ConvertToBudget_Transaction(Transaction selectedTransaction)
        {
            return new Budget_Transactions
            {
                Amount = selectedTransaction.Amount,
                CategoryID = (int)selectedTransaction.Category,
                CreatedDate = selectedTransaction.CreatedDate,
                DateTime = selectedTransaction.DateTime,
                Description = selectedTransaction.Description,
                ModifiedDate = selectedTransaction.ModifiedDate,
                TransactionID = selectedTransaction.TransactionID
            };
        }
    }
}
