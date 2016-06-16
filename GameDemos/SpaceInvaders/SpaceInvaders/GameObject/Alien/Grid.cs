using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Grid : Alien
    {
        public int alienCount;
        public int colCount;
        public float marchSpeed;
        public float bombFrequency;
        public float decayFactor;
        public float bombDecayFactor;
        public float movementXDirection;
        public float movementYDirection;
        public float movementXDistance;
        public float movementYDistance;
        public Random pRandom;
        public Grid(GameObjectName goName, SpriteBaseName sbName, float x, float y, int goIdx)
            : base(goName, sbName, AlienType.Grid, goIdx)
        {
            this.x = x;
            this.y = y;
            this.movementXDirection = 20.0f;
            this.movementYDirection = 0.0f;
            this.movementXDistance = 20.0f;
            this.movementYDistance = 5.0f;
            this.pCollisionObject.pCollisionSpriteBox.pLineColor = ColorFactory.Create(ColorName.Yellow).pAzulColor;
            this.alienCount = 0;
            this.colCount = 0;
            this.marchSpeed = 0.75f;
            this.bombFrequency = 1.0f;
            this.decayFactor = 0.0125f;
            this.bombDecayFactor = 0.01f;
            this.pRandom = new Random();
        }
        public void UpdateMarchSpeed(float decayFactor)
        {
            this.marchSpeed -= decayFactor;
        }
        public void UpdateBombFrequency()
        {
            this.bombFrequency -= this.bombDecayFactor;
        }
        public override void Update()
        {
            base.UpdateBoundingBox();
            base.Update();
        }
        public void Move()
        {
            MoveTree(this);
            if (this.movementYDirection < 0)
            {
                this.movementYDirection = 0;
            }
        }
        public Column GetColumn(int n)
        {   // retrives nth active column in grid
            Column col = (Column)this.pChild;
            for (int i = 0; i < n; i++)
            {
                col = (Column)col.pSibling;
            }
            return col;
        }

        private void MoveTree(GameObject pNode)
        {
            PCSNode pChild = null;
            pNode.x += this.movementXDirection;
            pNode.y += this.movementYDirection;
            pNode.Update();
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

        public override void Accept(Visitor other)
        {
            other.VisitGrid(this);
        }
        public override void VisitWallRoot(WallRoot pWallRoot)
        {
            CollisionPair.Collide(this, (GameObject)pWallRoot.pChild);
        }
        public override void VisitMissileRoot(MissileRoot pMissileRoot)
        {
             CollisionPair.Collide(pMissileRoot, (GameObject)this.pChild);
        }
    }
}
