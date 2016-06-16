using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GameSelectState : GameState
    {
        public override void Handle(Game pGame)
        {
            pGame.SetState(GameStateEnum.Active);
        }

        public override void Draw(Game pGame)
        {
            FontManager.DrawString("PUSH", 400.0f, 650.0f);
            FontManager.DrawString("1 - for 1 Player", 310.0f, 590.0f);
            //FontManager.DrawString("2 - for 2 Player", 310.0f, 530.0f);
        }

        public override void Start(Game pGame)
        {
            this.Handle(pGame);
        }

        public override void Die(Game pGame)
        {   // Do nothing, can't die in SelectState!
        }
    }
}
