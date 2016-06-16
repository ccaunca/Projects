using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class InputObserver : OLink
    {
        public abstract void Update();
        public InputSubject pSubject;
    }
}
