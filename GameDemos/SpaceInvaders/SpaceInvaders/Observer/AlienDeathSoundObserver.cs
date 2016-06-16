using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienDeathSoundObserver : CollisionObserver
    {
        public override void Update()
        {
            SoundManager.PlaySound(SoundManager.Find(SoundName.shoot));
        }
    }
}
