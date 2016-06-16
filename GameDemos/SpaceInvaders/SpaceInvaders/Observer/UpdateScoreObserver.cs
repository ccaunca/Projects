using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UpdateScoreObserver : CollisionObserver
    {
        public override void Update()
        {
            GameObject pAlien = this.pSubject.goB;
            ScoreManager.UpdateScore(pAlien);
        }
    }
}
