using System;

namespace SpaceInvaders
{
    class GameInstructionsEvent : Command
    {
        public override void execute(float currentTime)
        {
            Game pGame = GameManager.GetGame();
            pGame.SetState(GameStateEnum.Instructions);
        }
    }
}
