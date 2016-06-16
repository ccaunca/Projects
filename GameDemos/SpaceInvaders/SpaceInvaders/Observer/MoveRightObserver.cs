using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MoveRightObserver : InputObserver
    {
        public override void Update()
        {
            if (GameManager.GetGame().GetState() is GameActiveState)
            {
                Ship pShip = ShipManager.GetShip();
                if (pShip.x > 830.0f)
                {   // TODO: synch this up with actual RighttWall x placement.
                    // Don't move
                }
                else
                {
                    pShip.MoveRight();
                }
            }
        }
    }
}
