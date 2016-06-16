using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum ShipStateEnum
    {
        Ready, MissileFlying, RightWall, End
    }
    public abstract class ShipState
    {
        public abstract void Handle(Ship pShip);
        public abstract void MoveRight(Ship pShip);
        public abstract void MoveLeft(Ship pShip);
        public abstract void Shoot(Ship pShip);
    }
}
