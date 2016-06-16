using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class FallDagger : FallStrategy
    {
        private float oldY;
        public FallDagger()
        {
            this.oldY = 0.0f;
        }
        public override void Fall(Bomb pBomb)
        {
            Debug.Assert(pBomb != null);
            float targetY = this.oldY - 1.0f * pBomb.GetBoundingBoxHeight();
            if(pBomb.y < targetY)
            {
                pBomb.MultiplyScale(1.0f, -1.0f);
                oldY = targetY;
            }
        }

        public override void Reset(float y)
        {
            this.oldY = y;
        }
    }
}
