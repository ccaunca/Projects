using FallDown.Models;
using System;
using System.Collections.Generic;

namespace HiScoreWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class HiScoreService : IHiScoreService
    {
        public IEnumerable<FallDown_GetNHiScores_Result> GetNScores(string n)
        {
            return CarloniusRepository.GetScores(Convert.ToInt32(n));
        }

        public void InsertHiScore(string hiScore)
        {
            //CarloniusRepository.InsertHiScore(hiScore);
        }
    }
}
