using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFOBombSpawnEvent : Command
    {
        public override void execute(float currentTime)
        {
            if (UFOManager.IsUFOActive() && !UFOManager.IsUFOBombActive())
            {
                UFOManager.DropBomb();
            }
        }
    }
}
