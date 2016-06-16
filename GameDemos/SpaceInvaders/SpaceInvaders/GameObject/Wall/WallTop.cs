using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallTop : Wall
    {
        public WallTop(GameObjectName goName, SpriteBaseName sName, float x, float y, float w, float h, int idx)
            : base(goName, sName, WallType.Top, idx)
        {
            this.pCollisionObject.pCollisionRect.Set(x, y, w, h);
            this.x = x;
            this.y = y;
            this.pCollisionObject.pCollisionSpriteBox.pLineColor = ColorFactory.Create(ColorName.Orange).pAzulColor;
        }
        public override void Accept(Visitor other)
        {
            other.VisitWallTop(this);
        }
        public override void VisitMissileRoot(MissileRoot pMissileRoot)
        {
            CollisionPair.Collide((GameObject)pMissileRoot.pChild, this);
        }
        public override void VisitMissile(Missile pMissile)
        {
            CollisionPair collisionPair = CollisionPairManager.GetActiveCollisionPair();
            collisionPair.SetCollision(pMissile, this);
            collisionPair.NotifyObservers();
        }
        public override void VisitBomb(Bomb pBomb)
        {

        }
        public override void Update()
        {
            base.Update();
        }
    }
}
