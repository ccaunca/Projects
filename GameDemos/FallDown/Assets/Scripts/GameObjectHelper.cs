using UnityEngine;

namespace FallDownDemo
{
	public class GOHelper
	{
		public static GameObject GetByName(string name)
		{
			return GameObject.Find(name);
		}
	}
}