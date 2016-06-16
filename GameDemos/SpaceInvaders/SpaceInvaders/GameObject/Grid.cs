using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Grid : Alien
    {
        private float movementXDirection;
        private float movementYDirection;
        public Grid()
            : base(GameObjectName.Grid, SpriteBaseName.Null, AlienType.Hierarchy, 0.0f, 0.0f)
        {
            this.movementXDirection = 5.0f;
            this.movementYDirection = 0.0f;
        }
        public void Move()
        {
            if (this.movementXDirection == 0.0f && (this.x == 180.0f || this.x == -20.0f))
            {
                this.movementYDirection = -20.0f;
                this.y += this.movementYDirection;
            }
            else
            {
                this.x += this.movementXDirection;
            }
            
            MoveTree(this);
            if (this.x == 180.0f || this.x == -20.0f)
            {
                this.movementYDirection = -20.0f;
            }
            else
            {
                this.movementYDirection = 0.0f;
            }
            if (this.x > 180.0f || this.x < -20.0f)
            {
                this.movementXDirection *= -1.0f;
            }
        }

        private void MoveTree(GameObject pNode)
        {
            PCSNode pChild = null;
            pNode.pProxySprite.x += this.movementXDirection;
            pNode.pProxySprite.y += this.movementYDirection;
            if (pNode.pChild == null)
            {
                // base case
            }
            else
            {
                pChild = pNode.pChild;
                while (pChild != null)
                {
                    MoveTree((GameObject)pChild);
                    pChild = pChild.pSibling;
                }
            }
        }
        public override int getIndex()
        {
            throw new NotImplementedException();
        }
    }
}
