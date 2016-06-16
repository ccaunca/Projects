using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum ShieldType
    {
        Root, Grid,
        Column, Brick,
        LeftTop0, LeftTop1, LeftBottom,
        RightTop0, RightTop1, RightBottom,
        Uninitialized
    }
    public abstract class ShieldCategory : GameObject
    {
        private ShieldType type;
        protected ShieldCategory(GameObjectName goName, SpriteBaseName sName, ShieldType type, int idx)
            : base(goName, sName, idx)
        {
            this.type = type;
        }
        ~ShieldCategory()
        {

        }
    }
}
