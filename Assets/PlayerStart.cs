using UnityEngine;
using System.Collections;

public class PlayerStart : MonoBehaviour {
	private static PlayerStart _instance;

	public static PlayerStart instance
	{
		get
		{
			if(_instance == null)
				_instance = GameObject.FindObjectOfType<PlayerStart>();
			return _instance;
		}
	}
}
