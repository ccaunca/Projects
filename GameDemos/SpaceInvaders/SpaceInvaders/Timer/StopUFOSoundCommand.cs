using System;
using System.Diagnostics;
namespace SpaceInvaders
{
    public class StopUFOSoundCommand : Command
    {
        public override void execute(float currentTime)
        {
            TimerManager.Remove(TimerManager.Find(TimerEventName.PlayUFOSound));
        }
    }
}
