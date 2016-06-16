using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum ShipType
    {
        Ship, ShipRoot, Uninitialized
    }
    public abstract class ShipCategory : GameObject
    {
        private ShipType type;
        protected ShipCategory(GameObjectName goName, SpriteBaseName sName, ShipType type, int idx)
            : base(goName, sName, idx)
        {
            this.type = type;
        }
        ~ShipCategory()
        {

        }
    }
}
