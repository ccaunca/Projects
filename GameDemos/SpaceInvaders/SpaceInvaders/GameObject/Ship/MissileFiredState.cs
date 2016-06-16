using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MissileFlyingState : State
    {
        public Ship ship;
        public MissileFlyingState(Ship pShip)
        {
            Debug.Assert(pShip != null);
            this.ship = pShip;
        }
        public override void MoveRight()
        {
            Debug.WriteLine("Ship moved right");
        }

        public override void MoveLeft()
        {
            Debug.WriteLine("Ship moved left");
        }

        public override void Shoot()
        {
            Debug.WriteLine("Cannot shoot, missile is still active");
        }
    }
}
