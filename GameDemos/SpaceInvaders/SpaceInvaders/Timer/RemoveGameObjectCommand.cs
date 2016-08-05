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
            Debug.WriteLine("Removing {0}", this.pGameObject.gameObjectName);
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
