using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class TextureManager : Manager
    {
        private static TextureManager pInstance;
        private TextureManager(int reserveSize = 5, int reserveIncrement = 2)
            : base(reserveSize, reserveIncrement)
        {

        }
        #region Public Methods
        public static Texture Add(TextureName texName, string assetName)
        {
            TextureManager texMan = TextureManager.GetInstance();
            Texture tex = (Texture)texMan.BaseAdd();
            Debug.Assert(tex != null);
            tex.Set(texName, assetName);
            return tex;
        }
        public static void Create(int reserveSize = 3, int reserveIncrement = 1)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(reserveIncrement > 0);

            if (pInstance == null)
            {
                pInstance = new TextureManager(reserveSize, reserveIncrement);
                Texture texture = TextureManager.Add(TextureName.Null, "Stitch.tga");
                Debug.Assert(texture != null);
            }
        }
        public static void Destroy()
        {
            TextureManager textureMan = TextureManager.GetInstance();
            textureMan.BaseDestroy();
        }
        public static void Dump()
        {
            TextureManager texMan = TextureManager.GetInstance();
            texMan.BaseDump();
        }
        public static Texture Find(TextureName texName)
        {
            TextureManager texMan = TextureManager.GetInstance();
            return (Texture)texMan.BaseFind((DLink)new Texture { name = texName });
        }
        public static void Remove(DLink pNode)
        {
            TextureManager texMan = TextureManager.GetInstance();
            texMan.BaseRemove(pNode);
        }
        #endregion
        #region Private Methods
        private static TextureManager GetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }
        #endregion
        #region Base Class Implementation
        protected override bool Compare(DLink dLink1, DLink dLink2)
        {
            Debug.Assert(dLink1 != null);
            Debug.Assert(dLink2 != null);
            Texture tex1 = (Texture)dLink1;
            Texture tex2 = (Texture)dLink2;
            return (tex1.name == tex2.name);
        }

        protected override DLink CreateNode()
        {
            DLink pNode = new Texture();
            return pNode;
        }

        protected override void DumpNode(DLink node)
        {
            Debug.Assert(node != null);
            Texture tex = (Texture)node;
            Debug.WriteLine(String.Format("\tTexture:{0}({1})", tex.name, tex.GetHashCode()));
            Debug.WriteLine(String.Format("\tAzulTex:{0}({1})", tex.assetName, tex.pAzulTexture.GetHashCode()));
            Debug.WriteLine(String.Format("\tstatus:{0}", tex.status));
        }
        #endregion
    }
}
