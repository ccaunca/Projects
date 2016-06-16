using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GameObjectManager : Manager
    {
        private PCSTree pRoot;
        private static GameObjectManager pInstance;
        private GameObjectManager(int reserveSize = 5, int reserveIncrement = 2)
            : base(reserveSize, reserveIncrement)
        {
            this.pRoot = new PCSTree();
            Debug.Assert(this.pRoot != null);
        }
        ~GameObjectManager()
        {
            GameObjectManager.pInstance = null;
        }
        public static void Destroy()
        {
            GameObjectManager goMan = GameObjectManager.GetInstance();
            goMan.BaseDestroy();
        }
        public static GameObjectNode AttachTree(GameObject pGameObject)
        {
            Debug.Assert(pGameObject != null);
            GameObjectManager goMan = GameObjectManager.GetInstance();

            GameObjectNode goNode = (GameObjectNode)goMan.BaseAdd();
            Debug.Assert(goNode != null);

            goNode.Set(pGameObject);
            return goNode;
        }
        public static void Insert(GameObject pGameObject, GameObject pParent)
        {
            GameObjectManager goMan = GameObjectManager.GetInstance();
            Debug.Assert(pGameObject != null);

            if (pParent == null)
            {
                GameObjectManager.AttachTree(pGameObject);
            }
            else
            {
                Debug.Assert(pParent != null);

                goMan.pRoot.SetRoot(pParent);
                goMan.pRoot.Insert(pGameObject, pParent);
            }
        }
        public static void Create(int reserveSize = 3, int reserveIncrement = 1)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(reserveIncrement > 0);

            if(pInstance == null)
            {
                pInstance = new GameObjectManager(reserveSize, reserveIncrement);
            }
        }
        public static void DeactivateCollisionBoxes()
        {
            // Remove Bombs
            GameObject bombRoot = GameObjectManager.Find(GameObjectName.BombRoot);
            PCSTreeReverseIterator iterBomb = new PCSTreeReverseIterator(bombRoot);
            GameObject pGoBomb = (GameObject)iterBomb.First();
            while (!iterBomb.IsDone())
            {
                pGoBomb.DeactivateCollisionSprite();
                pGoBomb = (GameObject)iterBomb.Next();
            }
            GameObject missileRoot = GameObjectManager.Find(GameObjectName.MissileRoot);
            PCSTreeReverseIterator iterMissile = new PCSTreeReverseIterator(missileRoot);
            GameObject pGOMissile = (GameObject)iterMissile.First();
            while (!iterMissile.IsDone())
            {
                pGOMissile.DeactivateCollisionSprite();
                pGOMissile = (GameObject)iterMissile.Next();
            }

            // Remove Shields
            for (int i = 1; i < 5; ++i)
            {
                GameObject shieldRoot = GameObjectManager.Find(GameObjectName.ShieldRoot, i);
                PCSTreeReverseIterator iter = new PCSTreeReverseIterator(shieldRoot);
                GameObject pGO = (GameObject)iter.First();
                while (!iter.IsDone())
                {
                    pGO.DeactivateCollisionSprite();
                    pGO = (GameObject)iter.Next();
                }
            }

            // Remove Alien Grid
            GameObject grid = GameObjectManager.Find(GameObjectName.Grid);
            PCSTreeReverseIterator iterGrid = new PCSTreeReverseIterator(grid);
            GameObject pGameObj = (GameObject)iterGrid.First();
            while (!iterGrid.IsDone())
            {
                pGameObj.DeactivateCollisionSprite();
                pGameObj = (GameObject)iterGrid.Next();
            }
        }
        public static void Dump()
        {
            GameObjectManager goMan = GameObjectManager.GetInstance();
            goMan.BaseDump();
        }
        public static GameObject Find(GameObjectName goName, int index=0)
        {
            GameObjectManager goMan = GameObjectManager.GetInstance();
            GameObjectNode pRoot = (GameObjectNode)goMan.pActive;
            GameObject pGameObj = null;

            bool found = false;
            while (pRoot != null && found == false)
            {
                PCSTreeForwardIterator iter = new PCSTreeForwardIterator(pRoot.pGameObject);
                pGameObj = (GameObject)iter.First();

                while (!iter.IsDone())
                {
                    if ((pGameObj.gameObjectName == goName) && (pGameObj.index == index))
                    {
                        found = true;
                        break;
                    }
                    pGameObj = (GameObject)iter.Next();
                }
                pRoot = (GameObjectNode)pRoot.pDNext;
            }
            return pGameObj;
        }
        public static void Remove(GameObjectNode goNode)
        {
            Debug.Assert(goNode != null);
            GameObjectManager goMan = GameObjectManager.GetInstance();
            goMan.BaseRemove(goNode);
        }
        public static void Remove(GameObject pNode)
        {
            Debug.Assert(pNode != null);
            GameObjectManager pMan = GameObjectManager.GetInstance();
            GameObject pSafetyNode = pNode;
            GameObject pTmp = pNode;
            GameObject pRoot = null;
            while (pTmp != null)
            {
                pRoot = pTmp;
                pTmp = (GameObject)pTmp.pParent;
            }
            GameObjectNode pTree = (GameObjectNode)pMan.pActive;
            while (pTree != null)
            {
                if (pTree.pGameObject == pRoot)
                {
                    break;
                }
                pTree = (GameObjectNode)pTree.pDNext;
            }
            Debug.Assert(pTree != null);
            Debug.Assert(pTree.pGameObject != null);
            pMan.pRoot.SetRoot(pTree.pGameObject);
            pMan.pRoot.Remove(pNode);
        }
        public static void Update()
        {
            GameObjectManager goMan = GameObjectManager.GetInstance();
            GameObjectNode goNode = (GameObjectNode)goMan.pActive;
            while (goNode != null)
            {   
                PCSTreeReverseIterator iter = new PCSTreeReverseIterator(goNode.pGameObject);
                GameObject go = (GameObject)iter.First();
                while (!iter.IsDone())
                {
                    go.Update();
                    go = (GameObject)iter.Next();
                }
                goNode = (GameObjectNode)goNode.pDNext;
            }
        }
        public static PCSTree GetRootTree()
        {
            GameObjectManager goMan = GameObjectManager.GetInstance();
            return goMan.pRoot;
        }
        private static GameObjectManager GetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }
        protected override bool Compare(DLink dLink1, DLink dLink2)
        {
            bool result = false;
            GameObjectNode goNode1 = (GameObjectNode)dLink1;
            GameObjectNode goNode2 = (GameObjectNode)dLink2;
            if (goNode1.pGameObject.gameObjectName == goNode2.pGameObject.gameObjectName)
            {
                result = true;
            }
            return result;
        }

        protected override DLink CreateNode()
        {
            return new GameObjectNode();
        }

        protected override void DumpNode(DLink node)
        {
            Debug.Assert(node != null);
            GameObjectNode go = (GameObjectNode)node;
            if (go.pGameObject == null)
            {
                Debug.WriteLine("GameObject:null");
                Debug.WriteLine("(null,null)");
                Debug.WriteLine("ProxySprite:null");
            }
            else if (go.pGameObject.pProxySprite == null)
            {
                Debug.WriteLine(String.Format("GameObject:{0}({1})", go.pGameObject.gameObjectName, go.GetHashCode()));
                Debug.WriteLine(String.Format("({0},{1})", go.pGameObject.x, go.pGameObject.y));
                Debug.WriteLine("ProxySprite:null");
                
            }
            else
            {
                Debug.WriteLine(String.Format("GameObject:{0}{1}({2})", go.pGameObject.gameObjectName, go.pGameObject.index, go.GetHashCode()));
                Debug.WriteLine(String.Format("({0},{1})", go.pGameObject.x, go.pGameObject.y));
                Debug.WriteLine(String.Format("ProxySprite:{0}({1})", go.pGameObject.pProxySprite.name, go.pGameObject.pProxySprite.GetHashCode()));
            }
        }
    }
}
