using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum GameObjectName
    {
        Squid,
        Crab,
        Octopus,
        Shield, ShieldRoot, ShieldGrid, ShieldColumn, ShieldBrick,
        Missile, MissileRoot,
        Bomb, BombRoot,
        UFO,
        Grid,
        Column,
        Ship, ShipRoot,
        Splat, Explosion,
        WallRoot, WallLeft, WallRight, WallTop, WallBottom,
        Uninitialized,
        UFORoot,
        UFOBomb
    }
    public abstract class GameObject : Visitor
    {
        public float x;
        public float y;
        public GameObjectName gameObjectName;
        public int index;
        public bool markedForDeath;
        public ProxySprite pProxySprite;
        public CollisionObject pCollisionObject;
        protected GameObject(GameObjectName goName, SpriteBaseName spriteName, int idx)
        {
            this.gameObjectName = goName;
            this.index = idx;
            this.markedForDeath = false;
            this.x = 0.0f;
            this.y = 0.0f;
            this.pProxySprite = ProxySpriteManager.Add(spriteName);
            Debug.Assert(this.pProxySprite != null);
            this.pCollisionObject = new CollisionObject(this.pProxySprite);
            Debug.Assert(this.pCollisionObject != null);
        }
        ~GameObject()
        {
            this.gameObjectName = GameObjectName.Uninitialized;
            this.pProxySprite = null;
        }
        protected void UpdateBoundingBox()
        {
            PCSNode node = (PCSNode)this;
            node = node.pChild;

            GameObject go = (GameObject)node;

            CollisionRect collisionTotal = this.pCollisionObject.pCollisionRect;
            collisionTotal.Set(go.pCollisionObject.pCollisionRect);

            while (node != null)
            {
                go = (GameObject)node;
                collisionTotal.Union(go.pCollisionObject.pCollisionRect);
                node = node.pSibling;
            }
            this.x = this.pCollisionObject.pCollisionRect.x;
            this.y = this.pCollisionObject.pCollisionRect.y;
        }
        public void ActivateGameSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            pSpriteBatch.Attach(this.pProxySprite);
        }
        public void ActivateCollisionSprite(SpriteBatch pSpriteBatch)
        {
            if (GameManager.GetCollisionBoxes())
            {
                Debug.Assert(pSpriteBatch != null);
                pSpriteBatch.Attach(this.pCollisionObject.pCollisionSpriteBox);
            }
        }
        public void DeactivateCollisionSprite()
        {
            SpriteBatchNode sbNode = this.pCollisionObject.pCollisionSpriteBox.GetSpriteBatchNode();
            Debug.Assert(sbNode != null);
            SpriteBatchManager.Remove(sbNode);
        }
        public virtual void Remove()
        {
            Debug.Assert(this.pProxySprite != null);
            SpriteBatchNode sbNode = this.pProxySprite.GetSpriteBatchNode();
            Debug.Assert(sbNode != null);
            SpriteBatchManager.Remove(sbNode);
            if (GameManager.GetCollisionBoxes())
            {
                if (this.pCollisionObject.pCollisionSpriteBox.pSpriteBatchNode != null)
                {
                    sbNode = this.pCollisionObject.pCollisionSpriteBox.GetSpriteBatchNode();
                    Debug.Assert(sbNode != null);
                    SpriteBatchManager.Remove(sbNode);
                }
            }
            GameObjectManager.Remove(this);
        }
        public virtual void RemoveSprite()
        {
            Debug.Assert(this.pProxySprite != null);
            SpriteBatchNode sbNode = this.pProxySprite.GetSpriteBatchNode();
            Debug.Assert(sbNode != null);
            SpriteBatchManager.Remove(sbNode);
        }
        public override Enum getName()
        {
            return this.gameObjectName;
        }
        public override int getIndex()
        {
            return this.index;
        }
        public virtual void Update()
        {
            Debug.Assert(this.pProxySprite != null);
            this.pProxySprite.x = this.x;
            this.pProxySprite.y = this.y;

            Debug.Assert(this.pCollisionObject != null);
            this.pCollisionObject.UpdatePos(x, y);
        }
    }
}
