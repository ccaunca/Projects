using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GameObjectNode : DLink
    {
        public GameObject pGameObject;
        public PCSTree pTree;
        public GameObjectNode()
        {
            this.pGameObject = null;
        }
        public GameObjectNode(GameObjectName goName, int index)
        {
            this.pGameObject = new NullGameObject();
            this.pGameObject.gameObjectName = goName;
            this.pGameObject.index = index;
        }
        public void Set(GameObject go)
        {
            Debug.Assert(go != null);
            this.pGameObject = go;
        }
    }
}
