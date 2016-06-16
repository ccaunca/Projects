using System;

namespace SpaceInvaders
{
    class GameOverEvent : Command
    {
        public override void execute(float currentTime)
        {
            Game pGame = GameManager.GetGame();
            pGame.SetState(GameStateEnum.Over);
        }
    }
}
