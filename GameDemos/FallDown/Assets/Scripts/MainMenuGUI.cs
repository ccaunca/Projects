using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace FallDownDemo
{
    public class MainMenuGUI : MonoBehaviour
    {
        public GUIStyle guiBoxStyle;
        public GUIStyle guiButtonStyle;
        public GUIStyle guiHighScoreStyle;
        public GUIStyle guiTopScoresStyle;
        private const string highScoreKey = "HighScore";
        private const string highScoreNameKey = "HighScoreName";
        private int maxScores = 10;
        private int leftMarginRank = 50;
        private int leftMarginScore = 165;
        private int leftMarginInitials = 300;
        private int topMargin = 60;
        private int topMarginSpacingFactor = 30;
        private int titleHeight = 85;
        private int highScoreHeightFactor = 100;
        private float screenMargin = 175f;
        private float buttonMargin = 50f;
        private float buttonWidth = 200f;
        private float buttonHeight = 75f;
        private string boxTitle = "Fall Down";
        private string startButtonTitle = "Start";
        private string highScoresButtonTitle = "High Scores";
        private string resetHighScoresButtonTitle = "Reset High Scores";
        private string topHighScoresTitle = "TOP 10 HIGH SCORES";
        private string rankTitle = "Rank";
        private string scoreTitle = "Score";
        private string initialsTitle = "Initials";
        private string buttonHighScoreOKText = "OK";
        private bool allowReset = false;
        private string secretKey = "F@llD0wnDem0Key"; // Edit this value and make sure it's the same as the one stored on the server
        public string addScoreURL = "http://www.carlocaunca.com/AddScore.php?"; //be sure to add a ? to your url
        public string highscoreURL = "http://www.carlocaunca.com/Display.php";

        private bool highScoreWindow;

        private Rect boxRect;
        private Rect goButtonRect;
        private Rect highScoreButtonRect;
        private Rect highScoreWindowRect;
        private Rect rankRect;
        private Rect scoreRect;
        private Rect initialsRect;
        //private Rect highScoreTextBoxRect;
        //private Rect highScoreNameRect;
        private Rect highScoreOKButtonRect;
        private Rect resetHighScoresButtonRect;
        // Use this for initialization
        void Start()
        {
            SetRects();
        }

        void SetRects()
        {
            boxRect = new Rect((CameraManager.ScreenWidth() / 2) - 75, 15F, 150, CameraManager.ScreenHeight() - 4 * screenMargin);
            goButtonRect = new Rect((CameraManager.ScreenWidth() / 2) - (buttonWidth / 2), screenMargin + buttonMargin, buttonWidth, buttonHeight);
            highScoreButtonRect = new Rect((CameraManager.ScreenWidth() / 2) - (buttonWidth / 2), screenMargin + buttonMargin + buttonHeight, buttonWidth, buttonHeight);
            highScoreWindowRect = new Rect((CameraManager.ScreenWidth() / 12), 10, CameraManager.ScreenWidth() - (2 * (CameraManager.ScreenWidth() / 12)), CameraManager.ScreenHeight() - 25);
            rankRect = new Rect(CameraManager.ScreenWidth() / 2 - 300, highScoreWindowRect.yMin + 40, buttonWidth, titleHeight);
            scoreRect = new Rect(CameraManager.ScreenWidth() / 2 -200, highScoreWindowRect.yMin + 40, buttonWidth, titleHeight);
            initialsRect = new Rect(CameraManager.ScreenWidth() / 2 - 90, highScoreWindowRect.yMin + 40, buttonWidth, titleHeight);
            highScoreOKButtonRect = new Rect(CameraManager.ScreenWidth() / 3, 420, buttonWidth / 2, 25);
            if (allowReset)
                resetHighScoresButtonRect = new Rect((CameraManager.ScreenWidth() / 2) - (buttonWidth - 150), screenMargin + buttonMargin + 2 * buttonHeight, buttonWidth, buttonHeight);
        }

        // Update is called once per frame
        void Update()
        {
            SetRects();
        }

        void OnGUI()
        {
            GUI.Box(boxRect, boxTitle, guiBoxStyle);
            if (GUI.Button(goButtonRect, startButtonTitle, guiButtonStyle))
            {
                SceneManager.LoadScene((int)LevelEnum.Game);
            }
            if (GUI.Button(highScoreButtonRect, highScoresButtonTitle, guiButtonStyle))
            {
                highScoreWindow = true;
            }
            if (highScoreWindow)
            {
                CheckPlayerPrefs();
                GUI.ModalWindow(0, highScoreWindowRect, CreateHighScoreWindow, topHighScoresTitle, guiHighScoreStyle);
            }
            if (allowReset)
            {
                if (GUI.Button(resetHighScoresButtonRect, resetHighScoresButtonTitle, guiButtonStyle))
                    ClearAllScores();
            }
        }

        void CheckPlayerPrefs()
        {
            string hsNameKey;
            string hsScoreKey;
            for (int i = 1; i <= maxScores; i++)
            {
                hsNameKey = highScoreNameKey + i.ToString();
                hsScoreKey = highScoreKey + i.ToString();
                if (!PlayerPrefs.HasKey(hsScoreKey))
                    PlayerPrefs.SetInt(hsScoreKey, 0);
                if (!PlayerPrefs.HasKey(hsNameKey))
                    PlayerPrefs.SetString(hsNameKey, "***");
            }
        }

        void CreateHighScoreWindow(int windowId)
        {
            int j = maxScores;
            GUI.Label(rankRect, rankTitle, guiTopScoresStyle);
            GUI.Label(scoreRect, scoreTitle, guiTopScoresStyle);
            GUI.Label(initialsRect, initialsTitle, guiTopScoresStyle);
            //GUI.Label(new Rect(leftMarginRank, topMargin, buttonWidth, titleHeight), rankTitle, guiTopScoresStyle);
            //GUI.Label(new Rect(leftMarginScore, topMargin, buttonWidth, titleHeight), scoreTitle, guiTopScoresStyle);
            //GUI.Label(new Rect(leftMarginInitials, topMargin, buttonWidth, titleHeight), initialsTitle, guiTopScoresStyle);
            StartCoroutine(GetScores());
            for (int i = 1; i <= maxScores; i++)
            {
                GUI.Label(new Rect(rankRect.x, (topMarginSpacingFactor * i) + topMargin, buttonWidth, highScoreHeightFactor * i), "#" + i, guiTopScoresStyle);
                GUI.Label(new Rect(scoreRect.x, (topMarginSpacingFactor * i) + topMargin, buttonWidth, highScoreHeightFactor * i), PlayerPrefs.GetInt(GetHighScoreKeyText(j)).ToString(), guiTopScoresStyle);
                GUI.Label(new Rect(initialsRect.x, (topMarginSpacingFactor * i) + topMargin, buttonWidth, highScoreHeightFactor * i), PlayerPrefs.GetString(GetHighScoreNameKeyText(j)), guiTopScoresStyle);
                j--;
            }
            if (GUI.Button(highScoreOKButtonRect, buttonHighScoreOKText))
            {
                highScoreWindow = false;
            }
        }

        void ClearAllScores()
        {
            for (int x = 1; x <= maxScores; x++)
            {
                if (DoesKeyExist(GetHighScoreKeyText(x)) && DoesKeyExist(GetHighScoreNameKeyText(x)))
                {
                    PlayerPrefs.DeleteKey(GetHighScoreKeyText(x));
                    PlayerPrefs.DeleteKey(GetHighScoreNameKeyText(x));
                }
            }
        }

        private string GetHighScoreKeyText(int hsk)
        {
            return highScoreKey + hsk.ToString();
        }

        private string GetHighScoreNameKeyText(int hsnk)
        {
            return highScoreNameKey + hsnk.ToString();
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