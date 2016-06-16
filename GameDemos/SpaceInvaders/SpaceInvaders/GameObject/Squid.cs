using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Squid : Alien
    {
        public Squid(GameObjectName goName, SpriteBaseName spriteName, float x, float y)
            : base(goName, spriteName, AlienType.Squid, x, y)
        {
        }
        ~Squid()
        {
        }
        public override int getIndex()
        {
            throw new NotImplementedException();
        }
    }
}
