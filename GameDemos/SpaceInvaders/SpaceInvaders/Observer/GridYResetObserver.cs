using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GridYResetObserver : CollisionObserver
    {
        public GridYResetObserver()
        {

        }
        public override void Update()
        {
            Grid pGrid = (Grid)this.pSubject.goA;
            Wall pWall = (Wall)this.pSubject.goB;
            if (pWall.type == WallType.Right)
            {
                pGrid.movementYDirection = 0.0f;
            }
            else if (pWall.type == WallType.Left)
            {
                pGrid.movementYDirection = 0.0f;
            }
            else
            {
                Debug.Assert(false);
            }
        }
    }
}
