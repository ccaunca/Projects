using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AnimationSprite : Command
    {
        public Sprite pSprite;
        public SLink pCurrImage;
        public SLink pFirstImage;
        public AnimationSprite(SpriteBaseName spriteName)
        {
            this.pSprite = SpriteManager.Find(spriteName);
            Debug.Assert(this.pSprite != null);
        }
        public void Attach(ImageName imageName)
        {
            Image image = ImageManager.Find(imageName);
            Debug.Assert(image != null);
            ImageHolder imageHolder = new ImageHolder(image);
            if (pFirstImage == null)
            {
                pFirstImage = imageHolder;
                pCurrImage = pFirstImage;
            }
            else
            {
                pCurrImage.pNext = imageHolder;
                pCurrImage = pCurrImage.pNext;
            }            
        }
        public override void execute(float currentTime)
        {
            ImageHolder imageHolder = this.pCurrImage.pNext == null ? (ImageHolder)this.pFirstImage : (ImageHolder)this.pCurrImage.pNext;
            this.pCurrImage = imageHolder;
            this.pSprite.SwapImage(imageHolder.pImage);
        }
        public void Dump()
        {
            Debug.Assert(this.pFirstImage != null);
            Debug.WriteLine(String.Format("AnimationSprite:{0}({1})", this.pSprite.name.ToString(), this.GetHashCode()));
            Debug.WriteLine(String.Format("CurrentImage:{0}", this.pCurrImage.ToString()));
            Debug.WriteLine("Images:");
            SLink curr = pFirstImage;
            while (curr != null)
            {
                ImageHolder imageHolder = (ImageHolder)curr;
                Debug.WriteLine("\t\t{0}({1})", imageHolder.pImage.name.ToString(), imageHolder.pImage.GetHashCode());
                curr = curr.pNext;
            }
        }
    }
}
