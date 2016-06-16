using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteManager : Manager
    {
        private static SpriteManager pInstance;
        private SpriteManager(int reserveSize = 5, int reserveIncrement = 2)
            : base(reserveSize, reserveIncrement)
        {

        }
        #region Public Methods
        public static Sprite Add(SpriteBaseName name, ImageName imgName, float x, float y, float sx, float sy)
        {
            SpriteManager spriteMan = SpriteManager.GetInstance();
            Sprite sprite = (Sprite)spriteMan.BaseAdd();
            sprite.Set(name, imgName, x, y, sx, sy);
            return sprite;
        }
        public static void Create(int reserveSize = 3, int reserveIncrement = 1)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(reserveIncrement > 0);
            if (pInstance == null)
            {
                pInstance = new SpriteManager(reserveSize, reserveIncrement);

                Sprite sprite = SpriteManager.Add(SpriteBaseName.Null, ImageName.Null, 0, 0, 1, 1);
                Debug.Assert(sprite != null);
            }
        }
        public static void Dump()
        {
            SpriteManager spriteMan = SpriteManager.GetInstance();
            spriteMan.BaseDump();
        }
        public static Sprite Find(SpriteBaseName spriteName)
        {
            SpriteManager spriteMan = SpriteManager.GetInstance();
            return (Sprite)spriteMan.BaseFind(new Sprite { name = spriteName });
        }
        public static void Remove(DLink node)
        {
            SpriteManager spriteMan = SpriteManager.GetInstance();
            spriteMan.BaseRemove(node);
        }
        #endregion
        private static SpriteManager GetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }
        protected override bool Compare(DLink dLink1, DLink dLink2)
        {
            bool result = false;
            Sprite pSpriteBase1 = (Sprite)dLink1;
            Sprite pSpriteBase2 = (Sprite)dLink2;
            Debug.Assert(pSpriteBase1 != null);
            Debug.Assert(pSpriteBase2 != null);
            if (pSpriteBase1.name == pSpriteBase2.name)
            {
                result = true;
            }
            return result;
        }

        protected override DLink CreateNode()
        {
            DLink pNode = new Sprite();
            return pNode;
        }

        protected override void DumpNode(DLink node)
        {
            Debug.Assert(node != null);
            Sprite pSprite = (Sprite)node;
            Debug.WriteLine(String.Format("\t\tSprite:{0}({1})", pSprite.name.ToString(), pSprite.GetHashCode()));
            Debug.WriteLine(String.Format("\t\tColor:({0},{1},{2})", pSprite.pColor.red, pSprite.pColor.green, pSprite.pColor.blue));
            Debug.WriteLine(String.Format("\t\tImage:{0}", pSprite.pImage.name.ToString()));
            Debug.WriteLine(String.Format("\t\tAngle:{0}", pSprite.angle));
            Debug.WriteLine(String.Format("\t\tstatus:{0}", pSprite.status));
        }
    }
}
