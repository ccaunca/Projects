using System;

namespace SpaceInvaders
{
    public class UFOMoveRight : UFOStrategy
    {
        public UFOMoveRight()
        {
        }
        public override void Move(UFO pUFO)
        {
            pUFO.x += pUFO.speed;
        }
    }
}
