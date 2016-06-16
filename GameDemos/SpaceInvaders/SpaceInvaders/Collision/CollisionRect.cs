using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class CollisionRect : Azul.Rect
    {
        public float minY;
        public CollisionRect(float x, float y, float width, float height)
            : base(x, y, width, height)
        {
            this.minY = 0;
        }
        public CollisionRect(Azul.Rect rect)
            : base(rect)
        {
        }
        ~CollisionRect()
        {
        }
        public static bool Intersect(CollisionRect rect1, CollisionRect rect2)
        {   // use picture and explanation from class
            bool result = false;
            // rect1's min and max values per axes
            float rect1MinX = rect1.x - rect1.width / 2.0f;
            float rect1MaxX = rect1.x + rect1.width / 2.0f;
            float rect1MinY = rect1.y - rect1.height / 2.0f;
            float rect1MaxY = rect1.y + rect1.height / 2.0f;
            // rect2's min and max values per axes
            float rect2MinX = rect2.x - rect2.width / 2.0f;
            float rect2MaxX = rect2.x + rect2.width / 2.0f;
            float rect2MinY = rect2.y - rect2.height / 2.0f;
            float rect2MaxY = rect2.y + rect2.height / 2.0f;
            if (rect2MaxX < rect1MinX || rect2MinX > rect1MaxX || rect2MaxY < rect1MinY || rect2MinY > rect1MaxY)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }
        public void Union(CollisionRect ColRect)
        {
            float minX;
            float minY;
            float maxX;
            float maxY;

            if ((this.x - this.width / 2) < (ColRect.x - ColRect.width / 2))
            {
                minX = (this.x - this.width / 2);
            }
            else
            {
                minX = (ColRect.x - ColRect.width / 2);
            }

            if ((this.x + this.width / 2) > (ColRect.x + ColRect.width / 2))
            {
                maxX = (this.x + this.width / 2);
            }
            else
            {
                maxX = (ColRect.x + ColRect.width / 2);
            }

            if ((this.y + this.height / 2) > (ColRect.y + ColRect.height / 2))
            {
                maxY = (this.y + this.height / 2);
            }
            else
            {
                maxY = (ColRect.y + ColRect.height / 2);
            }

            if ((this.y - this.height / 2) < (ColRect.y - ColRect.height / 2))
            {
                minY = (this.y - this.height / 2);
            }
            else
            {
                minY = (ColRect.y - ColRect.height / 2);
            }
            this.minY = minY;
            this.width = (maxX - minX);
            this.height = (maxY - minY);
            this.x = minX + this.width / 2;
            this.y = minY + this.height / 2;
        }
    }
}
