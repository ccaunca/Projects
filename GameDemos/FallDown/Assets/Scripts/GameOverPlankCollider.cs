using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPlankCollider : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other)
    {
        Component component = other.GetComponent<Collider2D>();
        if (component.tag.Equals("Ball"))
        {
            SceneManager.LoadScene((int)LevelEnum.GameOver);
        }
    }
}
