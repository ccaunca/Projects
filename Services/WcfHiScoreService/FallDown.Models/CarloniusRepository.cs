using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;

namespace FallDown.Models
{
    public class CarloniusRepository
    {
        public static void InsertHiScore(FallDown_HiScore hiScore)
        {
            try
            {
                using (var context = new CarloniusEntities())
                {
                    context.FallDown_HiScore.Add(hiScore);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "InsertHiScore");
            }
        }

        public static IEnumerable<FallDown_GetNHiScores_Result> GetScores(int n)
        {
            List<FallDown_GetNHiScores_Result> hiScores = new List<FallDown_GetNHiScores_Result>();
            try
            {
                using (var context = new CarloniusEntities())
                {
                    ObjectResult<FallDown_GetNHiScores_Result> resultSet = context.FallDown_GetNHiScores(n);
                    List<FallDown_GetNHiScores_Result> resultList = (from scores in resultSet select scores).ToList();
                    foreach(FallDown_GetNHiScores_Result result in resultList)
                    {
                        hiScores.Add(result);
                    }
                }
                
            }
            catch(Exception ex)
            {
                HandleException(ex, "GetScores");
            }
            return hiScores;
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
    }
}
