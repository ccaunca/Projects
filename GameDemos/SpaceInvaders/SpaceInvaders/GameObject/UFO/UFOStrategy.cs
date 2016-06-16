using System;

namespace SpaceInvaders
{
    public enum MoveStrategy
    {
        Left, Right
    }
    public abstract class UFOStrategy
    {
        public abstract void Move(UFO pUFO);
    }
}
