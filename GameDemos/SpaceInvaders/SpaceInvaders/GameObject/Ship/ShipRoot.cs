using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipRoot : Ship
    {
        public ShipRoot(GameObjectName goName, SpriteBaseName sName, float x, float y)
            : base(goName, sName, x, y)
        {
            this.x = x;
            this.y = y;
        }

        public override void Accept(Visitor other)
        {
            other.VisitShipRoot(this);
        }

        public override void VisitBombRoot(BombRoot pBombRoot)
        {
            CollisionPair.Collide((GameObject)pBombRoot.pChild, this);
        }
        public override void VisitBomb(Bomb pBomb)
        {
            CollisionPair.Collide(pBomb, (GameObject)this.pChild);
        }
        public override void VisitGrid(Grid pGrid)
        {
            CollisionPair pCollisionPair = CollisionPairManager.GetActiveCollisionPair();
            Debug.Assert(pCollisionPair != null);
            pCollisionPair.SetCollision(pGrid, this);
            pCollisionPair.NotifyObservers();
        }
        public override void Update()
        {
            base.UpdateBoundingBox();
            base.Update();
        }
    }
}
