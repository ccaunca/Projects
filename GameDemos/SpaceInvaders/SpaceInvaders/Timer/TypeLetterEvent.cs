using System;

namespace SpaceInvaders
{
    public class TypeLetterEvent : Command
    {
        private string pLetter;
        private float x;
        private float y;
        private ColorName color;
        public TypeLetterEvent(string letter, float x, float y, ColorName color)
        {
            this.pLetter = letter;
            this.x = x;
            this.y = y;
            this.color = color;
        }
        public override void execute(float currentTime)
        {
            FontManager.DrawString(this.pLetter, this.x, this.y, this.color);
        }
    }
}
