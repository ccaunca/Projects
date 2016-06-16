using System;
using UnityEngine;

namespace FallDownDemo
{
	public class GameScoreGUI : MonoBehaviour
	{
		private const string highScoreKey = "HighScore";
		private const string playerScoreKey = "PlayerScore";
		private const float windowWidth = 100F;
		private const float windowHeight = 50F;
		private Rect scoreLabelRect;
		private Rect scoreTextRect;
		private Rect timeLabelRect;
		private Rect timeTextRect;
		private GameObject ball;
		private float ballHeight;
		private int _score;
		public int Score
		{
			get { return _score; }
			set { _score = value; }
		}
		public GUIStyle scoreTitleStyle;
		public GUIStyle scoreStyle;
	
		// Use this for initialization
		void Start () {
			PlayerPrefs.DeleteKey (playerScoreKey);
			ball = GameObject.Find ("Ball");
			ballHeight = ball.transform.position.y;
            SetRects();
		}
        void SetRects()
        {
            scoreLabelRect = new Rect(Screen.width - windowWidth, 0, windowWidth, windowHeight);
            scoreTextRect = new Rect(Screen.width - windowWidth, 20, windowWidth / 2, windowHeight / 2);
            timeLabelRect = new Rect(0, 0, windowWidth, windowHeight);
            timeTextRect = new Rect(0, 20, windowWidth / 2, windowHeight / 2);
        }
		
		// Update is called once per frame
		void Update () {
			Score = (int)(ballHeight - ball.transform.position.y);
			PlayerPrefs.SetInt(playerScoreKey, Score);
		}
	
		void OnGUI() {
			CreateScoreGUI();
			CreateTimeGUI();
		}
	
		void CreateScoreGUI()
		{
			GUI.Label(scoreLabelRect, "Score", scoreTitleStyle);
			GUI.TextArea(scoreTextRect, Score.ToString(), scoreStyle);
		}
	
		void CreateTimeGUI()
		{
			GUI.Label(timeLabelRect, "Time", scoreTitleStyle);
			GUI.TextArea(timeTextRect, Convert.ToInt32(Time.timeSinceLevelLoad).ToString (), scoreStyle);
		}
	}
}