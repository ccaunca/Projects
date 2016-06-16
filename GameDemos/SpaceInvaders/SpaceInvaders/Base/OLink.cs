using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class OLink
    {
        public OLink pONext;
        public OLink pOPrev;
        protected OLink()
        {
            this.pONext = null;
            this.pOPrev = null;
        }
    }
}
