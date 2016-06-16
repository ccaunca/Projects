using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldBrick : ShieldCategory
    {
        public ShieldBrick(GameObjectName goName, SpriteBaseName sName, float x, float y, int idx)
            : base(goName, sName, ShieldType.Brick, idx)
        {
            this.x = x;
            this.y = y;
            this.pCollisionObject.pCollisionSpriteBox.pLineColor = ColorFactory.Create(ColorName.White).pAzulColor;
        }
        ~ShieldBrick()
        {

        }
        public override void Accept(Visitor other)
        {
            other.VisitShieldBrick(this);
        }
        public override void VisitMissile(Missile pMissile)
        {
            CollisionPair collisionPair = CollisionPairManager.GetActiveCollisionPair();
            collisionPair.SetCollision(pMissile, this);
            collisionPair.NotifyObservers();
        }
        public override void VisitBomb(Bomb pBomb)
        {
            CollisionPair collisionPair = CollisionPairManager.GetActiveCollisionPair();
            collisionPair.SetCollision(pBomb, this);
            collisionPair.NotifyObservers();
        }
        public override void Update()
        {
            base.Update();
        }
    }
}
