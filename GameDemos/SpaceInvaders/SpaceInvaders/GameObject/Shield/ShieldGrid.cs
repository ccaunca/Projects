using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldGrid : ShieldCategory
    {
        public ShieldGrid(GameObjectName goName, SpriteBaseName sName, float x, float y, int idx)
            : base(goName, sName, ShieldType.Grid, idx)
        {
            this.x = x;
            this.y = y;
        }
        ~ShieldGrid()
        {

        }
        public override void Accept(Visitor other)
        {
            other.VisitShieldGrid(this);
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
