using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteBatchManager : Manager
    {
        private static SpriteBatchManager pInstance;
        private SpriteBatchManager(int reserveSize = 5, int reserveIncrement = 1)
            : base(reserveSize, reserveIncrement)
        {

        }
        private static SpriteBatchManager GetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }
        #region Public Functions
        public static SpriteBatch Add(SpriteBatchName spriteBatchName)
        {
            SpriteBatchManager spBatchMan = SpriteBatchManager.GetInstance();
            SpriteBatch spBatch = (SpriteBatch)spBatchMan.BaseAdd();
            spBatch.name = spriteBatchName;
            return spBatch;
        }
        public static void Create(int reserveSize = 3, int reserveIncrement = 1)
        {
            if (pInstance == null)
            {
                pInstance = new SpriteBatchManager(reserveSize, reserveIncrement);
            }
        }
        public static void Draw()
        {
            SpriteBatchManager sbMan = SpriteBatchManager.GetInstance();
            SpriteBatch pSpriteBatchActive = (SpriteBatch)sbMan.pActive;
            SpriteBatchNode sbNode = null;

            while (pSpriteBatchActive != null)
            {
                sbNode = (SpriteBatchNode)pSpriteBatchActive.pActive;
                while (sbNode != null)
                {
                    if (sbNode.pSpriteBase != null)
                    {
                        sbNode.pSpriteBase.Render();
                    }
                    sbNode = (SpriteBatchNode)sbNode.pCNext;
                }
                pSpriteBatchActive = (SpriteBatch)pSpriteBatchActive.pDNext;
            }
        }
        public static void Dump()
        {
            SpriteBatchManager spBatchMan = SpriteBatchManager.GetInstance();
            SpriteBatch sb = (SpriteBatch)SpriteBatchManager.Find(SpriteBatchName.Shields);
            
                Debug.WriteLine("SpriteBatch name {0}", sb.name);
                Debug.WriteLine("numActive: {0}", sb.mNumActive);
                Debug.WriteLine("numReserve: {0}", sb.mNumReserve);
                Debug.WriteLine("numTotal: {0}", sb.mNumTotalNodes);
        }
        public static SpriteBatch Find(SpriteBatchName spriteBatchName)
        {
            SpriteBatchManager spMan = SpriteBatchManager.GetInstance();
            SpriteBatch spBatch = (SpriteBatch)spMan.BaseFind(new SpriteBatch { name = spriteBatchName });
            return spBatch;
        }
        public static void Remove(SpriteBatch spriteBatch)
        {
            Debug.Assert(spriteBatch != null);
            SpriteBatchManager sbMan = SpriteBatchManager.GetInstance();
            sbMan.BaseRemove(spriteBatch);
        }
        public static void Remove(SpriteBatchNode spriteBatchNode)
        {
            Debug.Assert(spriteBatchNode != null);
            SpriteBatch sb = spriteBatchNode.GetSpriteBatch();

            Debug.Assert(sb != null);
            sb.Remove(spriteBatchNode);
        }
        #endregion
        #region Base Class Implementation
        protected override bool Compare(DLink dLink1, DLink dLink2)
        {
            bool result = false;
            SpriteBatch sp1 = (SpriteBatch)dLink1;
            SpriteBatch sp2 = (SpriteBatch)dLink2;
            if (sp1.name == sp2.name)
            {
                result = true;
            }
            return result;
        }

        protected override DLink CreateNode()
        {
            return new SpriteBatch();
        }

        protected override void DumpNode(DLink node)
        {
            SpriteBatch sp = (SpriteBatch)node;
            Debug.WriteLine(String.Format("\t\tSpriteBatch:{0}({1})", sp.name, sp.GetHashCode()));
            Debug.WriteLine(String.Format("\t\tstatus:{0}", sp.status));
        }
        #endregion
    }
}
