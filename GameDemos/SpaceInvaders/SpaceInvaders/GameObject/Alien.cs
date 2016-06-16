using System;

namespace SpaceInvaders
{
    public enum AlienType
    {
        Squid,
        Crab,
        Octopus,
        Hierarchy,
        Ship
    }
    public abstract class Alien : GameObject
    {
        public AlienType alienType;
        public Alien(GameObjectName goName, SpriteBaseName spriteName, AlienType alienType, float x, float y)
            : base(goName, GameObjectType.Alien, spriteName, x, y)
        {
            this.alienType = alienType;
        }
        ~Alien()
        {

        }
    }
}
