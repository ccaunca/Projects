using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallLeft : Wall
    {
        public WallLeft(GameObjectName goName, SpriteBaseName sName, float x, float y, float w, float h, int idx)
            : base(goName, sName, WallType.Left, idx)
        {
            this.pCollisionObject.pCollisionRect.Set(x, y, w, h);

            this.x = x;
            this.y = y;
            this.pCollisionObject.pCollisionSpriteBox.pLineColor = ColorFactory.Create(ColorName.Yellow).pAzulColor;
        }
        public override void Accept(Visitor other)
        {
            other.VisitWallLeft(this);
        }
        public override void Update()
        {
            base.Update();
        }
        public override void VisitGrid(Grid pGrid)
        {
            CollisionPair pCollisionPair = CollisionPairManager.GetActiveCollisionPair();
            Debug.Assert(pCollisionPair != null);
            pCollisionPair.SetCollision(pGrid, this);
            pCollisionPair.NotifyObservers();
        }
        public override void VisitUFORoot(UFORoot pUFORoot)
        {
            CollisionPair pCollisionPair = CollisionPairManager.GetActiveCollisionPair();
            Debug.Assert(pCollisionPair != null);
            pCollisionPair.SetCollision(pUFORoot, this);
            pCollisionPair.NotifyObservers();
        }
        public override void VisitBomb(Bomb pBomb)
        {
            
        }
    }
}
