using UnityEngine;
using UnityEngine.SceneManagement;

namespace FallDownDemo
{
	public class GameManager : MonoBehaviour {
		private bool isDown = true;
        private CameraManager cameraManager;
        private GameObject leftPlank;
        private GameObject rightPlank;
		// Use this for initialization
		void Start () {
            cameraManager = CameraManager.GetInstance();
			Physics2D.gravity = new Vector2(0, GetDirection());
            leftPlank = GameObject.Find("LeftPlank");
            rightPlank = GameObject.Find("RightPlank");
		}
	
		int GetDirection()
		{
			if (isDown)
				return -1;
			else
				return 1;
		}
		// Update is called once per frame
		void Update () {
            cameraManager.UpdateScreenBounds(transform, Camera.main);
            if (SceneManager.GetActiveScene().name == "Game")
            {
                rightPlank.transform.position = new Vector3(CameraManager.ScreenRightBound() + 1f, rightPlank.transform.position.y);
                leftPlank.transform.position = new Vector3(CameraManager.ScreenLeftBound() - 1f, leftPlank.transform.position.y);
            }
		}
	}
}