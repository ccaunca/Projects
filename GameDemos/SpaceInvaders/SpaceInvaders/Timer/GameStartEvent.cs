using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GameStartEvent : Command
    {
        private Game pGame;
        public GameStartEvent(Game pGame)
        {
            this.pGame = pGame;
        }
        public override void execute(float currentTime)
        {   // Called from RemoveShipObserver after CurrentPlayerLives == 0
            Debug.Assert(this.pGame != null);
            if (this.pGame.GetState() is GameOverState)
            {
                this.pGame.Start();
                ScoreManager.ResetLives();
            }
        }
    }
}
