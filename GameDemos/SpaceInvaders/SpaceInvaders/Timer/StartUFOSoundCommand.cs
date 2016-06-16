using System;
using System.Diagnostics;
namespace SpaceInvaders
{
    public class StartUFOSoundCommand : Command
    {
        public override void execute(float currentTime)
        {
            SoundManager.PlaySound(SoundManager.Find(SoundName.ufoHighPitch));
        }
    }
}
