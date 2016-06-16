using System;

namespace SpaceInvaders
{
    class ColorFactory
    {
        public static Color Create(ColorName colorName)
        {
            return new Color(colorName);
        }
    }
}
