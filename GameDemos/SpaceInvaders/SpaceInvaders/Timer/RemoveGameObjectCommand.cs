using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveGameObjectCommand : Command
    {
        private GameObject pGameObject;
        public RemoveGameObjectCommand(GameObject go)
        {
            this.pGameObject = go;
        }
        public override void execute(float currentTime)
        {
            if (this.pGameObject != null)
            {
                this.pGameObject.Remove();
                this.pGameObject = null;
            }
        }
    }
}
