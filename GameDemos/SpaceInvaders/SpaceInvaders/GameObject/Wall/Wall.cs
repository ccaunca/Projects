using System;

namespace SpaceInvaders
{
    public enum WallType
    {
        Root,
        Right,
        Left,
        Bottom,
        Top,
        Uninitialized
    }
    public abstract class Wall : GameObject
    {
        public WallType type;
        protected Wall(GameObjectName goName, SpriteBaseName spriteName, WallType wallType, int idx)
            : base(goName, spriteName, idx)
        {
            this.type = wallType;
        }
        ~Wall()
        {
        }
    }
}
