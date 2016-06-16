using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GridXObserver : CollisionObserver
    {
        public GridXObserver()
        {

        }
        public override void Update()
        {
            Grid pGrid = (Grid)this.pSubject.goA;
            Wall pWall = (Wall)this.pSubject.goB;
            if (pWall.type == WallType.Right)
            {
                pGrid.movementXDirection = -pGrid.movementXDistance;
            }
            else if (pWall.type == WallType.Left)
            {
                pGrid.movementXDirection = pGrid.movementXDistance;
            }
            else
            {
                Debug.Assert(false);
            }
        }
    }
}
