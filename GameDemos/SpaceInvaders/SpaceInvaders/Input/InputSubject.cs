using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class InputSubject
    {
        public InputObserver pObservers;
        public void RegisterObserver(InputObserver observer)
        {
            Debug.Assert(observer != null);
            observer.pSubject = this;

            if (pObservers == null)
            {
                pObservers = observer;
                observer.pONext = null;
                observer.pOPrev = null;
            }
            else
            {
                observer.pONext = pObservers;
                pObservers.pOPrev = observer;
                pObservers = observer;
            }
        }
        public void NotifyObservers()
        {
            InputObserver curr = this.pObservers;
            while (curr != null)
            {
                curr.Update();
                curr = (InputObserver)curr.pONext;
            }
        }
    }
}
