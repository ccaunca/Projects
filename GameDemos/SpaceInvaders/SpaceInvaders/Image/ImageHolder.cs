using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public class ImageHolder : SLink
    {
        public Image pImage;
        public ImageHolder(Image image)
        {
            this.pImage = image;
        }
    }
}
