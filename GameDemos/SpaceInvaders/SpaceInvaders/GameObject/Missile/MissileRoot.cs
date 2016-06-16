using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MissileRoot : MissileCategory
    {
        public MissileRoot(GameObjectName goName, SpriteBaseName sName, float x, float y, int idx)
            : base(goName, sName, MissileType.MissileRoot, idx)
        {
            this.x = x;
            this.y = y;

            this.pCollisionObject.pCollisionSpriteBox.pLineColor = ColorFactory.Create(ColorName.Red).pAzulColor;
        }
        public override void Accept(Visitor other)
        {
            other.VisitMissileRoot(this);
        }
        public override void VisitBombRoot(BombRoot pBombRoot)
        {
            CollisionPair.Collide((GameObject)pBombRoot.pChild, this);
        }
        public override void VisitBomb(Bomb pBomb)
        {
            CollisionPair.Collide(pBomb, (GameObject)this.pChild);
        }
        public override void VisitUFORoot(UFORoot pUFORoot)
        {
            CollisionPair.Collide((GameObject)pUFORoot.pChild, this);
        }
        public override void Update()
        {
            base.UpdateBoundingBox();
            base.Update();
        }
    }
}
