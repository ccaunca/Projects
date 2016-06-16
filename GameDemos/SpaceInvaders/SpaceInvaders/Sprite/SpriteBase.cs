using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum SpriteBaseName
    {
        Octopus,
        Squid,
        Crab,
        Box,
        Shield,
        Ship,
        Missile,
        UFO, UFOBomb,
        BombStraight, BombDagger, BombZigZag,
        Splat, Explosion,
        Brick, BrickLeftTop0, BrickLeftTop1, BrickLeftBottom,
        BrickRightTop0, BrickRightTop1, BrickRightBottom,
        Null,
        Uninitialized,
    }
    abstract public class SpriteBase : DLink
    {
        #region Properties
        public SpriteBatchNode pSpriteBatchNode;
        #endregion
        #region Constructor
        protected SpriteBase()
        {
            this.pSpriteBatchNode = null;
        }
        ~SpriteBase()
        {
            this.pSpriteBatchNode = null;
        }
        public SpriteBatchNode GetSpriteBatchNode()
        {
            Debug.Assert(this.pSpriteBatchNode != null);
            return this.pSpriteBatchNode;
        }
        public void SetSpriteBatchNode(SpriteBatchNode pSpriteBatchNode)
        {
            Debug.Assert(pSpriteBatchNode != null);
            this.pSpriteBatchNode = pSpriteBatchNode;
        }
        #endregion
        #region Abstract Method Definitions
        abstract public void Render();
        abstract public void Update();
        abstract public Enum GetName();
        #endregion
    }
}
