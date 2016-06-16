using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class EndState : ShipState
    {
        public Ship ship;
        public override void Handle(Ship pShip)
        {
            Debug.WriteLine("Ship handled!");
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
            Debug.WriteLine("Ship can't shoot!");
        }
    }
}
