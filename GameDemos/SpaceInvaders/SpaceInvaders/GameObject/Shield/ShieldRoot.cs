using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldRoot : ShieldCategory
    {
        public ShieldRoot(GameObjectName goName, SpriteBaseName sName, float x, float y, int idx)
            : base(goName, sName, ShieldType.Root, idx)
        {
            this.x = x;
            this.y = y;
            SpriteBatch sb = new SpriteBatch();
            SpriteBatchNode sbNode = new SpriteBatchNode();
            sbNode.Set(SpriteManager.Find(SpriteBaseName.Null), sb);
            this.pProxySprite.SetSpriteBatchNode(sbNode);
        }

        public override void Accept(Visitor other)
        {
            other.VisitShieldRoot(this);
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
        public override void Update()
        {
            base.UpdateBoundingBox();
            base.Update();
        }
    }
}
