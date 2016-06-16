using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipDeathSoundObserver : CollisionObserver
    {
        public override void Update()
        {
            SoundManager.PlaySound(SoundManager.Find(SoundName.explosion));
        }
    }
}
