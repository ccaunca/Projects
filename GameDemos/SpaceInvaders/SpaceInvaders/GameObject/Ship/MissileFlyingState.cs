using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MissileFlyingState : ShipState
    {
        public Ship ship;
        public override void Handle(Ship pShip)
        {

        }
        public override void MoveRight(Ship pShip)
        {
            pShip.x += pShip.speed;
        }

        public override void MoveLeft(Ship pShip)
        {
            pShip.x -= pShip.speed;
        }

        public override void Shoot(Ship pShip)
        {
            Debug.WriteLine("Cannot shoot, missile is still active");
        }
    }
}
