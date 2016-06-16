using System;

namespace SpaceInvaders
{
    public class UFORoot : UFO
    {
        public UFORoot(GameObjectName goName, SpriteBaseName sName, float x, float y)
            : base(goName, sName, null, x, y)
        {
            this.x = x;
            this.y = y;
        }
        public override void Update()
        {
            base.UpdateBoundingBox();
            base.Update();
        }
        public override void Accept(Visitor other)
        {
            other.VisitUFORoot(this);
        }
        public override void VisitMissileRoot(MissileRoot pMissileRoot)
        {
            CollisionPair.Collide(pMissileRoot, (GameObject)this.pChild);
        }
        public override void VisitWallRoot(WallRoot pWallRoot)
        {
            CollisionPair.Collide(this, (GameObject)pWallRoot.pChild);
        }
    }
}
