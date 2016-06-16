using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Crab : Alien
    {
        public float animationTime;
        public Crab(GameObjectName goName, SpriteBaseName spriteName, float x, float y, int idx)
            : base(goName, spriteName, AlienType.Crab, idx)
        {
            this.x = x;
            this.y = y;
        }
        ~Crab()
        {

        }
        public override void Update()
        {
            base.Update();
        }
        public override int getIndex()
        {
            throw new NotImplementedException();
        }

        public override void Accept(Visitor other)
        {
            other.VisitCrab(this);
        }
        public override void VisitMissileRoot(MissileRoot pMissileRoot)
        {
            CollisionPair.Collide((GameObject)pMissileRoot.pChild, this);
        }
        public override void VisitMissile(Missile pMissile)
        {   // we have a hit
            //Debug.WriteLine("Hit crab!");
            CollisionPair collisionPair = CollisionPairManager.GetActiveCollisionPair();
            collisionPair.SetCollision(pMissile, this);
            collisionPair.NotifyObservers();
        }
        public override void VisitBomb(Bomb pBomb)
        {

        }
    }
}
