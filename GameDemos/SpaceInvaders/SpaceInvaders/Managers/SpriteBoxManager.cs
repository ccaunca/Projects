using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteBoxManager : Manager
    {
        private static SpriteBoxManager pInstance;
        private SpriteBoxManager(int reserveSize = 3, int reserveIncrement = 1)
            : base(reserveSize, reserveIncrement)
        {

        }
        private static SpriteBoxManager GetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }
        #region Public Methods
        public static SpriteBox Add(SpriteBaseName name, Azul.Rect azulRect)
        {
            SpriteBoxManager spriteBoxMan = SpriteBoxManager.GetInstance();
            SpriteBox spriteBox = (SpriteBox)spriteBoxMan.BaseAdd();
            spriteBox.Set(name, azulRect.x, azulRect.y, azulRect.width, azulRect.height);
            return spriteBox;
        }
        public static void Create(int reserveSize = 3, int reserveIncrement = 1)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(reserveIncrement > 0);
            if (pInstance == null)
            {
                pInstance = new SpriteBoxManager(reserveSize, reserveIncrement);
            }
        }
        public static SpriteBox Find(SpriteBaseName name)
        {
            SpriteBoxManager sbMan = SpriteBoxManager.GetInstance();
            SpriteBox sb = (SpriteBox)sbMan.BaseFind(new SpriteBox { name = name });
            return sb;
        }
        #endregion
        protected override bool Compare(DLink dLink1, DLink dLink2)
        {
            bool result = false;
            Debug.Assert(dLink1 != null);
            Debug.Assert(dLink2 != null);
            SpriteBox pSpriteBox1 = (SpriteBox)dLink1;
            SpriteBox pSpriteBox2 = (SpriteBox)dLink2;
            if (pSpriteBox1.name == pSpriteBox2.name)
            {
                result = true;
            }
            return result;
        }

        protected override DLink CreateNode()
        {
            return new SpriteBox();
        }

        protected override void DumpNode(DLink node)
        {
            Debug.Assert(node != null);
            SpriteBox pSpriteBox = (SpriteBox)node;
            Debug.WriteLine(String.Format("\t\tSpriteBox:({0})", pSpriteBox.GetHashCode()));
            Debug.WriteLine(String.Format("\t\tLineColor:({0},{1},{2})", pSpriteBox.pLineColor.red, pSpriteBox.pLineColor.green, pSpriteBox.pLineColor.blue));
            Debug.WriteLine(String.Format("\t\tstatus:{0}", pSpriteBox.status));
        }
    }
}
