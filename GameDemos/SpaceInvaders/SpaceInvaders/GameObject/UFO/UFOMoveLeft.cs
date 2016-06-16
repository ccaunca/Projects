using System;

namespace SpaceInvaders
{
    public class UFOMoveLeft : UFOStrategy
    {
        public UFOMoveLeft()
        {

        }
        public override void Move(UFO pUFO)
        {
            pUFO.x -= pUFO.speed;
        }
    }
}
