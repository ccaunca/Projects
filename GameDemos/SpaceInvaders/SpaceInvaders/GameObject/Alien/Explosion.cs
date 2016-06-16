using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum ExplosionType
    {
        Explosion, Splat
    }
    public class Explosion : Alien
    {
        public Explosion(GameObjectName goName, SpriteBaseName sName, AlienType alienType, GameObject go, ColorName color, int idx)
            : base(goName, sName, alienType, idx)
        {
            this.x = go.x;
            this.y = go.y;
            this.pProxySprite.x = x;
            this.pProxySprite.y = y;
            this.pProxySprite.pSprite.SetColor(ColorFactory.Create(color).pAzulColor);
        }
        ~Explosion()
        {
        }
        public override void Accept(Visitor other)
        {
            throw new NotImplementedException();
        }
    }
}
