using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class CollisionObject
    {
        public CollisionRect pCollisionRect;
        public SpriteBox pCollisionSpriteBox;
        public CollisionObject(ProxySprite proxySprite)
        {   // we'll pass in proxySprites to generate collision boxes around
            Debug.Assert(proxySprite != null);
            Sprite pSprite = proxySprite.pSprite;
            Debug.Assert(pSprite != null);
            this.pCollisionRect = new CollisionRect(pSprite.GetScreenRect());
            //Debug.WriteLine("ProxySprite Rect for {0}:({1},{2}),w:{3},h:{4}", proxySprite.pSprite.name, proxySprite.x, proxySprite.y, proxySprite.pSprite.pScreenRect.width, proxySprite.pSprite.pScreenRect.height);
            //Debug.WriteLine("CollisionRect for {0}:({1},{2}),w:{3},h:{4}", proxySprite.pSprite.name, this.pCollisionRect.x, this.pCollisionRect.y, this.pCollisionRect.width, this.pCollisionRect.height);
            this.pCollisionSpriteBox = SpriteBoxManager.Add(SpriteBaseName.Box, this.pCollisionRect);
            Debug.Assert(this.pCollisionSpriteBox != null);
            this.pCollisionSpriteBox.pLineColor = ColorFactory.Create(ColorName.Red).pAzulColor;
        }
        ~CollisionObject()
        {

        }
        public void UpdatePos(float x, float y)
        {
            this.pCollisionRect.x = x;
            this.pCollisionRect.y = y;

            this.pCollisionSpriteBox.x = this.pCollisionRect.x;
            this.pCollisionSpriteBox.y = this.pCollisionRect.y;

            this.pCollisionSpriteBox.SetScreenRect(this.pCollisionRect.x, this.pCollisionRect.y, this.pCollisionRect.width, this.pCollisionRect.height);
            this.pCollisionSpriteBox.Update();
        }

        internal void Set(CollisionRect collisionTotal)
        {
            this.pCollisionRect.Set(collisionTotal);
            this.pCollisionSpriteBox.Set(SpriteBaseName.Box, collisionTotal.x, collisionTotal.y, collisionTotal.width, collisionTotal.height);
        }
    }
}
