using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteBatchNode : CLink
    {
        public SpriteBase pSpriteBase;
        private SpriteBatch pSpriteBatch;
        public SpriteBatchNode()
            : base()
        {
            this.pSpriteBase = null;
            this.pSpriteBatch = null;
        }
        public SpriteBatch GetSpriteBatch()
        {
            Debug.Assert(this.pSpriteBatch != null);
            return this.pSpriteBatch;
        }
        public void Set(SpriteBase sprite, SpriteBatch sb)
        {
            Debug.Assert(sprite != null);
            Debug.Assert(sb != null);

            this.pSpriteBase = sprite;
            this.pSpriteBatch = sb;

            Debug.Assert(this.pSpriteBase != null);
            this.pSpriteBase.SetSpriteBatchNode(this);
        }
        
    }
}