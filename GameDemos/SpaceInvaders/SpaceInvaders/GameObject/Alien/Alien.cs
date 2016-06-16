using System;

namespace SpaceInvaders
{
    public enum AlienType
    {
        Squid,
        Crab,
        Octopus,
        Grid,
        Column,
        Ship,
        Missile,
        Wall,
        Splat,
        Explosion,
        UFO
    }
    public abstract class Alien : GameObject
    {
        public AlienType alienType;
        public Alien(GameObjectName goName, SpriteBaseName spriteName, AlienType alienType, int idx)
            : base(goName, spriteName, idx)
        {
            this.alienType = alienType;
        }
        ~Alien()
        {

        }
    }
}
