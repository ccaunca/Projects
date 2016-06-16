using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipFactory
    {
        private PCSNode pParent;
        private PCSTree pTree;
        private SpriteBatch pSpriteBatch;
        private SpriteBatch pSpriteBoxBatch;
        public ShipFactory(SpriteBatchName sbName, PCSTree tree)
        {
            this.pSpriteBatch = SpriteBatchManager.Find(sbName);
            this.pSpriteBoxBatch = SpriteBatchManager.Find(SpriteBatchName.Boxes);
            Debug.Assert(this.pSpriteBatch != null);
            Debug.Assert(this.pSpriteBoxBatch != null);
            this.pTree = tree;
        }
        ~ShipFactory()
        {

        }
        public void SetParent(PCSNode pNode)
        {
            this.pParent = pNode;
        }
        public Alien Create(GameObjectName goName, float x, float y)
        {
            Alien pAlien = null;
            switch (goName)
            {
                case GameObjectName.Ship:
                    pAlien = new Ship(goName, SpriteBaseName.Ship, x, y);
                    break;
                case GameObjectName.Missile:
                    pAlien = new Missile(x, y);
                    break;
            }
            Debug.Assert(pAlien != null);
            //this.pTree.dumpTree();
            this.pTree.Insert(pAlien, this.pParent);
            //this.pTree.dumpTree();
            pAlien.ActivateGameSprite(this.pSpriteBatch);
            this.pSpriteBoxBatch.Attach(pAlien.pCollisionObject.pCollisionSpriteBox);
            return pAlien;
            
        }
    }
}
