using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GamePauseState : GameState
    {
        public override void Handle(Game pGame, GameStateEnum pState)
        {
            pGame.SetState(GameStateEnum.Active);
        }

        public override void Start(Game pGame)
        {
            throw new NotImplementedException();
        }

        public override void Pause(Game pGame)
        {
            throw new NotImplementedException();
        }

        public override void Restart(Game pGame)
        {
            throw new NotImplementedException();
        }

        public override void Resume(Game pGame)
        {
            Ship pShip = ShipManager.GetShip();
            pShip.SetState(ShipStateEnum.Ready);
            this.Handle(pGame, GameStateEnum.Active);
        }

        public override void Die(Game pGame)
        {
            throw new NotImplementedException();
        }
    }
}
