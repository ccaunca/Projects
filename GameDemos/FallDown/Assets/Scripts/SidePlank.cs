using UnityEngine;

public class SidePlank : MonoBehaviour {
	private GameObject leftPlank;
	private GameObject rightPlank;
	
	// Use this for initialization
	void Start () {
		leftPlank = GameObject.Find("LeftPlank");
		rightPlank = GameObject.Find ("RightPlank");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (this.name == "LeftPlank" && other.transform.name == "Ball")
		{
			other.transform.position = new Vector2(rightPlank.transform.position.x-1f, other.transform.position.y);
		}
		if (this.name == "RightPlank" && other.transform.name == "Ball")
		{
			other.transform.position = new Vector2(leftPlank.transform.position.x+1F, other.transform.position.y);
		}
	}
}
