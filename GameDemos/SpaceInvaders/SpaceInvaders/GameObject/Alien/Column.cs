using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Column : Alien
    {
        public int alienCount;
        public Column(GameObjectName goName, SpriteBaseName sName, float x, float y, int idx, Random pRandom)
            : base(goName, sName, AlienType.Column, idx)
        {
            this.x = x;
            this.y = y;
            this.pCollisionObject.pCollisionSpriteBox.pLineColor = ColorFactory.Create(ColorName.Green).pAzulColor;
            this.alienCount = 0;
        }
        ~Column()
        {
        }
        public override void Update()
        {
            base.UpdateBoundingBox();
            base.Update();
            //Debug.WriteLine("Column {0} is at height {1}", this.index, this.pCollisionObject.pCollisionRect.minY);
        }
        public override void Accept(Visitor other)
        {
            other.VisitColumn(this);
        }
        public override int getIndex()
        {
            throw new NotImplementedException();
        }
        public override void VisitMissileRoot(MissileRoot pMissileRoot)
        {
            CollisionPair.Collide(pMissileRoot, (GameObject)this.pChild);
        }
    }
}
