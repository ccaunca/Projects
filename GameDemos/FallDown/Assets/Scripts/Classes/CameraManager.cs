using UnityEngine;

namespace FallDownDemo
{
    class CameraManager
    {
        private static CameraManager Instance;
        private float screenWidth;
        private float screenHeight;
        private float screenLeftBound;
        private float screenRightBound;
        private float screenTopBound;
        private float screenBottomBound;
        private CameraManager()
        {
            
        }
        public static CameraManager GetInstance()
        {
            if (Instance == null)
            {
                Instance = new CameraManager();
            }
            return Instance;
        }
        public void UpdateScreenBounds(Transform transform, Camera camera)
        {
            float dist = (transform.position - camera.transform.position).z;
            screenLeftBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
            screenRightBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
            screenTopBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
            screenBottomBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
            screenWidth = Screen.width;
            screenHeight = Screen.height;
        }
        public static float ScreenWidth()
        {
            return Instance.screenWidth;
        }
        public static float ScreenHeight()
        {
            return Instance.screenHeight;
        }
        public static float ScreenLeftBound()
        {
            return Instance.screenLeftBound;
        }
        public static float ScreenRightBound()
        {
            return Instance.screenRightBound;
        }
        public static float ScreenTopBound()
        {
            return Instance.screenTopBound;
        }
        public static float ScreenBottomBound()
        {
            return Instance.screenBottomBound;
        }
    }
}
