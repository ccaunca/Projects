using System;

namespace SpaceInvaders
{
    public abstract class Command
    {
        public abstract void execute(float currentTime);
    }
}
