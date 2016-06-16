using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShootObserver : InputObserver
    {
        public override void Update()
        {
            if (GameManager.GetGame().GetState() is GameActiveState)
            {
                Ship pShip = ShipManager.GetShip();
                pShip.Shoot();
            }
        }
    }
}
