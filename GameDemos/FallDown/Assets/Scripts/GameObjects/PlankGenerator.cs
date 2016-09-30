using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace FallDownDemo
{
	public class PlankGenerator : MonoBehaviour {
        public System.Random random = new System.Random();
        #region Public Properties       
                                        // Defaults
        public float startXPosition;    // -40.0f
        public float startYPosition;    // -6.0f
        public float initialSpawnTime;  // 1.0f
        public float spawnDistance;     // 3.0f
        public int plankWidth;          // 35
        public Vector3 cameraPosition;
        #endregion
        private float spawnHeight;
        private GameObject block;
        private GameObject ball;
        private GameObject bottomPlank;
		private GameObject lastSpawnedPlank;
        private float rightBorder;
        private float leftBorder;
        private enum Levels
		{
			Beginner = 60,
			Intermediate = 90,
			Expert = 240
		}
        /// <summary>
        /// Use this for initialization
        /// </summary>
		void Start ()
        {   
            block = GameObject.Find ("Block");
            ball = GameObject.Find("Ball");
			bottomPlank = GameObject.Find ("BottomPlank");
			spawnHeight = startYPosition - spawnDistance;
		}

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update ()
        {
            //Debug.Log(string.Format("leftBound @ {0}", CameraManager.ScreenLeftBound()));
            //Debug.Log(string.Format("rightBound @ {0}", CameraManager.ScreenRightBound()));
            //Debug.Log(string.Format("topBound @ {0}", CameraManager.ScreenTopBound()));
            //Debug.Log(string.Format("bottomBound @ {0}", CameraManager.ScreenBottomBound()));
            if ((lastSpawnedPlank != null && bottomPlank.transform.position.y < lastSpawnedPlank.transform.position.y + 10F) ||
			    (Time.timeSinceLevelLoad < initialSpawnTime))
			{
				GeneratePlanks(Time.timeSinceLevelLoad);
				spawnHeight -= spawnDistance;
			}
		}

        /// <summary>
        /// Generates plank a plank with 'numHoles' holes
        /// </summary>
        /// <param name="numHoles"></param>
        void GeneratePlank(List<int> numHoles)
        {
            GameObject plank = new GameObject { name = "Plank" };
			for (int i = 0; i <= plankWidth; i++)
			{
				var intExists =
					(from hole in numHoles
                     where i == hole
					 select hole).ToList ();
				if (intExists.Count > 0) { }
				else
				{
					lastSpawnedPlank = (GameObject)Instantiate (block, new Vector2(CameraManager.ScreenLeftBound()+i, spawnHeight), Quaternion.identity);
					lastSpawnedPlank.transform.parent = plank.transform;
				}
			}
			plank.AddComponent<Plank>();
		}

        /// <summary>
        /// Generates planks given time
        /// </summary>
        /// <param name="time"></param>
        void GeneratePlanks(float time)
		{
			if (time > (float)Levels.Intermediate && time < (float)Levels.Expert)
			{
				GeneratePlank(GenerateHoles(7));
			}
			else if (time > (float)Levels.Expert)
			{
				GeneratePlank(GenerateHoles (5));
			}
			else
			{
				GeneratePlank(GenerateHoles (10));
			}
		}

        /// <summary>
        /// Generate 'numHoles' holes
        /// </summary>
        /// <param name="numHoles"></param>
        /// <returns></returns>
		List<int> GenerateHoles(int numHoles)
		{
			List<int> numbers = new List<int>();
			for (int i = 0; i < numHoles; i++)
			{
				int randomlyGeneratedInt = random.Next(plankWidth);
				while (DoesNumberExist(randomlyGeneratedInt, numbers))
				{
					randomlyGeneratedInt = random.Next (plankWidth);
				}
				numbers.Add (randomlyGeneratedInt);
			}
			return numbers;
		}
	
        /// <summary>
        /// Returns if 'randomInteger' exists in 'existingIntegers'
        /// </summary>
        /// <param name="randomInteger"></param>
        /// <param name="existingIntegers"></param>
        /// <returns></returns>
		bool DoesNumberExist(int randomInteger, List<int> existingIntegers)
		{
			var doesNumberExistQuery =
				from existingInteger in existingIntegers
				where existingInteger == randomInteger
				select existingInteger;
			if (doesNumberExistQuery.Count () > 0)
				return true;
			else
				return false;
		}
	}
}