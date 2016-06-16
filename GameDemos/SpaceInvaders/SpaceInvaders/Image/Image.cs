using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum ImageName
    {
        CrabA, CrabB,
        //Alien3A, Alien4A,
        //Alien5A, Alien6A,
        SquidA, SquidB,
        OctopusA, OctopusB,
        //Squid5A, Squid6A,
        Brick, BrickLeftTop0, BrickLeftTop1, BrickLeftBottom,
        BrickRightTop0, BrickRightTop1, BrickRightBottom,
        ShipExplosion, AlienExplosion, MissileExplosion,
        Ship, UFO, Missile,
        BombDaggerA, BombDaggerB,
        BombZigZagA, BombZigZagB,
        BombStraightA, BombStraightB,
        Null, Uninitialized
    }
    public class Image : DLink
    {
        #region Public Properties
        public ImageName name;
        public Azul.Rect pRect;
        public Texture pTexture;
        #endregion
        #region Constructors
        public Image()
        {
            this.name = ImageName.Uninitialized;
            this.pRect = new Azul.Rect();
            this.pTexture = null;
        }
        public Image(ImageName imgName, Azul.Rect rect, Texture text)
        {
            this.name = imgName;
            this.pRect = rect;
            this.pTexture = text;
        }
        #endregion
        #region Public Methods
        public void Set(ImageName imgName, TextureName textName, float x, float y, float width, float height)
        {
            this.name = imgName;
            this.pRect.Set(x, y, width, height);
            this.pTexture = TextureManager.Find(textName);
        }
        public void Dump()
        {
            Debug.WriteLine(String.Format("\t\tImage: {0}({1})", this.name, this.GetHashCode()));
            if (pTexture != null)
            {
                Debug.WriteLine(String.Format("\t\tTexture: {0}({1})", this.pTexture.name.ToString(), this.pTexture.pAzulTexture.GetHashCode()));
            }
            else
            {
                Debug.WriteLine("\t\tTexture: null");
            }
            if (this.pRect != null)
            {
                Debug.WriteLine(String.Format("\t\tRect: ({0},{1},{2},{3})", this.pRect.x, this.pRect.y, this.pRect.width, this.pRect.height));
            }
            else
            {
                Debug.WriteLine("\t\tRect: null");
            }
        }
        #endregion
    }
}
