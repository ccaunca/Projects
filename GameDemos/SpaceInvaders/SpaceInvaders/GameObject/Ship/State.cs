using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class State
    {
        public abstract void Handle(Ship pShip);
        public abstract void MoveRight(Ship pShip);
        public abstract void MoveLeft(Ship pShip);
        public abstract void Shoot(Ship pShip);
    }
}
