using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class CollisionSubject
    {
        public CollisionObserver pObservers;
        public GameObject goA;
        public GameObject goB;
        public CollisionSubject()
        {
            this.pObservers = null;
            this.goA = null;
            this.goB = null;
        }
        ~CollisionSubject()
        {
            this.pObservers = null;
            this.goA = null;
            this.goB = null;
        }
        public void RegisterObserver(CollisionObserver observer)
        {
            Debug.Assert(observer != null);
            observer.pSubject = this;

            if (this.pObservers == null)
            {
                this.pObservers = observer;
                observer.pONext = null;
                observer.pOPrev = null;
            }
            else
            {
                observer.pONext = pObservers;
                this.pObservers.pOPrev = observer;
                this.pObservers = observer;
            }

        }
        public void RemoveObserver(CollisionObserver observer)
        {
            throw new NotImplementedException();
        }

        public void NotifyObservers()
        {
            CollisionObserver curr = this.pObservers;
            while (curr != null)
            {
                curr.Update();
                curr = (CollisionObserver)curr.pONext; 
            }
        }
    }
}
