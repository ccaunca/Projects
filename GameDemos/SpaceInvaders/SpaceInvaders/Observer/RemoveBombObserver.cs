using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveBombObserver : CollisionObserver
    {
        public GameObject pBomb;
        public RemoveBombObserver()
        {
            this.pBomb = null;
        }
        public RemoveBombObserver(RemoveBombObserver observer)
        {
            this.pBomb = observer.pBomb;
        }
        public override void Update()
        {
            //Debug.WriteLine("RemoveBombObserver: {0} {1}", this.pSubject.goA, this.pSubject.goB);

            //this.pBomb = (Bomb)this.pSubject.goA;
            this.pBomb = Bomb.GetBomb(this.pSubject.goA, this.pSubject.goB);
            Debug.Assert(this.pBomb != null);
            //Debug.WriteLine("BombRemoveObserver: --> delete bomb {0}", pBomb);
            if (pBomb.markedForDeath == false)
            {
                pBomb.markedForDeath = true;
                RemoveBombObserver pObserver = new RemoveBombObserver(this);
                DelayedGameObjectManager.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            //Debug.WriteLine("Removing {0}:{1}", this.pBomb.gameObjectName, this.pBomb.GetHashCode());
            this.pBomb.Remove();
        }
    }
}
