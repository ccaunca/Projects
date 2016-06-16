using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class DelayedGameObjectManager
    {
        private static DelayedGameObjectManager pInstance = null;
        private CollisionObserver collisionObservers;
        private DelayedGameObjectManager()
        {
            this.collisionObservers = null;
        }
        private static DelayedGameObjectManager GetInstance()
        {
            if (pInstance == null)
            {
                pInstance = new DelayedGameObjectManager();
            }
            return pInstance;
        }
        public static void Process()
        {
            DelayedGameObjectManager delayedGOMan = DelayedGameObjectManager.GetInstance();
            CollisionObserver observer = delayedGOMan.collisionObservers;
            while (observer != null)
            {
                observer.Execute();
                observer = (CollisionObserver)observer.pONext;
            }
            observer = delayedGOMan.collisionObservers;
            CollisionObserver obs = null;
            while (observer != null)
            {
                obs = observer;
                observer = (CollisionObserver)observer.pONext;
                delayedGOMan.Detach(obs, ref delayedGOMan.collisionObservers);
            }
        }
        public static void Attach(CollisionObserver observer)
        {
            Debug.Assert(observer != null);
            DelayedGameObjectManager delayedGOMan = DelayedGameObjectManager.GetInstance();
            if (delayedGOMan.collisionObservers == null)
            {
                delayedGOMan.collisionObservers = observer;
                observer.pONext = null;
                observer.pOPrev = null;
            }
            else
            {
                observer.pONext = delayedGOMan.collisionObservers;
                observer.pOPrev = null;
                delayedGOMan.collisionObservers.pOPrev = observer;
                delayedGOMan.collisionObservers = observer;
            }
        }
        private void Detach(CollisionObserver observer, ref CollisionObserver coNode)
        {
            Debug.Assert(observer != null);

            if (observer.pOPrev != null)
            {
                observer.pOPrev.pONext = observer.pONext;
            }
            else
            {
                coNode = (CollisionObserver)observer.pONext;
            }

            if (observer.pONext != null)
            {
                observer.pONext.pOPrev = observer.pOPrev;
            }
        }

    }
}
