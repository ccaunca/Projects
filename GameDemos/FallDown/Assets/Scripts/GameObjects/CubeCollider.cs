using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //SceneManager.LoadScene((int)LevelEnum.Game);
    }
}
