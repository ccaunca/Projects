using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldFactory
    {
        private SpriteBatch pSpriteBatch;
        private SpriteBatch pCollisionSpriteBatch;
        private GameObject pParent;
        private PCSTree pTree;
        public ShieldFactory(SpriteBatchName sbName, SpriteBatchName collisionSBName, PCSTree pTree)
        {
            this.pSpriteBatch = SpriteBatchManager.Find(sbName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pCollisionSpriteBatch = SpriteBatchManager.Find(collisionSBName);
            Debug.Assert(this.pCollisionSpriteBatch != null);

            Debug.Assert(pTree != null);
            this.pTree = pTree;
        }
        ~ShieldFactory()
        {
            this.pSpriteBatch = null;
            this.pCollisionSpriteBatch = null;
            this.pParent = null;
            this.pTree = null;
        }
        public void SetParent(GameObject go)
        {
            this.pParent = go;
        }
        public ShieldCategory Create(ShieldType type, GameObjectName goName, float x = 0.0f, float y = 0.0f, int idx = 0)
        {
            ShieldCategory pShield = null;
            switch(type)
            {
                case ShieldType.Brick :
                    pShield = new ShieldBrick(goName, SpriteBaseName.Brick, x, y, idx);
                    break;
                case ShieldType.LeftTop0 :
                    pShield = new ShieldBrick(goName, SpriteBaseName.BrickLeftTop0, x, y, idx);
                    break;
                case ShieldType.LeftTop1 :
                    pShield = new ShieldBrick(goName, SpriteBaseName.BrickLeftTop1, x, y, idx);
                    break;
                case ShieldType.LeftBottom :
                    pShield = new ShieldBrick(goName, SpriteBaseName.BrickLeftBottom, x, y, idx);
                    break;
                case ShieldType.RightTop0 :
                    pShield = new ShieldBrick(goName, SpriteBaseName.BrickRightTop0, x, y, idx);
                    break;
                case ShieldType.RightTop1 :
                    pShield = new ShieldBrick(goName, SpriteBaseName.BrickRightTop1, x, y, idx);
                    break;
                case ShieldType.RightBottom :
                    pShield = new ShieldBrick(goName, SpriteBaseName.BrickRightBottom, x, y, idx);
                    break;
                case ShieldType.Root :
                    pShield = new ShieldRoot(goName, SpriteBaseName.Null, x, y, idx);
                    pShield.pCollisionObject.pCollisionSpriteBox.pLineColor = ColorFactory.Create(ColorName.Blue).pAzulColor;
                    Debug.Assert(false);
                    break;
                case ShieldType.Grid :
                    pShield = new ShieldGrid(goName, SpriteBaseName.Null, x, y, idx);
                    pShield.pCollisionObject.pCollisionSpriteBox.pLineColor = ColorFactory.Create(ColorName.Blue).pAzulColor;
                    break;
                case ShieldType.Column :
                    pShield = new ShieldColumn(goName, SpriteBaseName.Null, x, y, idx);
                    pShield.pCollisionObject.pCollisionSpriteBox.pLineColor = ColorFactory.Create(ColorName.Red).pAzulColor;
                    break;
                default :
                    Debug.Assert(false);
                    break;
            }
            this.pTree.Insert(pShield, this.pParent);
            pShield.ActivateGameSprite(this.pSpriteBatch);
            if (GameManager.GetCollisionBoxes())
            {
                pShield.ActivateCollisionSprite(this.pCollisionSpriteBatch);
            }
            return pShield;
        }
    }
}
