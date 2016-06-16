using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum FontName
    {
        Consolas36pt,
        Null,
        Uninitialized
    }
    public class Font : DLink
    {
        public FontName name;
        public int key;
        private Azul.Rect pAzulRect;
        private Texture pTexture;
        public Font()
            : base()
        {
            this.name = FontName.Uninitialized;
            this.pTexture = null;
            this.pAzulRect = new Azul.Rect();
            this.key = 0;
        }
        ~Font()
        {
            this.name = FontName.Uninitialized;
            this.pAzulRect = null;
            this.pTexture = null;
        }
        public void Set(FontName fontName, int key, TextureName textureName, float x, float y, float w, float h)
        {
            Debug.Assert(this.pAzulRect != null);
            this.name = fontName;

            this.pTexture = TextureManager.Find(textureName);
            Debug.Assert(pTexture != null);

            this.pAzulRect.Set(x, y, w, h);
            this.key = key;
        }
        public void Dump()
        {
        }
        public Azul.Rect GetAzulRect()
        {
            Debug.Assert(this.pAzulRect != null);
            return this.pAzulRect;
        }
        public Azul.Texture GetAzulTexture()
        {
            Debug.Assert(this.pTexture != null);
            return this.pTexture.pAzulTexture;
        }
    }
}
