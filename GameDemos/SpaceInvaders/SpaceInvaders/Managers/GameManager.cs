using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GameManager
    {
        private Game pGame;
        private GameInstructionsState pGameInstructionsState;
        private GameSelectState pGameSelectState;
        private GameActiveState pGameActiveState;
        private GameOverState pGameOverState;
        private static GameManager pInstance;
        private bool areCollisionBoxesActive;
        private GameManager(bool areCollisionBoxesActive)
        {
            this.areCollisionBoxesActive = areCollisionBoxesActive;
            this.pGame = new Game();//new Game(false, areCollisionBoxesActive);
            this.pGameInstructionsState = new GameInstructionsState();
            this.pGameSelectState = new GameSelectState();
            this.pGameActiveState = new GameActiveState();
            this.pGameOverState = new GameOverState();
        }
        ~GameManager()
        {
            this.pGameInstructionsState = null;
            this.pGameSelectState = null;
            this.pGameActiveState = null;
            this.pGameOverState = null;
        }
        public static void Create(bool areCollisionBoxesActive)
        {
            if (pInstance == null)
            {
                pInstance = new GameManager(areCollisionBoxesActive);
            }
            pInstance.pGame.SetState(GameStateEnum.Instructions);
        }
        //public static void Initialize()
        //{
        //    GameManager gameMan = GameManager.GetInstance();
        //    gameMan.pGame = new Game(false, gameMan.areCollisionBoxesActive);
        //}
        public static GameManager GetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }
        public static Game GetGame()
        {
            GameManager gameMan = GameManager.GetInstance();
            Debug.Assert(gameMan.pGame != null);
            return gameMan.pGame;
        }
        public static bool GetCollisionBoxes()
        {
            GameManager gameMan = GameManager.GetInstance();
            return gameMan.areCollisionBoxesActive;
        }
        public static void Draw()
        {
            GameManager gameMan = GameManager.GetInstance();
            gameMan.pGame.Draw();
        }
        public static void ClearGameScreen()
        {
            // Remove Bombs
            GameObject bombRoot = GameObjectManager.Find(GameObjectName.BombRoot);
            PCSTreeReverseIterator iterBomb = new PCSTreeReverseIterator(bombRoot);
            GameObject pGoBomb = (GameObject)iterBomb.First();
            while (!iterBomb.IsDone())
            {
                pGoBomb.Remove();
                pGoBomb = (GameObject)iterBomb.Next();
            }
            Missile pMissile = ShipManager.GetMissile();
            if (pMissile != null && pMissile.enabled)
            {
                pMissile.Remove();
            }
            // Remove Shields
            for (int i = 1; i < 5; ++i)
            {
                GameObject shieldRoot = GameObjectManager.Find(GameObjectName.ShieldRoot, i);
                PCSTreeReverseIterator iter = new PCSTreeReverseIterator(shieldRoot);
                GameObject pGO = (GameObject)iter.First();
                while (!iter.IsDone())
                {
                    pGO.Remove();
                    pGO = (GameObject)iter.Next();
                }
            }
            // Remove Alien Grid
            GameObject grid = GameObjectManager.Find(GameObjectName.Grid);
            PCSTreeReverseIterator iterGrid = new PCSTreeReverseIterator(grid);
            GameObject pGameObj = (GameObject)iterGrid.First();
            while (!iterGrid.IsDone())
            {
                pGameObj.Remove();
                pGameObj = (GameObject)iterGrid.Next();
            }
            // Remove CollisionBoxes
            if (GameManager.GetCollisionBoxes())
            {
                GameObject pWallRoot = GameObjectManager.Find(GameObjectName.WallRoot);
                PCSTreeReverseIterator iterWall = new PCSTreeReverseIterator(pWallRoot);
                GameObject pGO = (GameObject)iterWall.First();
                while (!iterWall.IsDone())
                {
                    pGO.Remove();
                    pGO = (GameObject)iterWall.Next();
                }
            }
        }
        public static void ActivateGame(bool isFirstRound)
        {
            GameManager gameMan = GameManager.GetInstance();
            gameMan.pGame = new Game(isFirstRound, gameMan.areCollisionBoxesActive);
        }
        public static GameState GetState(GameStateEnum gameState)
        {
            GameManager gameMan = GameManager.GetInstance();
            GameState pGameState = null;
            switch (gameState)
            {
                case GameStateEnum.Active :
                    pGameState = gameMan.pGameActiveState;
                    break;
                case GameStateEnum.Over :
                    pGameState = gameMan.pGameOverState;
                    break;
                case GameStateEnum.Select :
                    pGameState = gameMan.pGameSelectState;
                    break;
                case GameStateEnum.Instructions :
                    pGameState = gameMan.pGameInstructionsState;
                    break;
                default :
                    Debug.Assert(false);
                    break;
            }
            return pGameState;
        }
        
    }
}
