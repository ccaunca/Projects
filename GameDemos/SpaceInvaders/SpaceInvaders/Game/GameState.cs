using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum GameStateEnum
    {
        Select, Active, Over, Instructions
    }
    public abstract class GameState
    {
        public abstract void Handle(Game pGame);
        public abstract void Start(Game pGame);
        public abstract void Die(Game pGame);
        public abstract void Draw(Game pGame);
    }
}
