using UnityEngine;

public class BallController : MonoBehaviour {

	private int ballSpeed = 10;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftArrow))
			transform.Translate (-Vector2.right * Time.deltaTime * ballSpeed);
		if (Input.GetKey(KeyCode.RightArrow))
			transform.Translate (Vector2.right * Time.deltaTime * ballSpeed);
    }
}
