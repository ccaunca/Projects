using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BombSpawnEvent : Command
    {
        public GameObject pBombRoot;
        public SpriteBatch sbAliens;
        public SpriteBatch sbBoxes;
        private Random pRandom;
        public BombSpawnEvent(Random pRandom)
        {
            this.pBombRoot = GameObjectManager.Find(GameObjectName.BombRoot);
            Debug.Assert(this.pBombRoot != null);

            this.sbAliens = SpriteBatchManager.Find(SpriteBatchName.Aliens);
            Debug.Assert(sbAliens != null);

            this.sbBoxes = SpriteBatchManager.Find(SpriteBatchName.Boxes);
            Debug.Assert(sbBoxes != null);

            this.pRandom = pRandom;
        }
        public override void execute(float currentTime)
        {
            PCSTree pTree = GameObjectManager.GetRootTree();

            float value = pRandom.Next(100, 760);
            Bomb pBomb = new Bomb(GameObjectName.Bomb, SpriteBaseName.BombStraight, new FallStraight(), value, 850.0f, 0);

            pBomb.ActivateCollisionSprite(this.sbBoxes);
            pBomb.ActivateGameSprite(this.sbAliens);
            pTree.Insert(pBomb, pBombRoot);
        }
    }
}
