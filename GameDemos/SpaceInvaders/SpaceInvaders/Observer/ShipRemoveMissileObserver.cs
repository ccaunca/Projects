using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipRemoveMissileObserver : CollisionObserver
    {
        public GameObject pMissile;
        public ShipRemoveMissileObserver()
        {
            this.pMissile = null;
        }
        public ShipRemoveMissileObserver(ShipRemoveMissileObserver observer)
        {
            this.pMissile = observer.pMissile;
        }
        public override void Update()
        {
            Debug.WriteLine("ShipRemoveMissileObserver: {0} {1}", this.pSubject.goA, this.pSubject.goB);

            this.pMissile = Missile.GetMissile(this.pSubject.goA, this.pSubject.goB);
            Debug.WriteLine("MissileRemoveObserver: --> delete missile {0}", pMissile);
            if (pMissile.markedForDeath == false)
            {
                pMissile.markedForDeath = true;
                ShipRemoveMissileObserver pObserver = new ShipRemoveMissileObserver(this);
                DelayedGameObjectManager.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            this.pMissile.Remove();
        }
    }
}
