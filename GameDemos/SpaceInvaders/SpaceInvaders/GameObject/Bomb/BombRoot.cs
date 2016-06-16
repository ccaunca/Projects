using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BombRoot : BombCategory
    {
        public BombRoot(GameObjectName goName, SpriteBaseName sName, float x, float y, int idx)
            : base(goName, sName, BombType.BombRoot, idx)
        {
            this.x = x;
            this.y = y;
            SpriteBatch sb = new SpriteBatch();
            SpriteBatchNode sbNode = new SpriteBatchNode();
            sbNode.Set(SpriteManager.Find(SpriteBaseName.Null), sb);
            this.pProxySprite.SetSpriteBatchNode(sbNode);
            this.pCollisionObject.pCollisionSpriteBox.pLineColor = ColorFactory.Create(ColorName.White).pAzulColor;
        }
        public override void Accept(Visitor other)
        {
            other.VisitBombRoot(this);
        }
        public override void Update()
        {
            base.UpdateBoundingBox();
            base.Update();
        }
    }
}
