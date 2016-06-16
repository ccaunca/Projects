using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Missile : MissileCategory
    {
        public float speed;
        public bool enabled;
        public Missile(GameObjectName goName, SpriteBaseName sName, float x, float y, int idx)
            : base(goName, sName, MissileType.Missile, idx)
        {
            this.x = x;
            this.y = y;
            this.enabled = false;
            this.speed = 10.0f;
        }
        public override int getIndex()
        {
            throw new NotImplementedException();
        }
        public void SetActive(bool state)
        {
            this.enabled = state;
        }
        public void SetPosition(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public override void Update()
        {
            base.Update();
            this.y += speed;
        }
        public override void Remove()
        {
            this.pCollisionObject.pCollisionRect.Set(0, 0, 0, 0);
            base.Update();

            GameObject pParent = (GameObject)this.pParent;
            if (pParent != null)
            {
                pParent.Update();
            }

            base.Remove();
        }
        public override void VisitBomb(Bomb pBomb)
        {
            CollisionPair collisionPair = CollisionPairManager.GetActiveCollisionPair();
            collisionPair.SetCollision(pBomb, this);
            collisionPair.NotifyObservers();
        }
        public override void Accept(Visitor other)
        {
            other.VisitMissile(this);
        }
    }
}
