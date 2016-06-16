using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Missile : Alien
    {
        public float speed;
        public Missile(float x, float y)
            : base(GameObjectName.Missile, SpriteBaseName.Missile, AlienType.Missile, 0)
        {
            //this.pCollisionObject.pCollisionSpriteBox.pLineColor = ColorFactory.Create(ColorName.Violet).pAzulColor;
        }
        public override void Accept(Visitor other)
        {
            throw new NotImplementedException();
        }

        public override int getIndex()
        {
            throw new NotImplementedException();
        }
    }
}
