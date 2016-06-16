using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFO : UFOCategory
    {
        public float speed;
        public UFOStrategy pStrategy;
        
        public UFO(GameObjectName goName, SpriteBaseName sName, UFOStrategy strat, float x, float y)
            : base(goName, sName, UFOType.UFO, 0)
        {
            this.x = x;
            this.y = y;
            this.speed = 4.0f;
            this.pStrategy = strat;
            this.pProxySprite.pSprite.pColor = ColorFactory.Create(ColorName.Red).pAzulColor;
        }
        ~UFO()
        {

        }
        public override void Update()
        {
            if (this.pStrategy != null)
            {
                this.pStrategy.Move(this);
            }
            base.Update();
        }
        public override void Accept(Visitor other)
        {
            other.VisitUFO(this);
        }
        public override void VisitMissileRoot(MissileRoot pMissileRoot)
        {
            CollisionPair.Collide((GameObject)pMissileRoot.pChild, this);
        }
        public override void VisitMissile(Missile pMissile)
        {
            CollisionPair collisionPair = CollisionPairManager.GetActiveCollisionPair();
            collisionPair.SetCollision(pMissile, this);
            collisionPair.NotifyObservers();
        }
    }
}
