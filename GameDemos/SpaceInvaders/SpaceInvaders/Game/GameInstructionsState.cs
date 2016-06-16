using System;

namespace SpaceInvaders
{
    public class GameInstructionsState : GameState
    {
        public override void Handle(Game pGame)
        {
            throw new NotImplementedException();
        }

        public override void Draw(Game pGame)
        {
            FontManager.DrawString("PLAY", 400.0f, 800.0f);
            FontManager.DrawString("SPACE INVADERS", 320.0f, 700.0f);
            FontManager.DrawString("*SCORE ADVANCE TABLE*", 250.0F, 500.0f);
            FontManager.DrawString("= ??? MYSTERY", 380.0f, 440.0f);
            FontManager.DrawString("= 30 POINTS", 380.0f, 365.0f);
            FontManager.DrawString("= 20 POINTS", 380.0f, 290.0f);
            FontManager.DrawString("= 10 POINTS", 380.0f, 215.0f);
            Alien pUFO = new UFO(GameObjectName.UFO, SpriteBaseName.UFO, null, 325.0f, 440.0f);
            pUFO.pProxySprite.x = 325.0f;
            pUFO.pProxySprite.y = 440.0f;
            pUFO.pProxySprite.Render();
            pUFO.pProxySprite.Update();
            Alien pSquid = new Squid(GameObjectName.Squid, SpriteBaseName.Squid, 325.0f, 375.0f, 0);
            pSquid.pProxySprite.x = 325.0f;
            pSquid.pProxySprite.y = 375.0f;
            pSquid.pProxySprite.Render();
            pSquid.pProxySprite.Update();
            Alien pCrab = new Crab(GameObjectName.Crab, SpriteBaseName.Crab, 325.0f, 300.0f, 0);
            pCrab.pProxySprite.x = 325.0f;
            pCrab.pProxySprite.y = 300.0f;
            pCrab.pProxySprite.Render();
            pCrab.pProxySprite.Update();
            Alien pOctopus = new Octopus(GameObjectName.Octopus, SpriteBaseName.Octopus, 325.0f, 225.0f, 0);
            pOctopus.pProxySprite.x = 325.0f;
            pOctopus.pProxySprite.y = 225.0f;
            pOctopus.pProxySprite.Render();
            pOctopus.pProxySprite.Update();
        }

        public override void Start(Game pGame)
        {

        }

        public override void Die(Game pGame)
        {
            throw new NotImplementedException();
        }
    }
}
