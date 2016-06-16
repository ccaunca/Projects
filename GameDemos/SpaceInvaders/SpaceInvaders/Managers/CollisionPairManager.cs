using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class CollisionPairManager : Manager
    {
        private static CollisionPairManager pInstance;
        private CollisionPair pActiveCollisionPair;
        private CollisionPairManager(int reserveSize, int reserveIncrement)
            : base(reserveSize, reserveIncrement)
        {
            this.pActiveCollisionPair = null;
        }
        private static CollisionPairManager GetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }
        public static CollisionPair Add(CollisionPairName name, GameObject gameObject1, GameObject gameObject2)
        {
            CollisionPairManager cpMan = CollisionPairManager.GetInstance();
            CollisionPair collisionPair = (CollisionPair)cpMan.BaseAdd();
            Debug.Assert(collisionPair != null);

            collisionPair.Set(name, gameObject1, gameObject2);
            return collisionPair;
        }
        public static void Create(int reserveSize, int reserveIncrement)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(reserveIncrement > 0);
            if (pInstance == null)
            {
                pInstance = new CollisionPairManager(reserveSize, reserveIncrement);
            }
        }
        public static CollisionPair Find(CollisionPairName name)
        {
            CollisionPairManager collisionPairMan = CollisionPairManager.GetInstance();
            return (CollisionPair)collisionPairMan.BaseFind((DLink)new CollisionPair {name = name} );
        }
        public static CollisionPair GetActiveCollisionPair()
        {
            CollisionPairManager cpMan = CollisionPairManager.GetInstance();

            return cpMan.pActiveCollisionPair;
        }
        public static void Process()
        {
            CollisionPairManager collisionPairMan = CollisionPairManager.GetInstance();
            CollisionPair collisionPair = (CollisionPair)collisionPairMan.pActive;
            while (collisionPair != null)
            {
                collisionPairMan.pActiveCollisionPair = collisionPair;
                collisionPair.Process();
                collisionPair = (CollisionPair)collisionPair.pDNext;
            }
        }
        protected override bool Compare(DLink dLink1, DLink dLink2)
        {
            bool result = false;
            CollisionPair cp1 = (CollisionPair)dLink1;
            CollisionPair cp2 = (CollisionPair)dLink2;
            if (cp1.name == cp2.name)
            {
                result = true;
            }
            return result;
        }

        protected override DLink CreateNode()
        {
            return (DLink)new CollisionPair();
        }

        protected override void DumpNode(DLink node)
        {
            throw new NotImplementedException();
        }
    }
}
