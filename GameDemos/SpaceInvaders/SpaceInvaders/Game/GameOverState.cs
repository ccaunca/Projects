using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GameOverState : GameState
    {
        public override void Handle(Game pGame)
        {
            pGame.SetState(GameStateEnum.Instructions);
        }
        public override void Draw(Game pGame)
        {
            FontManager.DrawString("GAME OVER", 350.0f, 710.0f, ColorName.Red);
            CurrentPlayer currentPlayer = ScoreManager.GetCurrentPlayer();
            String strLives = String.Empty;
            if (currentPlayer == CurrentPlayer.Player1)
            {
                strLives = ScoreManager.GetScore(ScoreType.Player1Lives).ToString();
            }
            else
            {
                strLives = ScoreManager.GetScore(ScoreType.Player2Lives).ToString();
            }
            FontManager.DrawString(String.Format(" {0}", strLives), 32.0f, 50.0f);
        }
        public override void Start(Game pGame)
        {
            ScoreManager.ClearScore();
            this.Handle(pGame);
            TimerManager.Add(TimerEventName.SetGameState, TimerManager.GetCurrentTime() + 5.0f, TimerManager.GetCurrentTime() + 5.0f, new GameSelectEvent());
        }
        public override void Die(Game pGame)
        {   // Do nothing, GameOver already!
        }
    }
}
