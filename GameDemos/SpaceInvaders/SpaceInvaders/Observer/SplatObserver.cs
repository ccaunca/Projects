using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SplatObserver : CollisionObserver
    {
        public override void Update()
        {
            GameObject gameObjectA = this.pSubject.goA;
            GameObject gameObjectB = this.pSubject.goB;
            GameObject go = Missile.GetNonMissile(gameObjectA, gameObjectB);
            ColorName pColorName = ColorName.White;
            if(go.gameObjectName == GameObjectName.Bomb)
            {
                pColorName = ColorName.Orange;
            }
            else if(go.gameObjectName == GameObjectName.UFO)
            {
                pColorName = ColorName.Red;
            }
            else if(go.gameObjectName == GameObjectName.UFOBomb)
            {
                pColorName = ColorName.Yellow;
            }
            Alien pExplosion = new Explosion(GameObjectName.Splat, SpriteBaseName.Splat, AlienType.Splat, gameObjectB, pColorName, 0);
            SpriteBatch sbAliens = SpriteBatchManager.Find(SpriteBatchName.Aliens);
            SpriteBatch sbBoxes = SpriteBatchManager.Find(SpriteBatchName.Boxes);
            pExplosion.ActivateGameSprite(sbAliens);
            pExplosion.ActivateCollisionSprite(sbBoxes);
            GameObjectManager.AttachTree(pExplosion);
            TimerManager.Add(TimerEventName.RemoveGameObject, TimerManager.GetCurrentTime(), TimerManager.GetCurrentTime(), new RemoveGameObjectCommand(pExplosion));
        }
    }
}
