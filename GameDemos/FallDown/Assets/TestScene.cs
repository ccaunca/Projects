using UnityEngine;

namespace FallDownDemo
{
    public class TestScene : MonoBehaviour {
        GameObject go;
        // Use this for initialization
        void Start() {
            go = GameObject.Find("Cube");
            go.transform.position = new Vector3(CameraManager.ScreenRightBound()-1, go.transform.position.y, go.transform.position.z);
        }

        // Update is called once per frame
        void Update() {
            //go.transform.position = new Vector3(CameraManager.ScreenRightBound(), go.transform.position.y, go.transform.position.z);
        }
    }
}