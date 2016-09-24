using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

namespace FallDownDemo
{
	public class GameOverPrompt : MonoBehaviour {
		public GUIStyle guiStyle;
		public GUIStyle scoreStyle;
		public GUIStyle highScoreStyle;
		public GUIStyle newScoreStyle;
		public GUIStyle nameStyle;
		
		private List<HighScore> highScores;
		
		private const string playerScoreKey = "PlayerScore";
		private const string highScoreKey = "HighScore";
		private const string highScoreNameKey = "HighScoreName";
		private const int maxScores = 10;
		private const int maxNameLength = 3;

		private bool checkedHighScore = false;
		private bool highScoreWindow = false;
		private bool addedHighScore = false;
		private string highScoreControlName = "HighScoreControl";
		private string gameOverText = "GAME OVER";
		private string scoreText = "Score:";
		private string playAgainText = "Play again?";
		private string yesButtonText = "Yes";
		private string noButtonText = "No";
		private string okButtonText = "OK";
		private string highScoreText = "NEW HIGH SCORE!";
		private string enterNameText = "Please enter your initials: ";
		private string highScoreNameText = string.Empty;
		private int playerScore;

		private Rect mainWindow;
		private Rect gameOverTextRect;

        private Rect newHighScoreRect;// = new Rect((Screen.width/2)-270, (Screen.height/2)-280, 50, 20);
		private Rect newHighPlayerScoreRect = new Rect((Screen.width/2)-205, (Screen.height/2)-245, 50, 20);
		private Rect enterNameRect = new Rect((Screen.width/2)-325, (Screen.height/2)-170, 50, 20);
		private Rect nameEntryRect = new Rect((Screen.width/2)-210, (Screen.height/2)-120, 50, 20);
		private Rect okButtonRect = new Rect((Screen.width/2)-215, (Screen.height/2)-60, 50, 50);
		
		private Rect playerScoreTextRect = new Rect((Screen.width/2)-45, (Screen.height/2)-30, 150, 150);
		private Rect scoreTextRect = new Rect((Screen.width/2)-45, (Screen.height/2)-70, 150, 150);
		private Rect playAgainTextRect = new Rect((Screen.width/2)-5, (Screen.height/2)+35, 150, 150);
		private Rect yesButtonRect = new Rect((Screen.width/2)-10, (Screen.height/2)+55, 35, 25);
		private Rect noButtonRect = new Rect((Screen.width/2)+35, (Screen.height/2)+55, 35, 25);

        private string scoresText = string.Empty;
        private string secretKey = "F@llD0wnDem0Key"; // Edit this value and make sure it's the same as the one stored on the server
        public string addScoreURL = "http://www.carlocaunca.com/AddScore.php?"; //be sure to add a ? to your url
        public string highscoreURL = "http://www.carlocaunca.com/Display.php";

        void Start() {
            CreateRects();
			checkedHighScore = false;
			addedHighScore = false;
			playerScore = 0;
            highScores = new List<HighScore>();
            
		}
        void CreateRects()
        {
            float mainWindowLeft = Screen.width/8;
            float mainWindowTop = Screen.width/8;
            float mainWindowWidth = Screen.width - 2 * mainWindowLeft;
            float mainWindowHeight = Screen.height - 2 * mainWindowTop;
            mainWindow = new Rect(mainWindowLeft, mainWindowTop, mainWindowWidth, mainWindowHeight);
            gameOverTextRect = new Rect((Screen.width / 2)- 65, (Screen.height / 3) - 25, 150, 250);
            newHighScoreRect = new Rect((CameraManager.ScreenWidth() / 2), (Screen.height / 2) - 280, 50, 20);
            newHighPlayerScoreRect = new Rect((Screen.width / 2) - 205, (Screen.height / 2) - 245, 50, 20);
            enterNameRect = new Rect((Screen.width / 2) - 325, (Screen.height / 2) - 170, 50, 20);
            nameEntryRect = new Rect((Screen.width / 2) - 210, (Screen.height / 2) - 120, 50, 20);
            okButtonRect = new Rect((Screen.width / 2) - 215, (Screen.height / 2) - 60, 50, 50);

            playerScoreTextRect = new Rect((Screen.width / 2) - 45, (Screen.height / 2) - 25, 150, 150);
            scoreTextRect = new Rect((Screen.width / 2) - 45, (Screen.height / 2) - 60, 150, 150);
            playAgainTextRect = new Rect((Screen.width / 2) - 5, (Screen.height / 2) + 35, 150, 150);
            yesButtonRect = new Rect((Screen.width / 2) - 10, (Screen.height / 2) + 55, 35, 25);
            noButtonRect = new Rect((Screen.width / 2) + 35, (Screen.height / 2) + 55, 35, 25);
    }

		void OnGUI() {
            CreateRects();
			GUI.Box (mainWindow, string.Empty);
			CheckHighScore ();
			CreateHighScoreWindow();
			GUI.Label (gameOverTextRect, gameOverText, guiStyle);
			GUI.Label (scoreTextRect, scoreText, scoreStyle);
			GUI.Label (playerScoreTextRect, PlayerPrefs.GetInt(playerScoreKey).ToString (), scoreStyle);
			GUI.Label (playAgainTextRect, playAgainText);
			if (GUI.Button (yesButtonRect, yesButtonText))
			{
                SceneManager.LoadScene((int)LevelEnum.Game);
			}
			if (GUI.Button(noButtonRect, noButtonText))
			{
				SceneManager.LoadScene((int)LevelEnum.MainMenu);
			}
		}

		void CheckHighScore()
		{
			if (!checkedHighScore)
			{
				playerScore = PlayerPrefs.GetInt(playerScoreKey);
				highScoreWindow = IsHighScore(playerScore);
				checkedHighScore = true;
			}	
		}

		void CreateHighScoreWindow()
		{
			if (highScoreWindow)
			{
				GUI.ModalWindow(0, mainWindow, CreateHighScoreWindow, string.Empty, newScoreStyle);
			}
		}
		
		void CreateHighScoreWindow(int windowId)
		{
			GUI.Label(newHighScoreRect, highScoreText, highScoreStyle);
			GUI.Label(newHighPlayerScoreRect, PlayerPrefs.GetInt (playerScoreKey).ToString (), highScoreStyle);
			GUI.Label(enterNameRect, enterNameText, highScoreStyle);
			GUI.SetNextControlName(highScoreControlName);
			highScoreNameText = GUI.TextField(nameEntryRect, highScoreNameText, maxNameLength, highScoreStyle);
			GUI.FocusControl(highScoreControlName);
			if (GUI.Button(okButtonRect, okButtonText) && !string.IsNullOrEmpty(highScoreNameText))
			{
                DeleteAllHighScores();
				InsertNewScore();
				highScoreWindow = false;
			}
		}

        private void InsertNewScore()
        {
            List<HighScore> scores = new List<HighScore>();
            bool isNewHighScore = false;
            for (int i = 0; i < highScores.Count; i++)
            {
                if (playerScore > highScores[i].Score)
                {
                    isNewHighScore = true;
                    scores.Add(highScores[i]);
                }
                else //if (playerScore < hs.Score)
                {
                    if (!addedHighScore)
                    {
                        scores.Add(new HighScore(highScoreNameText, playerScore, DateTime.Now));
                        scores.Sort();
                        scores.RemoveAt(0);
                        addedHighScore = true;
                    }
                    scores.Add(highScores[i]);
                }
            }
            if (isNewHighScore && !addedHighScore)
                scores[0] = new HighScore(highScoreNameText, playerScore, DateTime.Now);
            scores.Sort();
            for (int i = 1; i <= scores.Count; i++)
            {
                PlayerPrefs.SetInt(GetHighScoreKeyText(i), scores[i - 1].Score);
                PlayerPrefs.SetString(GetHighScoreNameKeyText(i), scores[i - 1].Name);
                //Debug.Log ("highScore #" + i + ": " + GetHighScoreKeyText(i) + ": " + scores[i-1].Score.ToString());
                //Debug.Log ("highScoreName #" + i + ": " + GetHighScoreNameKeyText(i) + ": " + scores[i-1].Name);
            }
            PlayerPrefs.Save();
        }

		private void DeleteAllHighScores()
		{
			for (int x = 1; x <= maxScores; x++)
			{
				if (DoesKeyExist(GetHighScoreKeyText (x)) && DoesKeyExist(GetHighScoreNameKeyText(x)))
				{
					PlayerPrefs.DeleteKey(GetHighScoreKeyText (x));
					PlayerPrefs.DeleteKey(GetHighScoreNameKeyText(x));
				}
			}
		}
		
		private void GetAllHighScores()
		{
            StartCoroutine(GetScores());
            for (int j = 1; j <= maxScores; j++)
			{
				string hskText = GetHighScoreKeyText(j);
				string hsnkText = GetHighScoreNameKeyText(j);
				if (DoesKeyExist(hskText) && DoesKeyExist(hsnkText))
				{
					highScores.Add(new HighScore(PlayerPrefs.GetString (hsnkText), PlayerPrefs.GetInt (hskText), DateTime.Now));
				}
			}
            SortHighScores();
		}

        private void SortHighScores()
        {
            highScores.Sort();
        }
		
		private bool IsHighScore(int playerScore)
		{
			bool result = false;
            GetAllHighScores();
            if (highScores.Count == 0)
				result = true;
			else
			{
				int higherThan = 0;
				foreach (var hs in highScores)
				{
					if (playerScore > hs.Score)
					{
						result = true;
						higherThan++;
					}
				}
			}
			return result;
		}
        
		private string GetHighScoreKeyText(int hsk)
		{
			return highScoreKey + hsk.ToString();
		}

		private string GetHighScoreNameKeyText(int hsnk)
		{
			return highScoreNameKey + hsnk.ToString ();
		}

		private bool DoesKeyExist(string key)
		{
			return PlayerPrefs.HasKey(key);
		}

        // remember to use StartCoroutine when calling this function!
        IEnumerator PostScores(string name, int score)
        {
            //This connects to a server side php script that will add the name and score to a MySQL DB.
            // Supply it with a string representing the players name and the players score.
            string hash = Md5Sum(name + score + secretKey);

            string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;

            // Post the URL to the site and create a download object to get the result.
            WWW hs_post = new WWW(post_url);
            yield return hs_post; // Wait until the download is done

            if (hs_post.error != null)
            {
                print("There was an error posting the high score: " + hs_post.error);
            }
        }

        // Get the scores from the MySQL DB to display in a GUIText.
        // remember to use StartCoroutine when calling this function!
        IEnumerator GetScores()
        {
            gameObject.GetComponent<GUIText>().text = "Loading Scores";
            WWW hs_get = new WWW(highscoreURL);
            yield return hs_get;

            if (hs_get.error != null)
            {
                print("There was an error getting the high score: " + hs_get.error);
            }
            else
            {
                gameObject.GetComponent<GUIText>().text = hs_get.text; // this is a GUIText that will display the scores in game.
            }
        }
        string Md5Sum(string strToEncrypt)
        {
            System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
            byte[] bytes = ue.GetBytes(strToEncrypt);

            // encrypt bytes
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(bytes);

            // Convert the encrypted bytes back to a string (base 16)
            string hashString = "";

            for (int i = 0; i < hashBytes.Length; i++)
            {
                hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
            }

            return hashString.PadLeft(32, '0');
        }
    }
}