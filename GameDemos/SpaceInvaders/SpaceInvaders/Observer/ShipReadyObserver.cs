using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipReadyObserver : CollisionObserver
    {
        public override void Update()
        {
            Ship pShip = ShipManager.GetShip();
            pShip.SetState(ShipStateEnum.Ready);
            Missile pMissile = ShipManager.GetMissile();
            pMissile.SetActive(false);
        }
    }
}
