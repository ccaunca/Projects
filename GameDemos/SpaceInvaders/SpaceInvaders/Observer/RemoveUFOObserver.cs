using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveUFOObserver : CollisionObserver
    {
        public override void Update()
        {
            if (UFOManager.IsUFOActive())
            {
                UFOManager.DeactivateUFO();
                TimerManager.Remove(TimerManager.Find(TimerEventName.PlayUFOSound));
            }
        }
    }
}
