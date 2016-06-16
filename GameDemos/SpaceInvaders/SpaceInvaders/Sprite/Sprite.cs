using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Sprite : SpriteBase
    {
        #region Properties
        public SpriteBaseName name;
        public Azul.Sprite pAzulSprite;
        public Azul.Color pColor;
        public Image pImage;
        public Azul.Rect pScreenRect;
        public float x;
        public float y;
        public float sx;
        public float sy;
        public float angle;
        #endregion
        #region Constructor
        public Sprite()
            : base()
        {
            this.pAzulSprite = new Azul.Sprite();
            this.pScreenRect = new Azul.Rect();
            this.pColor = new Azul.Color(1.0f, 1.0f, 1.0f);
            this.pImage = new Image();
        }
        ~Sprite()
        {
            this.pAzulSprite = null;
            this.pColor = null;
            this.pImage = null;
            this.pScreenRect = null;
            this.Clear();
        }
        #endregion
        #region Public Methods
        public void Set(SpriteBaseName name, ImageName imgName, float x, float y, float w, float h)
        {
            this.name = name;
            this.pImage = ImageManager.Find(imgName);
            this.pScreenRect.Set(x, y, w, h);
            this.pColor.Set(1.0f, 1.0f, 1.0f, 1.0f);
            this.pAzulSprite.Swap(pImage.pTexture.pAzulTexture, pImage.pRect, this.pScreenRect, this.pColor);
            this.x = pAzulSprite.x;
            this.y = pAzulSprite.y;
            this.sx = pAzulSprite.sx;
            this.sy = pAzulSprite.sy;
        }
        public void SetColor(Azul.Color color)
        {
            Debug.Assert(pColor != null);
            this.pColor = color;
        }
        public void SwapImage(Image pImg)
        {
            Debug.Assert(pImg != null);

            this.pImage = pImg;
            this.pAzulSprite.SwapTexture(pImg.pTexture.pAzulTexture);
            this.pAzulSprite.SwapTextureRect(pImg.pRect);
        }
        public Azul.Rect GetScreenRect()
        {
            Debug.Assert(this.pScreenRect != null);
            return this.pScreenRect;
        }
        #endregion
        #region Base Class Implementation
        public override void Render()
        {
            this.pAzulSprite.Render();
        }

        public override void Update()
        {
            Debug.Assert(pAzulSprite != null);
            this.pAzulSprite.x = this.x;
            this.pAzulSprite.y = this.y;
            this.pAzulSprite.sx = this.sx;
            this.pAzulSprite.sy = this.sy;
            this.pAzulSprite.angle = this.angle;
            this.pAzulSprite.SwapColor(this.pColor);

            this.pAzulSprite.Update();
        }
        public override Enum GetName()
        {
            return this.name;
        }
        #endregion
    }
}
