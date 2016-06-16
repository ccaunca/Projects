using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipManager
    {
        private Ship pShip;
        private Missile pMissile;
        private ReadyState pReadyState;
        private MissileFlyingState pMissileFlyingState;
        private EndState pEndState;
        private static ShipManager pInstance;
        private ShipManager()
        {
            this.pShip = null;
            this.pMissile = null;
            this.pReadyState = new ReadyState();
            this.pMissileFlyingState = new MissileFlyingState();
            this.pEndState = new EndState();
        }
        ~ShipManager()
        {
            this.pShip = null;
            this.pMissile = null;
            this.pReadyState = null;
            this.pMissileFlyingState = null;
            this.pEndState = null;
        }
        public static void Create(bool isCollisionBoxActive)
        {
            if (pInstance == null)
            {
                pInstance = new ShipManager();
            }
            pInstance.pShip = ActivateShip(isCollisionBoxActive);
            pInstance.pShip.SetState(ShipStateEnum.Ready);
        }
        public static Ship ActivateShip(bool isCollisionBoxActive)
        {
            ShipManager shipMan = ShipManager.GetInstance();

            PCSTree pcsTree = GameObjectManager.GetRootTree();
            Debug.Assert(pcsTree != null);

            Ship pShip = new Ship(GameObjectName.Ship, SpriteBaseName.Ship, 150.0f, 100.0f);
            shipMan.pShip = pShip;

            SpriteBatch sbAliens = SpriteBatchManager.Find(SpriteBatchName.Aliens);
            SpriteBatch sbBoxes = SpriteBatchManager.Find(SpriteBatchName.Boxes);
            if (isCollisionBoxActive)
            {
                pShip.ActivateCollisionSprite(sbBoxes);
            }
            pShip.ActivateGameSprite(sbAliens);
            //sbAliens.Attach(pShip.pProxySprite);

            GameObject pShipRoot = GameObjectManager.Find(GameObjectName.ShipRoot);
            Debug.Assert(pShipRoot != null);
            pShip.Update();
            pcsTree.Insert(shipMan.pShip, pShipRoot);
            return shipMan.pShip;
        }
        public static Missile ActivateMissile()
        {
            ShipManager shipMan = ShipManager.GetInstance();

            PCSTree pcsTree = GameObjectManager.GetRootTree();
            Debug.Assert(pcsTree != null);

            Missile pMissile = new Missile(GameObjectName.Missile, SpriteBaseName.Missile, 400.0f, 100.0f, 0);
            pMissile.pProxySprite.pSprite.SetColor(ColorFactory.Create(ColorName.Green).pAzulColor);
            shipMan.pMissile = pMissile;

            SpriteBatch sbAliens = SpriteBatchManager.Find(SpriteBatchName.Aliens);
            SpriteBatch sbBoxes = SpriteBatchManager.Find(SpriteBatchName.Boxes);

            pMissile.ActivateCollisionSprite(sbBoxes);
            pMissile.ActivateGameSprite(sbAliens);

            GameObject pMissileRoot = GameObjectManager.Find(GameObjectName.MissileRoot);
            Debug.Assert(pMissileRoot != null);

            pcsTree.Insert(shipMan.pMissile, pMissileRoot);

            return shipMan.pMissile;
        }
        public static Missile GetMissile()
        {
            ShipManager shipMan = ShipManager.GetInstance();
            return shipMan.pMissile;
        }
        public static Ship GetShip()
        {
            ShipManager shipMan = ShipManager.GetInstance();
            Debug.Assert(shipMan.pShip != null);
            return shipMan.pShip;
        }
        public static void MoveShip(float x, float y)
        {
            Ship pShip = GetShip();
            pShip.x = x;
            pShip.y = y;
            pShip.Update();
        }
        public static ShipState GetState(ShipStateEnum shipState)
        {
            ShipManager shipMan = ShipManager.GetInstance();
            ShipState pShipState = null;

            switch (shipState)
            {
                case ShipStateEnum.Ready :
                    pShipState = shipMan.pReadyState;
                    break;
                case ShipStateEnum.MissileFlying :
                    pShipState = shipMan.pMissileFlyingState;
                    break;
                case ShipStateEnum.End :
                    pShipState = shipMan.pEndState;
                    break;
                default :
                    Debug.Assert(false);
                    break;
            }
            return pShipState;
        }
        public static ShipManager GetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }
    }
}
