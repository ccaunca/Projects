using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallBottom : Wall
    {
        public WallBottom(GameObjectName goName, SpriteBaseName sName, float x, float y, float w, float h, int idx)
            : base(goName, sName, WallType.Bottom, idx)
        {
            this.pCollisionObject.pCollisionRect.Set(x, y, w, h);
            this.x = x;
            this.y = y;
            this.pCollisionObject.pCollisionSpriteBox.pLineColor = ColorFactory.Create(ColorName.Yellow).pAzulColor;
        }
        public override void Accept(Visitor other)
        {
            other.VisitWallBottom(this);
        }
        public override void VisitBomb(Bomb pBomb)
        {
            CollisionPair cp = CollisionPairManager.GetActiveCollisionPair();
            cp.SetCollision(pBomb, this);
            cp.NotifyObservers();
        }
        public override void Update()
        {
            base.Update();
        }
    }
}
