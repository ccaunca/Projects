using System;

namespace SpaceInvaders
{
    public class GameOverObserver : CollisionObserver
    {
        public override void Update()
        {
            if (GameManager.GetGame().GetState() is GameActiveState)
            {
                GameObject ship = ShipManager.GetShip();
                if (ship != null)
                {
                    ship.Remove();
                    GameObject pShipRoot = GameObjectManager.Find(GameObjectName.ShipRoot);
                    pShipRoot.Remove();
                }
                Alien pExplosion = new Explosion(GameObjectName.Explosion, SpriteBaseName.Explosion, AlienType.Explosion, ship, ColorName.Green, 0);
                SpriteBatch sbAliens = SpriteBatchManager.Find(SpriteBatchName.Aliens);
                SpriteBatch sbBoxes = SpriteBatchManager.Find(SpriteBatchName.Boxes);
                pExplosion.ActivateGameSprite(sbAliens);
                pExplosion.ActivateCollisionSprite(sbBoxes);
                GameObjectManager.AttachTree(pExplosion);
                Game pGame = GameManager.GetGame();
                pGame.roundNum = 1;
                ScoreManager.ClearLives();
                TimerManager.ClearTimerManager();
                pExplosion.Remove();
                pGame.Die();
                TimerManager.Add(TimerEventName.GameStart, TimerManager.GetCurrentTime() + 10.0f, 10.0f, new GameStartEvent(pGame));
            }
        }
    }
}
