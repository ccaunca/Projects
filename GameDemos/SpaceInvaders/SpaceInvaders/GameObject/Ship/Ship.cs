using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Ship : ShipCategory
    {
        private ShipState state;
        public float speed;
        public Ship(GameObjectName goName, SpriteBaseName sName, float x, float y)
            : base(goName, sName, ShipType.Ship, 0)
        {
            this.x = x;
            this.y = y;
            this.state = null;
            this.speed = 5.0f;
            this.pProxySprite.pSprite.pColor = ColorFactory.Create(ColorName.Green).pAzulColor;
            this.pCollisionObject.pCollisionSpriteBox.pLineColor = ColorFactory.Create(ColorName.Blue).pAzulColor;
        }
        ~Ship()
        {
        }
        public void MoveLeft()
        {
            this.state.MoveLeft(this);
        }
        public void MoveRight()
        {
            this.state.MoveRight(this);
        }
        public void Shoot()
        {
            this.state.Shoot(this);
        }
        public override void Update()
        {
            base.Update();
            //Debug.WriteLine("Missile active:{0}", ShipManager.GetMissile() == null ? "null" : ShipManager.GetMissile().enabled.ToString());
        }
        public override int getIndex()
        {
            throw new NotImplementedException();
        }
        public override void Accept(Visitor other)
        {
            other.VisitShip(this);
        }
        public override void VisitWallRight(WallRight pWallRight)
        {
            //Debug.WriteLine("Hit right wall!");
        }
        public override void VisitWallLeft(WallLeft pWallLeft)
        {
            //Debug.WriteLine("Hit left wall!");
        }
        public override void VisitBombRoot(BombRoot pBombRoot)
        {
            CollisionPair.Collide((GameObject)pBombRoot.pChild, this);
        }
        public override void VisitBomb(Bomb pBomb)
        {
            //Debug.WriteLine("BOMB HIT!");
            CollisionPair cp = CollisionPairManager.GetActiveCollisionPair();
            cp.SetCollision(pBomb, this);
            cp.NotifyObservers();
        }
        public void SetState(ShipStateEnum shipState)
        {
            this.state = ShipManager.GetState(shipState);
        }
    }
}
