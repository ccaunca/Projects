using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Bomb : BombCategory
    {
        public float speed;
        private FallStrategy fallStrategy;
        public Bomb(GameObjectName goName, SpriteBaseName sName, FallStrategy strat, float x, float y, int idx)
            : base(goName, sName, BombType.Bomb, idx)
        {
            this.x = x;
            this.y = y;
            this.speed = 5.0f;
            Debug.Assert(strat != null);
            this.fallStrategy = strat;
            this.fallStrategy.Reset(this.y);
            this.pCollisionObject.pCollisionSpriteBox.pLineColor = ColorFactory.Create(ColorName.Orange).pAzulColor;
        }
        public void Reset()
        {
            this.y = 800.0f;
            this.fallStrategy.Reset(this.y);
        }
        public override void Remove()
        {
            this.pCollisionObject.pCollisionRect.Set(0, 0, 0, 0);
            base.Update();
            GameObject pParent = (GameObject)this.pParent;
            if (pParent != null)
            {
                pParent.Update();
                base.Remove();
            }
        }
        public override void Update()
        {
            base.Update();
            this.y -= this.speed;
            this.fallStrategy.Fall(this);
        }
        public float GetBoundingBoxHeight()
        {
            return this.pCollisionObject.pCollisionRect.height;
        }
        public override void Accept(Visitor other)
        {
            other.VisitBomb(this);
        }
        public void SetPosition(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public void MultiplyScale(float sx, float sy)
        {
            Debug.Assert(this.pProxySprite != null);
            this.pProxySprite.sx *= sx;
            this.pProxySprite.sy *= sy;
        }
    }
}
