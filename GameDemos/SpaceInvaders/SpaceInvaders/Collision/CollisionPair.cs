using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum CollisionPairName
    {
        AliensvMissile, AliensvWalls, AliensvShields, AliensvShip,
        BombsvShields, BombsvShip, BombsvMissile, BombsvBottom,
        MissilevGrid, MissilevTop, MissilevUFO, MissilevShields,
        UFOvWalls, ShipvWalls,
        GridvWalls,
        ColumnvMissile,
        Uninitialized,
        GridvShip,
    }
    public class CollisionPair : DLink
    {
        public GameObject Tree1;
        public GameObject Tree2;
        public CollisionPairName name;
        public CollisionSubject subject;
        public CollisionPair()
            : base()
        {
            this.Tree1 = null;
            this.Tree2 = null;
            this.name = CollisionPairName.Uninitialized;
            this.subject = new CollisionSubject();
        }
        public void Set(CollisionPairName cpName, GameObject gameObject1, GameObject gameObject2)
        {
            Debug.Assert(gameObject1 != null);
            Debug.Assert(gameObject2 != null);

            this.Tree1 = gameObject1;
            this.Tree2 = gameObject2;
            this.name = cpName;
        }
        public void SetCollision(GameObject go1, GameObject go2)
        {
            Debug.Assert(go1 != null);
            Debug.Assert(go2 != null);
            this.subject.goA = go1;
            this.subject.goB = go2;
        }
        public void Process()
        {
            Collide(this.Tree1, this.Tree2);
        }
        public static void Collide(GameObject gameObject1, GameObject gameObject2)
        {
            GameObject go1 = gameObject1;
            GameObject go2 = gameObject2;
            while(go1 != null)
            {
                go2 = gameObject2;
                while (go2 != null)
                {
                    CollisionRect collisionRect1 = go1.pCollisionObject.pCollisionRect;
                    CollisionRect collisionRect2 = go2.pCollisionObject.pCollisionRect;
                    if (CollisionRect.Intersect(collisionRect1, collisionRect2))
                    {
                        go1.Accept(go2);
                        break;
                    }
                    go2 = (GameObject)go2.pSibling;
                }
                go1 = (GameObject)go1.pSibling;
            }
        }
        public void NotifyObservers()
        {
            this.subject.NotifyObservers();
        }
    }
}
