using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallDown.Models
{
    public class CarloniusRepository
    {
        public static void InsertHiScore(int score, char[] name)
        {
            try
            {
                using (var entity = new CarloniusEntities())
                {
                    entity.FallDown_HiScore.Add(
                        new FallDown_HiScore
                        {
                            DateTime = DateTime.Now,
                            HiScoreValue = score,

                        });
                    entity.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static IEnumerable<FallDown_HiScore> GetScores(int n)
        {
            List<FallDown_HiScore> scores = new List<FallDown_HiScore>();
            try
            {
                using (var entity = new CarloniusEntities())
                {
                    //entity.FallDown_HiScore
                }
                
            }
            catch(Exception ex)
            {

            }
            return scores;
        }
    }
}
