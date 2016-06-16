using System;

namespace SpaceInvaders
{
    public class OnePlayerObserver : InputObserver
    {
        public override void Update()
        {
            Game pGame = GameManager.GetGame();
            if (pGame.GetState() is GameSelectState)
            {
                GameManager.ActivateGame(pGame.roundNum == 1);
                TimerManager.InitializeTimerManager();
            }
        }
    }
}
