using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GridYObserver : CollisionObserver
    {
        public override void Update() 
        {
            Grid pGrid = (Grid)this.pSubject.goA;
            Wall pWall = (Wall)this.pSubject.goB;
            if (pWall.type == WallType.Right)
            {
                pGrid.movementYDirection = -pGrid.movementYDistance;
            }
            else if (pWall.type == WallType.Left)
            {
                pGrid.movementYDirection = -pGrid.movementYDistance;
            }
            else
            {
                Debug.Assert(false);
            }
        }
    }
}
