using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveSplatCommand : Command
    {
        private GameObject pGameObject;
        public RemoveSplatCommand(GameObject pSplat)
        {
            this.pGameObject = pSplat;
        }
        public override void execute(float currentTime)
        {
            if (pGameObject != null)
            {
                this.pGameObject.Remove();
                this.pGameObject = null;
            }
        }
    }
}
