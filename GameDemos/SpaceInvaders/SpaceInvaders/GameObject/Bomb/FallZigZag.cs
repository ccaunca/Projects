using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class FallZigZag : FallStrategy
    {
        private float oldY;
        public FallZigZag()
        {
            this.oldY = 0.0f;
        }
        public override void Fall(Bomb pBomb)
        {
            Debug.Assert(pBomb != null);
            float targetY = this.oldY - 1.0f * pBomb.GetBoundingBoxHeight();
            if (pBomb.y < targetY)
            {
                pBomb.MultiplyScale(-1.0f, 1.0f);
                this.oldY = targetY;
            }
        }

        public override void Reset(float y)
        {
            this.oldY = y;
        }
    }
}
