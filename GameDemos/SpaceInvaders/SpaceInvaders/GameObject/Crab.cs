using System;

namespace SpaceInvaders
{
    public class Crab : Alien
    {
        public float animationTime;
        public Crab(GameObjectName goName, SpriteBaseName spriteName, float x, float y)
            : base(goName, spriteName, AlienType.Crab, x, y)
        {
        }
        ~Crab()
        {

        }
        public override int getIndex()
        {
            throw new NotImplementedException();
        }
    }
}
