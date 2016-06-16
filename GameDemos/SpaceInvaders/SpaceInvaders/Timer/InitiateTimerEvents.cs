using System;

namespace SpaceInvaders
{
    public class InitiateTimerEvents : Command
    {
        public override void execute(float currentTime)
        {
            TimerManager.InitializeTimerManager();
        }
    }
}
