using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MoveGridEvent : Command
    {
        public override void execute(float currentTime)
        {
            Grid pGrid = (Grid)GameObjectManager.Find(GameObjectName.Grid);
            pGrid.Move();
        }
    }
}
