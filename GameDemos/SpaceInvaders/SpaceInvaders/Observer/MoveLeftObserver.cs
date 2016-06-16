using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MoveLeftObserver : InputObserver
    {
        public override void Update()
        {
            if (GameManager.GetGame().GetState() is GameActiveState)
            {
                Ship pShip = ShipManager.GetShip();
                if (pShip.x < 55.0f)
                {   // TODO: synch this up with actual LeftWall x placement.
                    // Don't move
                }
                else
                {
                    pShip.MoveLeft();
                }
            }
        }
    }
}
