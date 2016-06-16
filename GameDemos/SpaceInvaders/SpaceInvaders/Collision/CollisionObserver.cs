using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class CollisionObserver : OLink
    {
        public int index;
        public abstract void Update();
        public virtual void Execute()
        {

        }
        public CollisionSubject pSubject;
    }
}
