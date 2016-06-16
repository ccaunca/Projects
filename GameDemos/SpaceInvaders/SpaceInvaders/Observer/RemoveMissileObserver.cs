using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveMissileObserver : CollisionObserver
    {
        public GameObject pMissile;
        public RemoveMissileObserver()
        {
            this.pMissile = null;
        }
        public RemoveMissileObserver(RemoveMissileObserver observer)
        {
            this.pMissile = observer.pMissile;
        }
        public override void Update()
        {
            //Debug.WriteLine("RemoveMissileObserver: {0} {1}", this.pSubject.goA, this.pSubject.goB);

            this.pMissile = Missile.GetMissile(this.pSubject.goA, this.pSubject.goB);
            //Debug.WriteLine("MissileRemoveObserver: --> delete missile {0}", pMissile);
            if (pMissile.markedForDeath == false)
            {
                pMissile.markedForDeath = true;
                RemoveMissileObserver pObserver = new RemoveMissileObserver(this);
                DelayedGameObjectManager.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            this.pMissile.Remove();
        }
    }
}
