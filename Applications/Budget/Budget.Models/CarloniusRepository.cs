using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;

namespace Budget.Models
{
    public class CarloniusRepository
    {
        public static void AddDateTimeLookup(DateTime date)
        {
            try
            {
                using (var context = new CarloniusEntities())
                {
                    DateTime shortDate = GetDateTime(date);
                    Budget_DateTimeLookup lookup = context.Budget_DateTimeLookup.FirstOrDefault(l => l.DateTime.Equals(shortDate));
                    if (lookup == null)
                    {
                        context.Budget_DateTimeLookup.Add(new Budget_DateTimeLookup { DateTime = shortDate });
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "AddDateTimeLookup");
            }
        }
        public static List<Budget_GetAllCategories_Result> GetAllCategories()
        {
            List<Budget_GetAllCategories_Result> categories = new List<Budget_GetAllCategories_Result>();
            try
            {
                using (var context = new CarloniusEntities())
                {
                    ObjectResult<Budget_GetAllCategories_Result> resultSet = context.Budget_GetAllCategories();
                    List<Budget_GetAllCategories_Result> resultList = (from cat in resultSet select cat).ToList();
                    foreach (Budget_GetAllCategories_Result result in resultList)
                    {
                        categories.Add(result);
                    }
                }
            }
            catch(Exception ex)
            {
                HandleException(ex, "GetAllCategories");
            }
            return categories.OrderBy(c => c.Category).ToList();
        }
        public static string GetCategoryByID(int categoryID)
        {
            string category = string.Empty;
            try
            {
                using (var context = new CarloniusEntities())
                {
                    category = context.Budget_Categories.FirstOrDefault(c => c.CategoryID == categoryID).Category;
                }
            }
            catch(Exception ex)
            {
                HandleException(ex, "GetCategoryByID");
            }
            return category;
        }
        public static Budget_Categories GetCategory(string category)
        {
            Budget_Categories cat = null;
            try
            {
                using (var context = new CarloniusEntities())
                {
                    cat = context.Budget_Categories.FirstOrDefault(c => c.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
                }
            }
            catch(Exception ex)
            {
                HandleException(ex, "GetCategory");
            }
            return cat;
        }
        public static void InsertCategory(Budget_Categories category)
        {
            try
            {
                using (var context = new CarloniusEntities())
                {
                    context.Budget_Categories.Add(category);
                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                HandleException(ex, "InsertCategory");
            }
        }
        public static ObservableCollection<Budget_Transactions> GetTransactionsByDateTimes(IEnumerable<DateTime> dates)
        {
            ObservableCollection<Budget_Transactions> transactions = new ObservableCollection<Budget_Transactions>();
            try
            {
                using (var context = new CarloniusEntities())
                {
                    List<TransactionsDatesView> transactionsView = context.TransactionsDatesViews
                        .Where(trans => dates.Contains(trans.Date)).ToList();
                    List<Budget_Transactions> budgetTransactionList = transactionsView
                        .Select(trans => new Budget_Transactions()
                        {
                            Amount = trans.Amount,
                            CategoryID = trans.CategoryID,
                            CreatedDate = trans.CreatedDate,
                            DateTime = trans.DateTime,
                            Description = trans.Description,
                            ModifiedDate = trans.ModifiedDate,
                            TransactionID = trans.TransactionID
                        })
                        .ToList();
                    foreach(var trans in budgetTransactionList)
                    {
                        transactions.Add(trans);
                    }
                }
            }
            catch(Exception ex)
            {
                HandleException(ex, "GetTransactionsByDateTimes");
            }
            return transactions;
        }
        public static ObservableCollection<Budget_Transactions> GetTransactionsByDateTime(DateTime datetime)
        {
            ObservableCollection<Budget_Transactions> transactions = new ObservableCollection<Budget_Transactions>();
            try
            {
                using (var context = new CarloniusEntities())
                {
                    List<TransactionsDatesView> transactionsView = context.TransactionsDatesViews
                        .Where(trans => datetime == trans.Date).ToList();
                    List<Budget_Transactions> budgetTransactionList = transactionsView
                        .Select(trans => new Budget_Transactions()
                        {
                            Amount = trans.Amount,
                            CategoryID = trans.CategoryID,
                            CreatedDate = trans.CreatedDate,
                            DateTime = trans.DateTime,
                            Description = trans.Description,
                            ModifiedDate = trans.ModifiedDate,
                            TransactionID = trans.TransactionID
                        })
                        .ToList();
                    foreach (var trans in budgetTransactionList)
                    {
                        transactions.Add(trans);
                    }
                }
            }
            catch(Exception ex)
            {
                HandleException(ex, "GetTransactionByDateTime");
            }
            return transactions;
        }
        public static void AddTransaction(Budget_Transactions transaction)
        {
            try
            {
                using (var context = new CarloniusEntities())
                {
                    context.Budget_Transactions.Add(transaction);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "AddTransaction");
            }
        }
        public static void UpdateTransaction(Budget_Transactions transaction)
        {
            try
            {
                using (var context = new CarloniusEntities())
                {
                    context.Budget_Transactions.Attach(transaction);
                    context.Entry(transaction).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                HandleException(ex, "UpdateTransaction");
            }
        }
        public static void DeleteTransaction(Budget_Transactions transaction)
        {
            try
            {
                using (var context = new CarloniusEntities())
                {
                    context.Budget_Transactions.Attach(transaction);
                    context.Budget_Transactions.Remove(transaction);
                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                HandleException(ex, "DeleteTransaction");
            }
        }
        /// <summary>
        /// Handle exception and print debugMessage
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="debugMessage"></param>
        private static void HandleException(Exception ex, string debugMessage)
        {
            Debug.WriteLine("CarloniusRepository Exception occurred attempting to {0} : {1}", debugMessage, ex.Message);
            throw ex;
        }
        private static DateTime GetDateTime(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
        }
    }
}
