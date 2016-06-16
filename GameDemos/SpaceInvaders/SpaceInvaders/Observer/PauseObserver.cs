using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class PauseObserver : InputObserver
    {
        public override void Update()
        {
            if (GameManager.GetState() is GamePauseState)
            {
                //GameManager.Unpause();
            }
            else
            {
                //GameManager.Pause();
            }
        }
    }
}
