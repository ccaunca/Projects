using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienFactory
    {
        private PCSNode pParent;
        public PCSTree pTree;
        private SpriteBatch pSpriteBatch;
        private Random pRandom;
        public AlienFactory(SpriteBatchName sbName, PCSTree tree, Random random)
        {
            this.pSpriteBatch = SpriteBatchManager.Find(sbName);
            Debug.Assert(this.pSpriteBatch != null);
            this.pTree = tree;
            Debug.Assert(this.pTree != null);
            this.pRandom = random;
        }
        ~AlienFactory()
        {
            this.pParent = null;
            this.pTree = null;
            this.pSpriteBatch = null;
        }
        public void SetParent(PCSNode pNode)
        {
            this.pParent = pNode;
        }
        public Alien Create(AlienType alienType, GameObjectName goName, int goIdx = 0, float x = 0.0f, float y = 0.0f)
        {
            Alien pAlien = null;
            switch(alienType)
            {
                case AlienType.Crab :
                    pAlien = new Crab(goName, SpriteBaseName.Crab, x, y, goIdx);
                    break;
                case AlienType.Squid :
                    pAlien = new Squid(goName, SpriteBaseName.Squid, x, y, goIdx);
                    break;
                case AlienType.Octopus :
                    pAlien = new Octopus(goName, SpriteBaseName.Octopus, x, y, goIdx);
                    break;
                case AlienType.Grid :
                    pAlien = new Grid(goName, SpriteBaseName.Null, x, y, goIdx);
                    GameObjectManager.AttachTree(pAlien);
                    break;
                case AlienType.Column :
                    pAlien = new Column(goName, SpriteBaseName.Null, x, y, goIdx, this.pRandom);
                    break;
                default :
                    Debug.Assert(pAlien != null);
                    break;
            }
            this.pTree.Insert(pAlien, this.pParent);
            pAlien.ActivateGameSprite(this.pSpriteBatch);
            pAlien.ActivateCollisionSprite(this.pSpriteBatch);
            return pAlien;
        }
    }
}
