using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class UFOManager
    {
        private UFO pUFO;
        private Bomb pBomb;
        private static Random pRandom = new Random();
        private static UFOManager pInstance;
        private bool isUFOActive;
        private bool isUFOBombActive;
        private UFOManager()
        {
            this.pUFO = null;
            this.pBomb = null;
            this.isUFOActive = false;
            this.isUFOBombActive = false;
        }
        ~UFOManager()
        {
            this.pUFO = null;
            this.pBomb = null;
        }
        public static void Create(bool isCollisionBoxActive)
        {
            if (pInstance == null)
            {
                pInstance = new UFOManager();
            }
        }
        public static void Activate()
        {
            UFOManager.SetUFOActive(true);
            UFOManager.SetUFOBombActive(true);
        }
        public static void Deactivate()
        {
            UFOManager.SetUFOActive(false);
            UFOManager.SetUFOBombActive(false);
        }
        public static UFO ActivateUFO(bool isCollisionBoxActive)
        {
            Debug.WriteLine("UFO has been activated!");
            UFOManager ufoMan = UFOManager.GetInstance();

            PCSTree pcsTree = GameObjectManager.GetRootTree();
            Debug.Assert(pcsTree != null);

            UFOStrategy strat = new UFOMoveRight();
            float x = -10.0f;
            if (pRandom.Next(0, 2) == 0)
            {
                strat = new UFOMoveLeft();
                x = 915.0f;
            }

            UFO pUFO = new UFO(GameObjectName.UFO, SpriteBaseName.UFO, strat, x, 905.0f);
            ufoMan.pUFO = pUFO;

            UFORoot pUFORoot = (UFORoot)GameObjectManager.Find(GameObjectName.UFORoot);
            SpriteBatch sbAliens = SpriteBatchManager.Find(SpriteBatchName.Aliens);
            SpriteBatch sbBoxes = SpriteBatchManager.Find(SpriteBatchName.Boxes);
            if (isCollisionBoxActive)
            {
                pUFO.ActivateCollisionSprite(sbBoxes);
            }
            pUFO.ActivateGameSprite(sbAliens);
            pcsTree.Insert(ufoMan.pUFO, pUFORoot);
            SetUFOActive(true);
            return ufoMan.pUFO;
        }
        public static void DeactivateUFO()
        {
            Debug.WriteLine("Deactivating UFO!");
            UFOManager ufoMan = UFOManager.GetInstance();
            ufoMan.pUFO.Remove();
            Debug.WriteLine("Deactivated UFO success!");
            SetUFOActive(false);
            SetUFOBombActive(false);
        }
        public static void DropBomb()
        {
            UFOManager ufoMan = UFOManager.GetInstance();
            UFO ufo = ufoMan.pUFO;
            PCSTree pcsTree = GameObjectManager.GetRootTree();
            Debug.Assert(pcsTree != null);

            Bomb pBomb = new Bomb(GameObjectName.UFOBomb, SpriteBaseName.UFOBomb, new FallStraight(), ufo.x, ufo.y, 0);
            pBomb.pProxySprite.pSprite.SetColor(ColorFactory.Create(ColorName.Red).pAzulColor);
            ufoMan.pBomb = pBomb;

            SpriteBatch sbAliens = SpriteBatchManager.Find(SpriteBatchName.Aliens);
            SpriteBatch sbBoxes = SpriteBatchManager.Find(SpriteBatchName.Boxes);

            pBomb.ActivateCollisionSprite(sbBoxes);
            pBomb.ActivateGameSprite(sbAliens);

            GameObject pBombRoot = GameObjectManager.Find(GameObjectName.BombRoot);
            Debug.Assert(pBombRoot != null);

            pcsTree.Insert(ufoMan.pBomb, pBombRoot);
            SetUFOBombActive(true);
            Debug.WriteLine("UFO Bomb dropped!");
        }
        public static void DeactivateBomb()
        {
            Debug.WriteLine("Deactivating UFO Bomb!");
            UFOManager ufoMan = UFOManager.GetInstance();
            ufoMan.pBomb.Remove();
            Debug.WriteLine("Deactivated UFO Bomb success!");
            SetUFOBombActive(false);
        }
        public static Bomb GetBomb()
        {
            UFOManager ufoMan = UFOManager.GetInstance();
            return ufoMan.pBomb;
        }
        public static Random GetRandom()
        {
            return pRandom;
        }
        public static UFO GetUFO()
        {
            UFOManager ufoMan = UFOManager.GetInstance();
            Debug.Assert(ufoMan.pUFO != null);
            return ufoMan.pUFO;
        }
        public static void MoveUFO(float x, float y)
        {
            UFO pUFO = GetUFO();
            pUFO.x = x;
            pUFO.y = y;
            pUFO.Update();
        }
        public static UFOManager GetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }
        public static void SetUFOActive(bool isActive)
        {
            UFOManager ufoMan = UFOManager.GetInstance();
            ufoMan.isUFOActive = isActive;
        }
        public static bool IsUFOActive()
        {
            UFOManager ufoMan = UFOManager.GetInstance();
            return ufoMan.isUFOActive;
        }
        public static void SetUFOBombActive(bool isActive)
        {
            UFOManager ufoMan = UFOManager.GetInstance();
            ufoMan.isUFOBombActive = isActive;
        }
        public static bool IsUFOBombActive()
        {
            UFOManager ufoMan = UFOManager.GetInstance();
            return ufoMan.isUFOBombActive;
        }
    }
}