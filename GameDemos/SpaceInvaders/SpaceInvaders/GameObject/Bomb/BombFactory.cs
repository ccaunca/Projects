using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BombFactory
    {
        private PCSNode pParent;
        public PCSTree pTree;
        private SpriteBatch pSpriteBatch;
        public BombRoot pBombRoot;
        public BombFactory(SpriteBatchName sbName, PCSTree tree)
        {
            this.pSpriteBatch = SpriteBatchManager.Find(sbName);
            Debug.Assert(this.pSpriteBatch != null);
            this.pTree = tree;
            Debug.Assert(this.pTree != null);
        }
        ~BombFactory()
        {
            this.pParent = null;
            this.pTree = null;
            this.pSpriteBatch = null;
        }
        public void SetParent(PCSNode pNode)
        {
            this.pParent = pNode;
        }
        public Bomb Create(float x, float y, FallType type)
        {
            Bomb pBomb = null;
            this.pBombRoot = new BombRoot(GameObjectName.BombRoot, SpriteBaseName.Null, 0.0f, 0.0f, 0);
            this.pTree.Insert(pBombRoot, null);
            pBombRoot.ActivateCollisionSprite(this.pSpriteBatch);
            pBomb = new Bomb(GameObjectName.Bomb, SpriteBaseName.BombStraight, new FallStraight(), x, y, 0);
            this.pTree.Insert(pBomb, pBombRoot);
            pBomb.ActivateCollisionSprite(this.pSpriteBatch);
            pBomb.ActivateGameSprite(this.pSpriteBatch);
            GameObjectManager.AttachTree(pBombRoot);
            return pBomb;
        }
    }
}
