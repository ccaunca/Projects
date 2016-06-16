using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveUFOCommand : Command
    {
        public override void execute(float currentTime)
        {
            if (UFOManager.IsUFOActive())
            {
                UFOManager.DeactivateUFO();
                TimerManager.Remove(TimerManager.Find(TimerEventName.PlayUFOSound));
            }
        }
    }
}
