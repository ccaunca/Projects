using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldColumn : ShieldCategory
    {
        public ShieldColumn(GameObjectName goName, SpriteBaseName sName, float x, float y, int idx)
            : base(goName, sName, ShieldType.Column, idx)
        {
            this.x = x;
            this.y = y;
        }
        public override void Accept(Visitor other)
        {
            other.VisitShieldColumn(this);
        }
        public override void VisitMissile(Missile pMissile)
        {
            CollisionPair.Collide(pMissile, (GameObject)this.pChild);
        }
        public override void VisitBomb(Bomb pBomb)
        {
            CollisionPair.Collide(pBomb, (GameObject)this.pChild);
        }
        public override void Update()
        {
            base.UpdateBoundingBox();
            base.Update();
        }
    }
}
