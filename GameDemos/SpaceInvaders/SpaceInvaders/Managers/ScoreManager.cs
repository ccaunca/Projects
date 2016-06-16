using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum CurrentPlayer
    {
        Player1, Player2
    }
    public enum ScoreType
    {
        Player1Score, Player2Score, HiScore, Player1Lives, Player2Lives, Credits
    }
    public class ScoreManager
    {
        private static ScoreManager pInstance = null;
        private int player1Score;
        private int player2Score;
        private int hiScore;
        private int player1Lives;
        private int player2Lives;
        private int credits;
        private CurrentPlayer currentPlayer;
        private static Random pRandom = new Random();
        private ScoreManager()
        {
            this.player1Score = 0;
            this.player2Score = 0;
            this.hiScore = 0;
            this.player1Lives = 3;
            this.player2Lives = 3;
            this.credits = 0;
            this.currentPlayer = CurrentPlayer.Player1;
        }
        private static ScoreManager GetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }
        public static void Initialize()
        {
            if (pInstance == null)
            {
                pInstance = new ScoreManager();
            }
        }
        public static void SetCurrentPlayer(CurrentPlayer newPlayer)
        {
            ScoreManager scoreMan = ScoreManager.GetInstance();
            scoreMan.currentPlayer = newPlayer;
        }
        public static void UpdateScore(GameObject go)
        {
            ScoreManager scoreMan = ScoreManager.GetInstance();
            CurrentPlayer player = GetCurrentPlayer();
            GameObjectName goName = go.gameObjectName;
            int scoreValue = 0;
            switch (goName)
            {
                case GameObjectName.Crab :
                    scoreValue = 20;
                    break;
                case GameObjectName.Squid :
                    scoreValue = 30;
                    break;
                case GameObjectName.Octopus :
                    scoreValue = 10;
                    break;
                case GameObjectName.UFO :
                    scoreValue = GenerateUFOScore();
                    break;
                default :
                    Debug.Assert(false);
                    break;
            }
            UpdateCurrentPlayerScore(scoreValue);
        }

        private static int GenerateUFOScore()
        {
            int random = pRandom.Next(0, 6);
            switch(random)
            {
                case 1 :
                    random = 100;
                    break;
                case 2 :
                    random = 150;
                    break;
                case 3 :
                    random = 200;
                    break;
                case 4 :
                    random = 250;
                    break;
                case 5 :
                    random = 300;
                    break;
                case 0:
                default:
                    random = 50;
                    break;
            }
            return random;
        }
        public static int UpdateLives()
        {
            int currentLives = 0;
            ScoreManager scoreMan = ScoreManager.GetInstance();
            CurrentPlayer player = GetCurrentPlayer();
            if (player == CurrentPlayer.Player1)
            {
                scoreMan.player1Lives -= 1;
                currentLives = scoreMan.player1Lives;
            }
            else if (player == CurrentPlayer.Player2)
            {
                scoreMan.player2Lives -= 1;
                currentLives = scoreMan.player2Lives;
            }
            else
            {
                Debug.Assert(false);
            }
            return currentLives;
        }
        public static void OneUp()
        {
            ScoreManager scoreMan = ScoreManager.GetInstance();
            CurrentPlayer player = GetCurrentPlayer();
            if (player == CurrentPlayer.Player1)
            {
                scoreMan.player1Lives += 1;
            }
            else if (player == CurrentPlayer.Player2)
            {
                scoreMan.player2Lives += 1;
            }
            else
            {
                Debug.Assert(false);
            }
        }
        public static void UpdateCurrentPlayerScore(int value)
        {
            ScoreManager scoreMan = ScoreManager.GetInstance();
            CurrentPlayer player = GetCurrentPlayer();
            if (player == CurrentPlayer.Player1)
            {
                scoreMan.player1Score += value;
            }
            else if (player == CurrentPlayer.Player2)
            {
                scoreMan.player2Score += value;
            }
            else
            {
                Debug.Assert(false);
            }
        }
        public static CurrentPlayer GetCurrentPlayer()
        {
            ScoreManager scoreMan = ScoreManager.GetInstance();
            return scoreMan.currentPlayer;
        }
        public static int GetLives()
        {
            int lives = 0;
            ScoreManager scoreMan = ScoreManager.GetInstance();
            if (GetCurrentPlayer() == CurrentPlayer.Player1)
            {
                lives = GetScore(ScoreType.Player1Lives);
            }
            else
            {
                lives = GetScore(ScoreType.Player2Lives);
            }
            return lives;
        }
        public static void UpdateScore(ScoreType type, int value)
        {
            ScoreManager scoreMan = ScoreManager.GetInstance();
            switch (type)
            {
                case ScoreType.Player1Score :
                    scoreMan.player1Score = value;
                    break;
                case ScoreType.Player2Score :    
                    scoreMan.player2Score = value;
                    break;
                case ScoreType.HiScore :
                    scoreMan.hiScore = value;
                    break;
                case ScoreType.Player1Lives :
                    scoreMan.player1Lives = value;
                    break;
                case ScoreType.Player2Lives:
                    scoreMan.player2Lives = value;
                    break;
                case ScoreType.Credits :
                    scoreMan.credits = value;
                    break;
                default :
                    Debug.Assert(false);
                    break;
            }
        }
        public static int GetScore(ScoreType type)
        {
            int score = 0;
            ScoreManager scoreMan = ScoreManager.GetInstance();
            switch(type)
            {
                case ScoreType.Player1Score:
                    score = scoreMan.player1Score;
                    break;
                case ScoreType.Player2Score:
                    score = scoreMan.player2Score;
                    break;
                case ScoreType.HiScore:
                    score = scoreMan.hiScore;
                    break;
                case ScoreType.Player1Lives:
                    score = scoreMan.player1Lives;
                    break;
                case ScoreType.Player2Lives:
                    score = scoreMan.player2Lives;
                    break;
                case ScoreType.Credits:
                    score = scoreMan.credits;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            return score;
        }
        public static int GetPlayerScore(CurrentPlayer currPlayer)
        {
            int score = -1;
            if (currPlayer == CurrentPlayer.Player1)
            {
                score = GetScore(ScoreType.Player1Score);
            }
            else
            {
                score = GetScore(ScoreType.Player2Score);
            }
            return score;
        }

        public static void UpdateHighScore(CurrentPlayer currentPlayer)
        {
            int hiScore = GetScore(ScoreType.HiScore);
            int playerScore = GetPlayerScore(currentPlayer);
            if (hiScore < playerScore)
            {
                UpdateScore(ScoreType.HiScore, playerScore);
            }
        }

        public static void ClearScore()
        {
            CurrentPlayer currentPlayer = GetCurrentPlayer();
            ScoreType scoreType = ScoreType.Player1Score;
            if (currentPlayer == CurrentPlayer.Player2)
            {
                scoreType = ScoreType.Player2Score;
            }
            UpdateScore(scoreType, 0);
        }

        public static void ResetLives()
        {
            CurrentPlayer currentPlayer = GetCurrentPlayer();
            ScoreType scoreType = ScoreType.Player1Lives;
            if (currentPlayer == CurrentPlayer.Player2)
            {
                scoreType = ScoreType.Player2Lives;
            }
            UpdateScore(scoreType, 3);
        }
        public static void ClearLives()
        {
            CurrentPlayer currentPlayer = GetCurrentPlayer();
            ScoreType scoreType = ScoreType.Player1Lives;
            if (currentPlayer == CurrentPlayer.Player2)
            {
                scoreType = ScoreType.Player2Lives;
            }
            UpdateScore(scoreType, 0);
        }
    }
}
