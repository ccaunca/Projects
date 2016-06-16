using System;

namespace SpaceInvaders
{
    public enum ColorName
    {
        Red, Orange, Yellow,
        Green, Blue, Indigo,
        Violet, Black, White
    }
    public class Color
    {
        public Azul.Color pAzulColor;
        public ColorName name;
        public Color(ColorName colorName)
        {
            this.name = colorName;
            this.pAzulColor = GetColor(colorName);
        }
        private Azul.Color GetAzulColor(float r, float g, float b)
        {
            return new Azul.Color(r, g, b);
        }
        private Azul.Color GetColor(ColorName colorName)
        {
            Azul.Color azulColor = GetAzulColor(1.0f, 1.0f, 1.0f);
            switch(colorName)
            {
                case ColorName.Red :
                    azulColor.Set(1.0f, 0.0f, 0.0f);
                    break;
                case ColorName.Orange :
                    azulColor.Set(0.5f, 0.5f, 0.0f);
                    break;
                case ColorName.Yellow :
                    azulColor.Set(1.0f, 1.0f, 0.0f);
                    break;
                case ColorName.Green :
                    azulColor.Set(0.0f, 1.0f, 0.0f);
                    break;
                case ColorName.Blue :
                    azulColor.Set(0.0f, 0.0f, 1.0f);
                    break;
                case ColorName.Indigo :
                    azulColor.Set(0.4f, 0.0f, 1.0f);
                    break;
                case ColorName.Violet :
                    azulColor.Set(0.5f, 0.0f, 1.0f);
                    break;
                case ColorName.Black :
                    azulColor.Set(0.0f, 0.0f, 0.0f);
                    break;
                case ColorName.White :
                default :
                    azulColor.Set(1.0f, 1.0f, 1.0f);
                    break;
            }
            return azulColor;
        }
    }
}
