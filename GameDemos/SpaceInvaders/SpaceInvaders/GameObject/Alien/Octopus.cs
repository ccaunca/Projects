using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Octopus : Alien
    {
        public Octopus(GameObjectName goName, SpriteBaseName spriteName, float x, float y, int idx)
            : base(goName, spriteName, AlienType.Octopus, idx)
        {
            this.x = x;
            this.y = y;
        }
        ~Octopus()
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
            other.VisitOctopus(this);
        }
        public override void VisitMissileRoot(MissileRoot pMissileRoot)
        {
            CollisionPair.Collide((GameObject)pMissileRoot.pChild, this);
        }
        public override void VisitMissile(Missile pMissile)
        {
            //Debug.WriteLine("Hit Octopus!");
            CollisionPair collisionPair = CollisionPairManager.GetActiveCollisionPair();
            collisionPair.SetCollision(pMissile, this);
            collisionPair.NotifyObservers();
        }
        public override void VisitBomb(Bomb pBomb)
        {

        }
    }
}
