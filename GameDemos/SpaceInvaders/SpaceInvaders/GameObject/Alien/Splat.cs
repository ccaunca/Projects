using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Splat : Alien
    {
        public Splat(GameObjectName goName, SpriteBaseName sName, float x, float y, int idx)
            : base(goName, sName, AlienType.Splat, idx)
        {
            this.x = x;
            this.y = y;
            this.pProxySprite.x = x;
            this.pProxySprite.y = y;
            this.pCollisionObject.pCollisionSpriteBox.pLineColor = ColorFactory.Create(ColorName.Black).pAzulColor;
        }
        ~Splat()
        {
        }
        public override void Accept(Visitor other)
        {
            throw new NotImplementedException();
        }
    }
}
