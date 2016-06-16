using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteBox : SpriteBase
    {
        #region Properties
        public SpriteBaseName name;
        public Azul.SpriteBox pAzulSpriteBox;
        public Azul.Color pLineColor;
        public Azul.Rect pScreenRect;
        public float x;
        public float y;
        public float sx;
        public float sy;
        public float angle;
        #endregion
        #region Constructor
        public SpriteBox()
            : base()
        {
            this.name = SpriteBaseName.Uninitialized;
            this.pAzulSpriteBox = new Azul.SpriteBox();
            this.pLineColor = ColorFactory.Create(ColorName.White).pAzulColor;
            this.pScreenRect = new Azul.Rect();
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.angle = 0.0f;
        }
        
        ~SpriteBox()
        {
            this.name = SpriteBaseName.Uninitialized;
            this.pAzulSpriteBox = null;
            this.pLineColor = null;
            this.pScreenRect = null;
        }
        #endregion
        #region Public Functions
        public void Set(SpriteBaseName name, float x, float y, float w, float h)
        {
            this.name = name;
            this.pScreenRect.Set(x, y, w, h);
            this.pAzulSpriteBox.Swap(this.pScreenRect, this.pLineColor);
            this.x = pAzulSpriteBox.x;
            this.y = pAzulSpriteBox.y;
            this.sx = pAzulSpriteBox.sx;
            this.sy = pAzulSpriteBox.sy;
            this.angle = pAzulSpriteBox.angle;
        }
        public void SetScreenRect(float x, float y, float w, float h)
        {
            this.Set(this.name, x, y, w, h);
        }
        #endregion
        #region Base Class Implementation
        public override void Render()
        {
            this.pAzulSpriteBox.Render();
        }

        public override void Update()
        {
            this.pAzulSpriteBox.x = this.x;
            this.pAzulSpriteBox.y = this.y;
            this.pAzulSpriteBox.sx = this.sx;
            this.pAzulSpriteBox.sy = this.sy;
            this.pAzulSpriteBox.angle = this.angle;
            this.pAzulSpriteBox.SwapColor(this.pLineColor);
            this.pAzulSpriteBox.Update();
        }
        public override Enum GetName()
        {
            return this.name;
        }
        #endregion
    }
}
