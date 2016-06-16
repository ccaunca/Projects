using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ReadyState : ShipState
    {
        public override void Handle(Ship pShip)
        {
            pShip.SetState(ShipStateEnum.MissileFlying);
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
            Missile pMissile = ShipManager.ActivateMissile();
            pMissile.SetPosition(pShip.x, pShip.y + 20);
            pMissile.SetActive(true);
            SoundManager.PlaySound(SoundManager.Find(SoundName.invaderKilled));
            this.Handle(pShip);
        }
    }
}
