using System;

namespace SpaceInvaders
{
    public enum UFOType
    {
        UFO, UFORoot, Uninitialized
    }
    public abstract class UFOCategory : Alien
    {
        private UFOType type;
        protected UFOCategory(GameObjectName goName, SpriteBaseName sName, UFOType type, int idx)
            : base(goName, sName, AlienType.UFO, idx)
        {
            this.type = type;
        }
        ~UFOCategory()
        {
        }
    }
}
