using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveAlienObserver : CollisionObserver
    {
        public GameObject pAlien;
        public RemoveAlienObserver()
        {
            this.pAlien = null;
        }
        public RemoveAlienObserver(GameObject alien)
        {
            this.pAlien = alien;
        }
        public override void Update()
        {
            //Debug.WriteLine("RemoveAlienObserver: {0} {1}", this.pSubject.goA, this.pSubject.goB);
            GameObject pAlien = this.pSubject.goB;
            if (pAlien.markedForDeath == false)
            {
                pAlien.markedForDeath = true;
                RemoveAlienObserver pObserver = new RemoveAlienObserver(pAlien);
                DelayedGameObjectManager.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            Column pColumn = (Column)this.pAlien.pParent;
            Grid pGrid = (Grid)pColumn.pParent;
            this.pAlien.Remove();
            pColumn.alienCount--;
            pGrid.alienCount--;
            TimerManager.UpdateTimerManager(pGrid);
            if (pColumn.alienCount == 0)
            {
                ((GameObject)pColumn).Remove();
                pGrid.colCount--;
            }
            if (pGrid.alienCount == 0)
            {   // Round over!  Start next round
                ((GameObject)pGrid).Remove();
                ScoreManager.OneUp();
                //ShipManager.MoveShip(150.0f, 100.0f);
                TimerManager.ClearTimerManager();
                UFOManager.Deactivate();
                //TimerManager.Dump();
                Game pGame = GameManager.GetGame();
                pGame.roundNum++;
                GameManager.ActivateGame(false);
                UFOManager.Activate();
                TimerManager.InitializeTimerManager();
            }
        }
    }
}