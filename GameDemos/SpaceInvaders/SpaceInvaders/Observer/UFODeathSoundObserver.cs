using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFODeathSoundObserver : CollisionObserver
    {
        public override void Update()
        {
            SoundManager.PlaySound(SoundManager.Find(SoundName.ufoLowPitch));
        }
    }
}
