using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum FallType
    {
        Straight, Dagger, ZigZag
    }
    public abstract class FallStrategy
    {
        public abstract void Fall(Bomb pBomb);
        public abstract void Reset(float y);
    }
}
