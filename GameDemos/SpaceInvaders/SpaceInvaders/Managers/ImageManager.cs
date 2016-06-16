using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ImageManager : Manager
    {
        private static ImageManager pInstance = null;
        private ImageManager(int reserveSize = 5, int reserveIncrement = 2) :
            base(reserveSize, reserveIncrement)
        {
        }
        public static void Create(int reserveSize = 3, int reserveIncrement = 1)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(reserveIncrement > 0);

            if (pInstance == null)
            {
                pInstance = new ImageManager(reserveSize, reserveIncrement);

                Image image = ImageManager.Add(ImageName.Null, TextureName.Null, 0, 0, 1, 1);
                Debug.Assert(image != null);
            }
        }
        private static ImageManager GetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }
        public static void Dump()
        {
            ImageManager imgMan = ImageManager.GetInstance();
            imgMan.BaseDump();
        }
        public static Image Find(ImageName imgName)
        {
            ImageManager imgMan = ImageManager.GetInstance();
            return (Image)imgMan.BaseFind(new Image { name = imgName });
        }
        public static Image Add(ImageName imgName, TextureName texName, float x, float y, float width, float height)
        {   // Remove from Reserve, Add to Active
            ImageManager imageMan = ImageManager.GetInstance();
            Image pImage = (Image)imageMan.BaseAdd();
            Debug.Assert(pImage != null);
            pImage.Set(imgName, texName, x, y, width, height);
            return pImage;
        }
        #region Base Class Implementation
        protected override bool Compare(DLink dLink1, DLink dLink2)
        {
            Debug.Assert(dLink1 != null);
            Debug.Assert(dLink2 != null);
            Image image1 = (Image)dLink1;
            Image image2 = (Image)dLink2;
            return (image1.name == image2.name);
        }
        protected override DLink CreateNode()
        {
            DLink pNode = new Image();
            return pNode;
        }
        protected override void DumpNode(DLink node)
        {
            Debug.Assert(node != null);
            Image pImage = (Image)node;
            pImage.Dump();
        }
        #endregion
    }
}
