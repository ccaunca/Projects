using System;

namespace SpaceInvaders
{
    class GameSelectEvent : Command
    {
        public override void execute(float currentTime)
        {
            Game pGame = GameManager.GetGame();
            pGame.SetState(GameStateEnum.Select);
        }
    }
}
