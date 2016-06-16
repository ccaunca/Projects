using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveSplatObserver : CollisionObserver
    {
        private GameObject pGameObject;
        public RemoveSplatObserver()
        {
            this.pGameObject = null;
        }
        public RemoveSplatObserver(GameObject go)
        {
            this.pGameObject = go;
        }
        public override void Update()
        {
            GameObject pSplat = GameObjectManager.Find(GameObjectName.Splat);
            if (pSplat.markedForDeath == false)
            {
                pSplat.markedForDeath = true;
                RemoveSplatObserver pObserver = new RemoveSplatObserver(pSplat);
                DelayedGameObjectManager.Attach(pObserver);
            }
        }
    }
}
