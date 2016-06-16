using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveBrickObserver : CollisionObserver
    {
        public GameObject pBrick;
        public RemoveBrickObserver()
        {
            this.pBrick = null;
        }
        public RemoveBrickObserver(RemoveBrickObserver observer)
        {
            this.pBrick = observer.pBrick;
        }
        public override void Update()
        {
            //Debug.WriteLine("RemoveBrickObserver: {0} {1}", this.pSubject.goA, this.pSubject.goB);

            this.pBrick = (ShieldBrick)this.pSubject.goB;
            //Debug.WriteLine("BrickRemoveObserver: --> delete missile {0}", pBrick);
            if (pBrick.markedForDeath == false)
            {
                pBrick.markedForDeath = true;
                RemoveBrickObserver pObserver = new RemoveBrickObserver(this);
                DelayedGameObjectManager.Attach(pObserver);
            }
        }
        public override void Execute()
        {
             GameObject goBrick = (GameObject)this.pBrick;
            GameObject goParent = (GameObject)goBrick.pParent;
            goBrick.Remove();
            if (goParent.pChild == null)
             {
                GameObject goTmp = (GameObject)goParent.pParent;
                goParent.Remove();
                if (goTmp.pChild == null)
                {
                    goTmp.Remove();
                }
            }
        }
    }
}
