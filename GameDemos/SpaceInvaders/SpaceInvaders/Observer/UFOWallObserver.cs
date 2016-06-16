using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFOWallObserver : CollisionObserver
    {
        public override void Update()
        {
            UFORoot pUFORoot = (UFORoot)this.pSubject.goA;
            UFO pUFO = (UFO)pUFORoot.pChild;
            if (pUFO != null)
            {
                Wall pWall = (Wall)this.pSubject.goB;

                if ((pUFO.pStrategy is UFOMoveLeft && pWall.gameObjectName == GameObjectName.WallLeft) ||
                    (pUFO.pStrategy is UFOMoveRight && pWall.gameObjectName == GameObjectName.WallRight))
                {
                    if (UFOManager.IsUFOActive())
                    {
                        Debug.WriteLine("RemoveUFO TimerEvent added for {0}", pUFO.GetHashCode());
                        TimerManager.Add(TimerEventName.RemoveUFO, TimerManager.GetCurrentTime() + 1.0f, TimerManager.GetCurrentTime() + 1.0f, new RemoveUFOCommand());
                        TimerManager.Add(TimerEventName.StopUFOSound, TimerManager.GetCurrentTime() + 0.5f, TimerManager.GetCurrentTime() + 0.5f, new StopUFOSoundCommand());
                        
                    }
                    //pUFO.Remove();
                    //pUFORoot.Remove();
                }
            }
        }
    }
}
