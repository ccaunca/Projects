using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveExplosionObserver : CollisionObserver
    {
        private GameObject pGameObject;
        private GameObjectName goName;
        public RemoveExplosionObserver(GameObject go)
        {
            this.pGameObject = go;
            this.goName = go.gameObjectName;
        }
        public override void Update()
        {
            GameObject pExplosion = GameObjectManager.Find(this.goName);
            if (pExplosion.markedForDeath == false)
            {
                pExplosion.markedForDeath = true;
                RemoveExplosionObserver pObserver = new RemoveExplosionObserver(pExplosion);
                DelayedGameObjectManager.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            this.pGameObject.Remove();
        }
    }
}
