using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum TextureName
    {
        Aliens, Birds, HotPink, Sprites, Stitch, Shields, Uninitialized, Null,
        Consolas36pt,
    }
    public class Texture : DLink
    {
        #region Properties
        public TextureName name;
        public string assetName;
        public Azul.Texture pAzulTexture;
        #endregion
        #region Constructors
        public Texture()
        {
            this.name = TextureName.Uninitialized;
            this.assetName = "HotPink.tga";
            this.pAzulTexture = new Azul.Texture("HotPink.tga");
        }
        public Texture(TextureName textureName, string assetName)
        {
            this.Set(textureName, assetName);
        }
        #endregion
        #region Public Methods
        public void Set(TextureName textureName, string assetName)
        {
            this.name = textureName;
            this.assetName = assetName;
            Azul.Texture pTex = new Azul.Texture(assetName);
            if (pTex == null)
            {
                pTex = new Azul.Texture("HotPink.tga");
            }
            this.pAzulTexture = pTex;
        }
        public void Dump()
        {
            Debug.WriteLine(String.Format("\t\tTexture:{0}({1})", this.name, this.GetHashCode()));
            Debug.WriteLine(String.Format("\t\tAssetName:{0}({1})", this.assetName, this.pAzulTexture.GetHashCode()));
        }
        #endregion
    }
}
