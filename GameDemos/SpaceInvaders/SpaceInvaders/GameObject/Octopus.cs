using System;

namespace SpaceInvaders
{
    public class Octopus : Alien
    {
        public Octopus(GameObjectName goName, SpriteBaseName spriteName, float x, float y)
            : base(goName, spriteName, AlienType.Octopus, x, y)
        {
        }
        ~Octopus()
        {

        }
        public override int getIndex()
        {
            throw new NotImplementedException();
        }
    }
}
