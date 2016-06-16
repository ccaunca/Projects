using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UpdateUFOScoreObserver : CollisionObserver
    {
        public override void Update()
        {
            UFO pUFO = (UFO)this.pSubject.goB;
            if (pUFO != null)
            {
                ScoreManager.UpdateScore(pUFO);
            }
        }
    }
}
