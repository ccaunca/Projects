using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallFactory
    {
        private PCSNode pParent;
        private PCSTree pTree;
        private SpriteBatch pSpriteBoxBatch;
        public WallFactory(PCSTree tree)
        {
            this.pSpriteBoxBatch = SpriteBatchManager.Find(SpriteBatchName.Boxes);
            Debug.Assert(this.pSpriteBoxBatch != null);
            this.pTree = tree;
        }
        ~WallFactory()
        {

        }
        public void SetParent(PCSNode pNode)
        {
            this.pParent = pNode;
        }
        public Wall Create(float x, float y)
        {
            Wall pWall = null;
            pWall = new Wall(GameObjectName.Wall, SpriteBaseName.Uninitialized, x, y, 0);
            Debug.Assert(pWall != null);
            //GameObjectManager.Add(pWall);
            //this.pTree.dumpTree();
            this.pTree.Insert(pWall, this.pParent);
            //this.pTree.dumpTree();
            //this.pSpriteBoxBatch.Attach(pWall.pCollisionObject.pCollisionSpriteBox);
            return pWall;
        }
    }
}
