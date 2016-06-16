using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveShipObserver : CollisionObserver
    {
        public GameObject pShip;
        public RemoveShipObserver()
        {
            this.pShip = null;
        }
        public RemoveShipObserver(RemoveShipObserver observer)
        {
            this.pShip = observer.pShip;
        }
        public override void Update()
        {
            //Debug.WriteLine("RemoveShipObserver: {0} {1}", this.pSubject.goA, this.pSubject.goB);
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
            //pGame.Pause();
            int lives = ScoreManager.UpdateLives();
            if (lives == 0)
            {
                pGame.roundNum = 1;
                TimerManager.ClearTimerManager();
                pExplosion.Remove();
                pGame.Die();
                TimerManager.Add(TimerEventName.GameStart, TimerManager.GetCurrentTime() + 10.0f, 10.0f, new GameStartEvent(pGame));
            }
            else
            {
                TimerManager.Add(TimerEventName.RemoveGameObject, TimerManager.GetCurrentTime(), TimerManager.GetCurrentTime(), new RemoveGameObjectCommand(pExplosion));
                PCSTree pRootTree = GameObjectManager.GetRootTree();
                //ShipRoot pShipRoot = (ShipRoot)GameObjectManager.Find(GameObjectName.ShipRoot);
                //pShipRoot.ActivateCollisionSprite(sbBoxes);
                //pShipRoot.ActivateGameSprite(sbAliens);
                ShipManager.Create(GameManager.GetCollisionBoxes());
            }
        }
    }
}