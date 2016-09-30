using UnityEngine;
using UnityEngine.SceneManagement;

namespace FallDownDemo 
{
	public class GameOverTrigger : MonoBehaviour
	{
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private const string highScoreKey = "HighScore";
		void OnTriggerEnter2D(Collider2D other)
		{
            Debug.Log("OnTriggerEnter2D");
			if (other.gameObject.name == "Ball")
			{
                Debug.Log("Hit Ball!");
                SceneManager.LoadScene((int)LevelEnum.GameOver);
			}
		}
		void OnTriggerExit2D(Collider2D other)
		{
			if (other.gameObject.name == "Block")
			{
			}
			else 
			{
				//Destroy(other.gameObject);
			}
		}
	
	}
}