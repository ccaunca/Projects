using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallRoot : Wall
    {
        public WallRoot(GameObjectName goName, SpriteBaseName sbName, float x, float y, int idx)
            : base(goName, sbName, WallType.Root, idx)
        {
            this.x = x;
            this.y = y;

            this.pCollisionObject.pCollisionSpriteBox.pLineColor = ColorFactory.Create(ColorName.White).pAzulColor;
            SpriteBatch sb = new SpriteBatch();
            SpriteBatchNode sbNode = new SpriteBatchNode();
            sbNode.Set(SpriteManager.Find(SpriteBaseName.Null), sb);
            this.pProxySprite.SetSpriteBatchNode(sbNode);
        }

        public override void Accept(Visitor other)
        {
            other.VisitWallRoot(this);
        }
        public override void Update()
        {
            base.UpdateBoundingBox();
            base.Update();
        }
        public override void VisitGrid(Grid pGrid)
        {
            CollisionPair.Collide(pGrid, (GameObject)this.pChild);
        }
        public override void VisitMissileRoot(MissileRoot pMissileRoot)
        {
            CollisionPair.Collide((GameObject)pMissileRoot.pChild, this);
        }
        public override void VisitMissile(Missile pMissile)
        {
            CollisionPair.Collide(pMissile, (GameObject)this.pChild);
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
            CollisionPair.Collide(pUFORoot, (GameObject)this.pChild);
        }
    }
}
