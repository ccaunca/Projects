using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GameActiveState : GameState
    {
        public override void Handle(Game pGame)
        {
            pGame.SetState(GameStateEnum.Over);
        }

        public override void Draw(Game pGame)
        {
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
        {  // Do nothing, already started 
        }

        public override void Die(Game pGame)
        {
            CurrentPlayer currentPlayer = ScoreManager.GetCurrentPlayer();
            ScoreManager.UpdateHighScore(currentPlayer);
            GameManager.ClearGameScreen();
            this.Handle(pGame);
        }
    }
}
