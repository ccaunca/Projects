using UnityEngine;

public class Plank : MonoBehaviour {
	private GameObject gameOverPlank;
	// Use this for initialization
	void Start () {
		gameOverPlank = GameObject.Find ("GameOverPlank");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 gameOverPlankGlobalPosition = gameOverPlank.transform.TransformPoint(gameOverPlank.transform.position);
		Vector3 plankGlobalPosition = this.transform.TransformPoint(this.transform.position);
		if (plankGlobalPosition.y > gameOverPlankGlobalPosition.y)
		{
			GameObject.Destroy (this);
		}
	}
}
