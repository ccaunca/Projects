using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;

namespace Budget.Models
{
    public class CarloniusRepository
    {
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
            return categories;
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
        public static ObservableCollection<Budget_Transactions> GetTransactionsByDateTime(DateTime datetime)
        {
            ObservableCollection<Budget_Transactions> transactions = new ObservableCollection<Budget_Transactions>();
            try
            {
                using (var context = new CarloniusEntities())
                {
                    ObjectResult<GetTransactionsByDateTime_Result> resultSet = context.GetTransactionsByDateTime(GetDateTime(datetime));
                    List<GetTransactionsByDateTime_Result> resultList = (from trans in resultSet select trans).ToList();
                    foreach(GetTransactionsByDateTime_Result result in resultList)
                    {
                        transactions.Add(new Budget_Transactions
                        {
                            TransactionID = result.TransactionID,
                            Amount = result.Amount,
                            CreatedDate = result.CreatedDate,
                            CategoryID = result.CategoryID,
                            DateTime = result.DateTime,
                            Description = result.Description
                        });
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
