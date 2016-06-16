using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Game
    {
        int _horizontalSpace = 65;
        float _gridStartingHeight = 850.0f;
        private GameState state;
        public int roundNum;
        public Game()
        {   // Demo/Select Screen
            this.roundNum = 1;
            TextureManager.Create(2, 2);
            ImageManager.Create(5, 2);
            SpriteManager.Create(5, 2);
            SpriteBoxManager.Create(1, 1);
            SpriteBatchManager.Create(2, 1);
            TimerManager.Create(3, 1);
            GameObjectManager.Create(3, 1);
            ProxySpriteManager.Create(10, 5);
            CollisionPairManager.Create(2, 1);
            SoundManager.Create(9, 1);
            FontManager.Create(26, 10);
            ScoreManager.Initialize();

            //---------------------------------------------------------------------------------------------------------
            // Load Textures
            //---------------------------------------------------------------------------------------------------------
            Texture pSpritesTexture = TextureManager.Add(TextureName.Sprites, "Sprites.tga");
            Texture pShieldTexture = TextureManager.Add(TextureName.Shields, "Shield.tga");
            TextureManager.Add(TextureName.Consolas36pt, "consolas36pt.tga");
            FontManager.AddXml("Consolas36pt.xml", FontName.Consolas36pt, TextureName.Consolas36pt);

            //---------------------------------------------------------------------------------------------------------
            // Load Sounds
            //---------------------------------------------------------------------------------------------------------
            SoundManager.Add(SoundName.explosion);
            SoundManager.Add(SoundName.fastInvader1);
            SoundManager.Add(SoundName.fastInvader2);
            SoundManager.Add(SoundName.fastInvader3);
            SoundManager.Add(SoundName.fastInvader4);
            SoundManager.Add(SoundName.invaderKilled);
            SoundManager.Add(SoundName.shoot);
            SoundManager.Add(SoundName.ufoHighPitch);
            SoundManager.Add(SoundName.ufoLowPitch);

            //---------------------------------------------------------------------------------------------------------
            // Create SpriteBatches
            //---------------------------------------------------------------------------------------------------------
            SpriteBatch sbAliens = SpriteBatchManager.Add(SpriteBatchName.Aliens);
            SpriteBatch sbBoxes = SpriteBatchManager.Add(SpriteBatchName.Boxes);
            SpriteBatch sbShips = SpriteBatchManager.Add(SpriteBatchName.Ships);
            SpriteBatch sbSplats = SpriteBatchManager.Add(SpriteBatchName.Splats);
            SpriteBatch sbShields = SpriteBatchManager.Add(SpriteBatchName.Shields);
            SpriteBatch sbFonts = SpriteBatchManager.Add(SpriteBatchName.Fonts);

            InitializeImageManager();

            //---------------------------------------------------------------------------------------------------------
            // Create Sprites
            //---------------------------------------------------------------------------------------------------------
            SpriteManager.Add(SpriteBaseName.UFO, ImageName.UFO, 200.0f, 200.0f, 50.0f, 50.0f);
            SpriteManager.Add(SpriteBaseName.UFOBomb, ImageName.Missile, 100.0f, 700.0f, 5.0f, 32.0f);
            SpriteManager.Add(SpriteBaseName.Squid, ImageName.SquidA, 100.0f, 800.0f, 50.0f, 50.0f);
            SpriteManager.Add(SpriteBaseName.Crab, ImageName.CrabA, 100.0f, 750.0f, 50.0f, 50.0f);
            SpriteManager.Add(SpriteBaseName.Octopus, ImageName.OctopusA, 100.0f, 700.0f, 50.0f, 50.0f);
            SpriteManager.Add(SpriteBaseName.Ship, ImageName.Ship, 100.0f, 700.0f, 50.0f, 50.0f);
            SpriteManager.Add(SpriteBaseName.Missile, ImageName.Missile, 100.0f, 700.0f, 5.0f, 32.0f);
            SpriteManager.Add(SpriteBaseName.BombStraight, ImageName.BombStraightA, 100.0f, 100.0f, 10.0f, 60.0f);
            SpriteManager.Add(SpriteBaseName.BombDagger, ImageName.BombDaggerA, 100.0f, 100.0f, 20.0f, 60.0f);
            SpriteManager.Add(SpriteBaseName.BombZigZag, ImageName.BombZigZagA, 200.0f, 200.0f, 20.0f, 60.0f);
            SpriteManager.Add(SpriteBaseName.Brick, ImageName.Brick, 50.0f, 25.0f, 40.0f, 20.0f);
            SpriteManager.Add(SpriteBaseName.BrickLeftTop0, ImageName.BrickLeftTop0, 50.0f, 25.0f, 40.0f, 20.0f);
            SpriteManager.Add(SpriteBaseName.BrickLeftTop1, ImageName.BrickLeftTop1, 50.0f, 25.0f, 40.0f, 20.0f);
            SpriteManager.Add(SpriteBaseName.BrickLeftBottom, ImageName.BrickLeftBottom, 50.0f, 25.0f, 40.0f, 20.0f);
            SpriteManager.Add(SpriteBaseName.BrickRightTop0, ImageName.BrickRightTop0, 50.0f, 25.0f, 40.0f, 20.0f);
            SpriteManager.Add(SpriteBaseName.BrickRightTop1, ImageName.BrickRightTop1, 50.0f, 25.0f, 40.0f, 20.0f);
            SpriteManager.Add(SpriteBaseName.BrickRightBottom, ImageName.BrickRightBottom, 50.0f, 25.0f, 40.0f, 20.0f);
            SpriteManager.Add(SpriteBaseName.Splat, ImageName.AlienExplosion, 200.0f, 200.0f, 50.0f, 50.0f);
            SpriteManager.Add(SpriteBaseName.Explosion, ImageName.ShipExplosion, 200.0f, 200.0f, 50.0f, 50.0f);
            
            // Input
            InputSubject inputSubject;
            inputSubject = InputManager.GetOneSubject();
            inputSubject.RegisterObserver(new OnePlayerObserver());

            inputSubject = InputManager.GetArrowLeftSubject();
            inputSubject.RegisterObserver(new MoveLeftObserver());

            inputSubject = InputManager.GetArrowRightSubject();
            inputSubject.RegisterObserver(new MoveRightObserver());

            inputSubject = InputManager.GetSpaceSubject();
            inputSubject.RegisterObserver(new ShootObserver());
            //inputSubject = InputManager.GetTwoSubject();
            //inputSubject.RegisterObserver(new TwoPlayerObserver());

            TimerManager.Add(TimerEventName.SetGameState, TimerManager.GetCurrentTime() + 5.0f, TimerManager.GetCurrentTime() + 5.0f, new GameSelectEvent());
        }
        public Game(bool isFirstRound, bool areCollisionBoxesActive)
        {   // Active Game
            this.SetState(GameStateEnum.Active);
            SpriteBatch sbBoxes = SpriteBatchManager.Find(SpriteBatchName.Boxes);
            SpriteBatch sbAliens = SpriteBatchManager.Find(SpriteBatchName.Aliens);
            PCSTree pRootTree = GameObjectManager.GetRootTree();
            WallRoot pWallRoot = (WallRoot)GameObjectManager.Find(GameObjectName.WallRoot);
            BombRoot pBombRoot = (BombRoot)GameObjectManager.Find(GameObjectName.BombRoot);
            MissileRoot pMissileRoot = (MissileRoot)GameObjectManager.Find(GameObjectName.MissileRoot);
            ShipRoot pShipRoot = (ShipRoot)GameObjectManager.Find(GameObjectName.ShipRoot);
            UFORoot pUFORoot = (UFORoot)GameObjectManager.Find(GameObjectName.UFORoot);
            ShieldRoot pShieldRoot1 = (ShieldRoot)GameObjectManager.Find(GameObjectName.ShieldRoot, 1);
            ShieldRoot pShieldRoot2 = (ShieldRoot)GameObjectManager.Find(GameObjectName.ShieldRoot, 2);
            ShieldRoot pShieldRoot3 = (ShieldRoot)GameObjectManager.Find(GameObjectName.ShieldRoot, 3);
            ShieldRoot pShieldRoot4 = (ShieldRoot)GameObjectManager.Find(GameObjectName.ShieldRoot, 4);
            if (isFirstRound)
            {
                // Create Walls
                pWallRoot = new WallRoot(GameObjectName.WallRoot, SpriteBaseName.Null, 0.0f, 0.0f, 0);
                pRootTree.Insert(pWallRoot, null);
                if (areCollisionBoxesActive)
                {
                    pWallRoot.ActivateCollisionSprite(sbBoxes);
                }
                WallTop pWallTop = new WallTop(GameObjectName.WallTop, SpriteBaseName.Null, 448.0f, 950.0f, 850.0f, 30.0f, 0);
                pRootTree.Insert(pWallTop, pWallRoot);
                if (areCollisionBoxesActive)
                {
                    pWallTop.ActivateCollisionSprite(sbBoxes);
                }
                pWallTop.ActivateGameSprite(sbAliens);

                WallRight pWallRight = new WallRight(GameObjectName.WallRight, SpriteBaseName.Null, 896.0f, 500.0f, 50.0f, 900.0f, 0);
                pRootTree.Insert(pWallRight, pWallRoot);
                if (areCollisionBoxesActive)
                {
                    pWallRight.ActivateCollisionSprite(sbBoxes);
                }
                pWallRight.ActivateGameSprite(sbAliens);

                WallLeft pWallLeft = new WallLeft(GameObjectName.WallLeft, SpriteBaseName.Null, 0.0f, 500.0f, 50.0f, 900.0f, 0);
                pRootTree.Insert(pWallLeft, pWallRoot);
                if (areCollisionBoxesActive)
                {
                    pWallLeft.ActivateCollisionSprite(sbBoxes);
                }
                pWallLeft.ActivateGameSprite(sbAliens);

                WallBottom pWallBottom = new WallBottom(GameObjectName.WallBottom, SpriteBaseName.Null, 448.0f, 50.0f, 850.0f, 30.0f, 0);
                pRootTree.Insert(pWallBottom, pWallRoot);
                if (areCollisionBoxesActive)
                {
                    pWallBottom.ActivateCollisionSprite(sbBoxes);
                }
                pWallBottom.ActivateGameSprite(sbAliens);
                GameObjectManager.AttachTree(pWallRoot);

                // Create ShipRoot and MissileRoot
                pMissileRoot = new MissileRoot(GameObjectName.MissileRoot, SpriteBaseName.Null, 0.0f, 0.0f, 0);
                pRootTree.Insert(pMissileRoot, null);
                if (areCollisionBoxesActive)
                {
                    pMissileRoot.ActivateCollisionSprite(sbBoxes);
                }
                GameObjectManager.AttachTree(pMissileRoot);
                pShipRoot = new ShipRoot(GameObjectName.ShipRoot, SpriteBaseName.Null, 0.0f, 0.0f);
                pRootTree.Insert(pShipRoot, null);
                if (areCollisionBoxesActive)
                {
                    pShipRoot.ActivateCollisionSprite(sbBoxes);
                }
                pShipRoot.ActivateGameSprite(sbAliens);
                GameObjectManager.AttachTree(pShipRoot);
                ShipManager.Create(areCollisionBoxesActive);

                // Create BombRoot
                pBombRoot = new BombRoot(GameObjectName.BombRoot, SpriteBaseName.Null, 0.0f, 0.0f, 0);
                pRootTree.Insert(pBombRoot, null);
                if (areCollisionBoxesActive)
                {
                    pBombRoot.ActivateCollisionSprite(sbBoxes);
                }
                GameObjectManager.AttachTree(pBombRoot);

                // Create UFORoot
                pUFORoot = new UFORoot(GameObjectName.UFORoot, SpriteBaseName.Null, 0.0f, 0.0f);
                pRootTree.Insert(pUFORoot, null);
                if (areCollisionBoxesActive)
                {
                    pUFORoot.ActivateCollisionSprite(sbBoxes);
                }
                pUFORoot.ActivateGameSprite(sbAliens);
                GameObjectManager.AttachTree(pUFORoot);
                UFOManager.Create(areCollisionBoxesActive);

                // Create Shields
                pShieldRoot1 = GenerateShield(pRootTree, 100.0f, 200.0f, 1);
                pShieldRoot2 = GenerateShield(pRootTree, 100.0f + 1 * 200, 200.0f, 2);
                pShieldRoot3 = GenerateShield(pRootTree, 100.0f + 2 * 200, 200.0f, 3);
                pShieldRoot4 = GenerateShield(pRootTree, 100.0f + 3 * 200, 200.0f, 4);
            }
            else
            {
                _gridStartingHeight -= 100.0f;
            }

            PopulateAlienGrid(_gridStartingHeight);
            Alien pGridRoot = (Alien)GameObjectManager.Find(GameObjectName.Grid);

            // Create CollisionPairs
            CollisionPair cpMissileWall = CollisionPairManager.Add(CollisionPairName.MissilevTop, pMissileRoot, pWallRoot);
            Debug.Assert(cpMissileWall != null);
            cpMissileWall.subject.RegisterObserver(new RemoveMissileObserver());
            cpMissileWall.subject.RegisterObserver(new ShipReadyObserver());

            CollisionPair cpBombWall = CollisionPairManager.Add(CollisionPairName.BombsvBottom, pBombRoot, pWallRoot);
            Debug.Assert(cpBombWall != null);
            cpBombWall.subject.RegisterObserver(new RemoveBombObserver());

            CollisionPair cpMissilevGrid = CollisionPairManager.Add(CollisionPairName.MissilevGrid, pMissileRoot, pGridRoot);
            Debug.Assert(cpMissilevGrid != null);
            cpMissilevGrid.subject.RegisterObserver(new RemoveMissileObserver());
            cpMissilevGrid.subject.RegisterObserver(new RemoveAlienObserver());
            cpMissilevGrid.subject.RegisterObserver(new ShipReadyObserver());
            cpMissilevGrid.subject.RegisterObserver(new AlienDeathSoundObserver());
            cpMissilevGrid.subject.RegisterObserver(new SplatObserver());
            cpMissilevGrid.subject.RegisterObserver(new UpdateScoreObserver());

            CollisionPair cpUFOMissile = CollisionPairManager.Add(CollisionPairName.MissilevUFO, pMissileRoot, pUFORoot);
            cpUFOMissile.subject.RegisterObserver(new RemoveMissileObserver());
            cpUFOMissile.subject.RegisterObserver(new RemoveUFOObserver());
            cpUFOMissile.subject.RegisterObserver(new ShipReadyObserver());
            cpUFOMissile.subject.RegisterObserver(new AlienDeathSoundObserver());
            cpUFOMissile.subject.RegisterObserver(new SplatObserver());
            cpUFOMissile.subject.RegisterObserver(new UpdateScoreObserver());
            cpUFOMissile.subject.RegisterObserver(new UFODeathSoundObserver());

            CollisionPair cpUFOWalls = CollisionPairManager.Add(CollisionPairName.UFOvWalls, pUFORoot, pWallRoot);
            cpUFOWalls.subject.RegisterObserver(new UFOWallObserver());

            CollisionPair cpGridvWalls = CollisionPairManager.Add(CollisionPairName.GridvWalls, pGridRoot, pWallRoot);
            Debug.Assert(cpGridvWalls != null);
            cpGridvWalls.subject.RegisterObserver(new GridXObserver());
            cpGridvWalls.subject.RegisterObserver(new GridYObserver());

            CollisionPair cpGridvShip = CollisionPairManager.Add(CollisionPairName.GridvShip, pGridRoot, pShipRoot);
            cpGridvShip.subject.RegisterObserver(new GameOverObserver());
            cpGridvShip.subject.RegisterObserver(new ShipDeathSoundObserver());

            CollisionPair cpMissileShield1 = CollisionPairManager.Add(CollisionPairName.MissilevShields, pMissileRoot, pShieldRoot1);
            cpMissileShield1.subject.RegisterObserver(new RemoveMissileObserver());
            cpMissileShield1.subject.RegisterObserver(new RemoveBrickObserver());
            cpMissileShield1.subject.RegisterObserver(new ShipReadyObserver());
            CollisionPair cpBombShield1 = CollisionPairManager.Add(CollisionPairName.BombsvShields, pBombRoot, pShieldRoot1);
            cpBombShield1.subject.RegisterObserver(new RemoveBombObserver());
            cpBombShield1.subject.RegisterObserver(new RemoveBrickObserver());

            CollisionPair cpMissileShield2 = CollisionPairManager.Add(CollisionPairName.MissilevShields, pMissileRoot, pShieldRoot2);
            cpMissileShield2.subject.RegisterObserver(new RemoveMissileObserver());
            cpMissileShield2.subject.RegisterObserver(new RemoveBrickObserver());
            cpMissileShield2.subject.RegisterObserver(new ShipReadyObserver());
            CollisionPair cpBombShield2 = CollisionPairManager.Add(CollisionPairName.BombsvShields, pBombRoot, pShieldRoot2);
            cpBombShield2.subject.RegisterObserver(new RemoveBombObserver());
            cpBombShield2.subject.RegisterObserver(new RemoveBrickObserver());

            CollisionPair cpBombShield3 = CollisionPairManager.Add(CollisionPairName.BombsvShields, pBombRoot, pShieldRoot3);
            cpBombShield3.subject.RegisterObserver(new RemoveBombObserver());
            cpBombShield3.subject.RegisterObserver(new RemoveBrickObserver());
            CollisionPair cpMissileShield3 = CollisionPairManager.Add(CollisionPairName.MissilevShields, pMissileRoot, pShieldRoot3);
            cpMissileShield3.subject.RegisterObserver(new RemoveMissileObserver());
            cpMissileShield3.subject.RegisterObserver(new RemoveBrickObserver());
            cpMissileShield3.subject.RegisterObserver(new ShipReadyObserver());

            CollisionPair cpBombShield4 = CollisionPairManager.Add(CollisionPairName.BombsvShields, pBombRoot, pShieldRoot4);
            cpBombShield4.subject.RegisterObserver(new RemoveBombObserver());
            cpBombShield4.subject.RegisterObserver(new RemoveBrickObserver());
            CollisionPair cpMissileShield4 = CollisionPairManager.Add(CollisionPairName.MissilevShields, pMissileRoot, pShieldRoot4);
            cpMissileShield4.subject.RegisterObserver(new RemoveMissileObserver());
            cpMissileShield4.subject.RegisterObserver(new RemoveBrickObserver());
            cpMissileShield4.subject.RegisterObserver(new ShipReadyObserver());

            CollisionPair cpBombMissile = CollisionPairManager.Add(CollisionPairName.BombsvMissile, pBombRoot, pMissileRoot);
            cpBombMissile.subject.RegisterObserver(new RemoveBombObserver());
            cpBombMissile.subject.RegisterObserver(new RemoveMissileObserver());
            cpBombMissile.subject.RegisterObserver(new ShipReadyObserver());
            cpBombMissile.subject.RegisterObserver(new SplatObserver());

            CollisionPair cpBombShip = CollisionPairManager.Add(CollisionPairName.BombsvShip, pBombRoot, pShipRoot);
            cpBombShip.subject.RegisterObserver(new RemoveBombObserver());
            cpBombShip.subject.RegisterObserver(new RemoveShipObserver());
            cpBombShip.subject.RegisterObserver(new ShipDeathSoundObserver());
            cpBombShip.subject.RegisterObserver(new ShipEndObserver());

            //TimerManager.ClearTimerManager();
        }
        ~Game()
        {
        }
        public void PopulateAlienGrid(float gridHeight)
        {
            //---------------------------------------------------------------------------------------------------------
            // Create GameObjects via AlienFactory
            //---------------------------------------------------------------------------------------------------------
            PCSTree pAlienTree = new PCSTree();
            AlienFactory alienFactory = new AlienFactory(SpriteBatchName.Aliens, pAlienTree, new Random());
            alienFactory.SetParent(null);
            Alien pGridRoot = alienFactory.Create(AlienType.Grid, GameObjectName.Grid);
            Grid pGrid = (Grid)pGridRoot;
            pGrid.marchSpeed = 0.75f;
            pGrid.bombFrequency = 1.0f;
            for (int i = 0; i < 11; i++)
            {
                int additive = _horizontalSpace * i;
                alienFactory.SetParent(pGridRoot);
                Alien pColumn = alienFactory.Create(AlienType.Column, GameObjectName.Column, i, 0.0f, 0.0f);
                alienFactory.SetParent(pColumn);
                alienFactory.Create(AlienType.Squid, GameObjectName.Squid, i, 51.0f + additive, gridHeight);
                alienFactory.Create(AlienType.Crab, GameObjectName.Crab, i + 11, 51.5f + additive, gridHeight - 75.0f);
                alienFactory.Create(AlienType.Crab, GameObjectName.Crab, i, 51.5f + additive, gridHeight - 150.0f);
                alienFactory.Create(AlienType.Octopus, GameObjectName.Octopus, i + 11, 51.5f + additive, gridHeight - 225.0f);
                alienFactory.Create(AlienType.Octopus, GameObjectName.Octopus, i, 51.5f + additive, gridHeight - 300.0f);
                Column col = (Column)pColumn;
                col.alienCount += 5;
                pGrid.alienCount += 5;
                pGrid.colCount++;
            }
            GameObjectManager.AttachTree(pGridRoot);
        }
        public GameState GetState()
        {
            return this.state;
        }
        public void SetState(GameStateEnum gameState)
        {
            this.state = GameManager.GetState(gameState);
        }
        public void Start()
        {
            this.state.Start(this);
        }
        public void Draw()
        {
            this.state.Draw(this);
        }
        public void Die()
        {
            this.state.Die(this);
        }
        #region Private Methods
        private void InitializeImageManager()
        {
            ImageManager.Add(ImageName.CrabA, TextureName.Sprites, 9.0f, 2.0f, 110.0f, 80.0f);
            ImageManager.Add(ImageName.CrabB, TextureName.Sprites, 137.0f, 2.0f, 110.0f, 80.0f);
            ImageManager.Add(ImageName.SquidA, TextureName.Sprites, 280.0f, 2.0f, 80.0f, 80.0f);
            ImageManager.Add(ImageName.SquidB, TextureName.Sprites, 408.0f, 2.0f, 80.0f, 80.0f);
            ImageManager.Add(ImageName.OctopusA, TextureName.Sprites, 4.0f, 87.0f, 120.0f, 80.0f);
            ImageManager.Add(ImageName.OctopusB, TextureName.Sprites, 132.0f, 87.0f, 120.0f, 80.0f);
            ImageManager.Add(ImageName.UFO, TextureName.Sprites, 2.0f, 440.0f, 123.0f, 57.0f);
            ImageManager.Add(ImageName.Ship, TextureName.Sprites, 155.0f, 442.0f, 73.0f, 52.0f);
            ImageManager.Add(ImageName.Brick, TextureName.Shields, 20.0f, 210.0f, 10.0f, 5.0f);
            ImageManager.Add(ImageName.BrickLeftTop0, TextureName.Shields, 15.0f, 180.0f, 10.0f, 5.0f);
            ImageManager.Add(ImageName.BrickLeftTop1, TextureName.Shields, 15.0f, 185.0f, 10.0f, 5.0f);
            ImageManager.Add(ImageName.BrickLeftBottom, TextureName.Shields, 35.0f, 215.0f, 10.0f, 5.0f);
            ImageManager.Add(ImageName.BrickRightTop0, TextureName.Shields, 75.0f, 180.0f, 10.0f, 5.0f);
            ImageManager.Add(ImageName.BrickRightTop1, TextureName.Shields, 75.0f, 185.0f, 10.0f, 5.0f);
            ImageManager.Add(ImageName.BrickRightBottom, TextureName.Shields, 55.0f, 215.0f, 10.0f, 5.0f);
            ImageManager.Add(ImageName.BombDaggerA, TextureName.Sprites, 141.0f, 536.0f, 12.0f, 32.0f);
            ImageManager.Add(ImageName.BombDaggerB, TextureName.Sprites, 157.0f, 536.0f, 12.0f, 32.0f);
            ImageManager.Add(ImageName.BombZigZagA, TextureName.Sprites, 173.0f, 540.0f, 12.0f, 28.0f);
            ImageManager.Add(ImageName.BombZigZagB, TextureName.Sprites, 189.0f, 540.0f, 12.0f, 28.0f);
            ImageManager.Add(ImageName.BombStraightA, TextureName.Sprites, 205.0f, 536.0f, 12.0f, 32.0f);
            ImageManager.Add(ImageName.BombStraightB, TextureName.Sprites, 221.0f, 536.0f, 12.0f, 32.0f);
            ImageManager.Add(ImageName.Missile, TextureName.Sprites, 237.0f, 536.0f, 4.0f, 32.0f);
            ImageManager.Add(ImageName.ShipExplosion, TextureName.Sprites, 267.0f, 438.0f, 105.0f, 61.0f);
            ImageManager.Add(ImageName.AlienExplosion, TextureName.Sprites, 400.0f, 439.0f, 96.0f, 58.0f);
            ImageManager.Add(ImageName.MissileExplosion, TextureName.Sprites, 314.0f, 544.0f, 24.0f, 32.0f);
        }        
        private ShieldRoot GenerateShield(PCSTree pRootTree, float startX, float startY, int idx)
        {
            ShieldRoot pShieldRoot = new ShieldRoot(GameObjectName.ShieldRoot, SpriteBaseName.Null, 0.0f, 0.0f, idx);
            pRootTree.Insert(pShieldRoot, null);
            GameObjectManager.AttachTree(pShieldRoot);
            ShieldFactory shieldFactory = new ShieldFactory(SpriteBatchName.Shields, SpriteBatchName.Boxes, pRootTree);
            shieldFactory.SetParent(pShieldRoot);
            ShieldCategory pShieldGrid = shieldFactory.Create(ShieldType.Grid, GameObjectName.ShieldGrid);

            int j = 0;
            ShieldCategory pCol;
            shieldFactory.SetParent(pShieldGrid);
            pCol = shieldFactory.Create(ShieldType.Column, GameObjectName.ShieldColumn, j++);
            shieldFactory.SetParent(pCol);

            int i = 0;

            float start_x = startX;
            float start_y = startY;
            float off_x = 0;
            float brickWidth = 15.0f;
            float brickHeight = 10.0f;

            for (int x = 0; x < 8; x++)
            {
                shieldFactory.Create(ShieldType.Brick, GameObjectName.ShieldBrick, start_x, start_y + x * brickHeight, i++);
            }
            shieldFactory.Create(ShieldType.LeftTop1, GameObjectName.ShieldBrick, start_x, start_y + 8 * brickHeight, i++);
            shieldFactory.Create(ShieldType.LeftTop0, GameObjectName.ShieldBrick, start_x, start_y + 9 * brickHeight, i++);
            shieldFactory.SetParent(pShieldGrid);
            pCol = shieldFactory.Create(ShieldType.Column, GameObjectName.Column, j++);
            shieldFactory.SetParent(pCol);

            off_x += brickWidth;
            for (int x = 0; x < 10; x++)
            {
                shieldFactory.Create(ShieldType.Brick, GameObjectName.ShieldBrick, start_x + off_x, start_y + x * brickHeight, i++);
            }
            shieldFactory.SetParent(pShieldGrid);
            pCol = shieldFactory.Create(ShieldType.Column, GameObjectName.Column, j++);
            shieldFactory.SetParent(pCol);

            off_x += brickWidth;
            shieldFactory.Create(ShieldType.LeftBottom, GameObjectName.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
            for (int x = 3; x < 10; x++)
            {
                shieldFactory.Create(ShieldType.Brick, GameObjectName.ShieldBrick, start_x + off_x, start_y + x * brickHeight, i++);
            }
            shieldFactory.SetParent(pShieldGrid);
            pCol = shieldFactory.Create(ShieldType.Column, GameObjectName.Column, j++);
            shieldFactory.SetParent(pCol);

            off_x += brickWidth;
            for (int x = 3; x < 10; x++)
            {
                shieldFactory.Create(ShieldType.Brick, GameObjectName.ShieldBrick, start_x + off_x, start_y + x * brickHeight, i++);
            }
            shieldFactory.SetParent(pShieldGrid);
            pCol = shieldFactory.Create(ShieldType.Column, GameObjectName.Column, j++);
            shieldFactory.SetParent(pCol);

            off_x += brickWidth;
            shieldFactory.Create(ShieldType.RightBottom, GameObjectName.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight, i++);
            for (int x = 3; x < 10; x++)
            {
                shieldFactory.Create(ShieldType.Brick, GameObjectName.ShieldBrick, start_x + off_x, start_y + x * brickHeight, i++);
            }
            shieldFactory.SetParent(pShieldGrid);
            pCol = shieldFactory.Create(ShieldType.Column, GameObjectName.Column, j++);
            shieldFactory.SetParent(pCol);

            off_x += brickWidth;
            for (int x = 0; x < 10; x++)
            {
                shieldFactory.Create(ShieldType.Brick, GameObjectName.ShieldBrick, start_x + off_x, start_y + x * brickHeight, i++);
            }
            shieldFactory.SetParent(pShieldGrid);
            pCol = shieldFactory.Create(ShieldType.Column, GameObjectName.Column, j++);
            shieldFactory.SetParent(pCol);

            off_x += brickWidth;
            for (int x = 0; x < 8; x++)
            {
                shieldFactory.Create(ShieldType.Brick, GameObjectName.ShieldBrick, start_x + off_x, start_y + x * brickHeight, i++);
            }
            shieldFactory.Create(ShieldType.RightTop1, GameObjectName.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
            shieldFactory.Create(ShieldType.RightTop0, GameObjectName.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

            return pShieldRoot;
        }
        #endregion
    }
}
