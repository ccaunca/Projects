using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class FallStraight : FallStrategy
    {
        private float oldY;
        public FallStraight()
        {
            this.oldY = 0.0f;
        }
        public override void Fall(Bomb pBomb)
        {
            Debug.Assert(pBomb != null);
        }

        public override void Reset(float y)
        {
            this.oldY = y;
        }
    }
}
