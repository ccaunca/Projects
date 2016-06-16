using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum ProxySpriteName
    {
        Proxy,
        Null,
        Uninitialized
    }
    public class ProxySprite : SpriteBase
    {
        public ProxySpriteName name;
        public Sprite pSprite;
        public float x;
        public float y;
        public float sx;
        public float sy;
        public ProxySprite()
        {
            this.name = ProxySpriteName.Proxy;
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.pSprite = null;
        }
        public ProxySprite(SpriteBaseName spriteName)
        {
            this.name = ProxySpriteName.Proxy;
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.pSprite = SpriteManager.Find(spriteName);
            Debug.Assert(this.pSprite != null);
        }
        ~ProxySprite()
        {
            this.pSprite = null;
        }
        public void Set(SpriteBaseName spriteName)
        {
            this.name = ProxySpriteName.Proxy;
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.pSprite = SpriteManager.Find(spriteName);
            Debug.Assert(pSprite != null);
        }
        public override void Render()
        {
            UpdateSprite();
            this.pSprite.Update();
            this.pSprite.Render();
        }

        public override void Update()
        {

        }
        public override Enum GetName()
        {
            return this.name;
        }
        private void UpdateSprite()
        {
            this.pSprite.x = this.x;
            this.pSprite.y = this.y;
            this.pSprite.sx = this.sx;
            this.pSprite.sy = this.sy;
        }
    }
}
