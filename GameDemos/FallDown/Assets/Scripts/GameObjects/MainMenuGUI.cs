using UnityEngine;
using UnityEngine.SceneManagement;

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
        private bool highScoreWindow;
        private Rect boxRect;
        private Rect goButtonRect;
        private Rect highScoreButtonRect;
        private Rect highScoreWindowRect;
        private Rect rankRect;
        private Rect scoreRect;
        private Rect initialsRect;
        private Rect highScoreOKButtonRect;
        private Rect resetHighScoresButtonRect;
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
        void OnGUI()
        {
            SetRects();
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
    }
}