using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienFactory
    {
        private PCSNode pParent;
        private PCSTree pTree;
        private SpriteBatch pSpriteBatch;
        private SpriteBatch pSpriteBoxBatch;
        public AlienFactory(SpriteBatchName sbName, PCSTree tree)
        {
            this.pSpriteBatch = SpriteBatchManager.Find(sbName);
            this.pSpriteBoxBatch = SpriteBatchManager.Find(SpriteBatchName.Boxes);
            Debug.Assert(this.pSpriteBatch != null);
            this.pTree = tree;
        }
        ~AlienFactory()
        {

        }
        public void SetParent(PCSNode pNode)
        {
            this.pParent = pNode;
        }
        public Alien Create(AlienType alienType, float x, float y)
        {
            Alien pAlien = null;
            switch(alienType)
            {
                case AlienType.Crab :
                    pAlien = new Crab(GameObjectName.Crab, SpriteBaseName.Crab, x, y);
                    break;
                case AlienType.Squid :
                    pAlien = new Squid(GameObjectName.Squid, SpriteBaseName.Squid, x, y);
                    break;
                case AlienType.Octopus :
                    pAlien = new Octopus(GameObjectName.Octopus, SpriteBaseName.Octopus, x, y);
                    break;
                case AlienType.Hierarchy :
                    pAlien = new Grid();
                    break;
                case AlienType.Ship :
                    pAlien = new Ship(GameObjectName.Ship, SpriteBaseName.Ship, x, y);
                    break;
                default :
                    Debug.Assert(pAlien != null);
                    break;
            }
            GameObjectManager.Add(pAlien);
            //this.pTree.dumpTree();
            if (alienType != AlienType.Ship)
            {
                this.pTree.Insert(pAlien, this.pParent);
            }
            //this.pTree.dumpTree();
            this.pSpriteBatch.Attach(pAlien.pProxySprite);
            this.pSpriteBoxBatch.Attach(pAlien.pCollisionObject.pCollisionSpriteBox);
            return pAlien;
        }
    }
}
