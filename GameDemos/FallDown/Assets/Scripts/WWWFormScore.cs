using UnityEngine;
using System.Collections;

namespace FallDownDemo
{
    public class WWWFormScore : MonoBehaviour
    {
        string highscore_URL = "http://www.carlocaunca.com/testHighScores.pl";
        string playName = "Player 1";
        int score = -1;

        // Initialization
        IEnumerator Start()
        {
            // Create a form object for sending high score data to the server
            WWWForm form = new WWWForm();
            // Assuming the perl script manages high scores for different games
            form.AddField("game", "MyGameName");
            // The name of the player submitting the scores
            form.AddField("playerName", playName);
            // The score
            form.AddField("score", score);

            // Create a download object
            WWW download = new WWW(highscore_URL, form);

            // Wait until the download is done
            yield return download;

            if (!string.IsNullOrEmpty(download.error))
            {
                print("Error downloading: " + download.error);
            }
            else
            {
                // show the high scores
                Debug.Log(download.text);
            }
        }
    }
}
