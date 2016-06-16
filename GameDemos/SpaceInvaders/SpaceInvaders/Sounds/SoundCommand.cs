using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SoundCommand : Command
    {
        public Sound pSound;
        public SoundCommand(SoundName soundName)
        {
            this.pSound = SoundManager.Find(soundName);
            Debug.Assert(this.pSound != null);
        }
        public override void execute(float currentTime)
        {
            SoundManager.PlaySound(this.pSound);
        }
    }
}
