using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ProxySpriteManager : Manager
    {
        private static ProxySpriteManager pInstance;
        private ProxySpriteManager(int reserveSize, int reserveIncrement)
            : base(reserveSize, reserveIncrement)
        {

        }
        public static ProxySprite Add(SpriteBaseName spriteName)
        {
            ProxySpriteManager proxySpriteMan = ProxySpriteManager.GetInstance();
            ProxySprite proxySprite = (ProxySprite)proxySpriteMan.BaseAdd();
            proxySprite.Set(spriteName);
            return proxySprite;
        }
        public static void Create(int reserveSize, int reserveIncrement)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(reserveIncrement > 0);
            if (pInstance == null)
            {
                pInstance = new ProxySpriteManager(reserveSize, reserveIncrement);
            }
        }
        public static void Dump()
        {
            ProxySpriteManager proxySpriteMan = ProxySpriteManager.GetInstance();
            proxySpriteMan.BaseDump();
        }

        private static ProxySpriteManager GetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }
        protected override bool Compare(DLink dLink1, DLink dLink2)
        {
            throw new NotImplementedException();
        }

        protected override DLink CreateNode()
        {
            return (DLink)new ProxySprite();
        }

        protected override void DumpNode(DLink node)
        {
            throw new NotImplementedException();
        }
    }
}
