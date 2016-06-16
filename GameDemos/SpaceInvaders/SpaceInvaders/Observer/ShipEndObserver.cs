using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipEndObserver : CollisionObserver
    {
        public override void Update()
        {
            Ship pShip = ShipManager.GetShip();
            pShip.SetState(ShipStateEnum.End);
        }
    }
}
