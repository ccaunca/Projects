using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BombSpawnEvent : Command
    {
        public GameObject pBombRoot;
        public SpriteBatch sbAliens;
        public SpriteBatch sbBoxes;
        private Grid pGrid;
        public BombSpawnEvent(Grid pGrid)
        {
            this.pBombRoot = GameObjectManager.Find(GameObjectName.BombRoot);
            Debug.Assert(this.pBombRoot != null);

            this.sbAliens = SpriteBatchManager.Find(SpriteBatchName.Aliens);
            Debug.Assert(sbAliens != null);

            this.sbBoxes = SpriteBatchManager.Find(SpriteBatchName.Boxes);
            Debug.Assert(sbBoxes != null);

            this.pGrid = pGrid;
        }
        public override void execute(float currentTime)
        {
            SpawnBomb();
        }
        private void SpawnBomb()
        {
            int random = this.pGrid.pRandom.Next(0, pGrid.colCount);
            Column pColumn = pGrid.GetColumn(random);
            //Debug.WriteLine("Firing from Column{0}", random);
            if (pColumn != null)
            {
                PCSTree pTree = GameObjectManager.GetRootTree();
                SpriteBaseName sName = SpriteBaseName.Null;
                FallStrategy pStrat = null;
                switch (pGrid.pRandom.Next(0, 3))
                {
                    case 0:
                        sName = SpriteBaseName.BombStraight;
                        pStrat = new FallStraight();
                        break;
                    case 1:
                        sName = SpriteBaseName.BombDagger;
                        pStrat = new FallDagger();
                        break;
                    case 2:
                    default:
                        sName = SpriteBaseName.BombZigZag;
                        pStrat = new FallZigZag();
                        break;
                }
                Bomb pBomb = new Bomb(GameObjectName.Bomb, sName, pStrat, pColumn.x, pColumn.pCollisionObject.pCollisionRect.minY, 0);
                pBomb.ActivateCollisionSprite(this.sbBoxes);
                pBomb.ActivateGameSprite(this.sbAliens);
                pTree.Insert(pBomb, this.pBombRoot);
            }
        }
    }
}
